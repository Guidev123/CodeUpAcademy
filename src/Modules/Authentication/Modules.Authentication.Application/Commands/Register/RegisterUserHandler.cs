using CodeUp.Common.Abstractions;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Mappers;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Register;

public sealed class RegisterUserHandler(IUserRepository userRepository,
                    ITokenService tokenService,
                    IPasswordHasherService passwordHasher,
                    IUnitOfWork uow)
    : ICommandHandler<RegisterUserCommand, LoginResponseDTO>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _uow = uow;    

    public async Task<Response<LoginResponseDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validation = new RegisterUserValidation().Validate(request);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").Cast<string?>().ToList();
            return new(null, 400, "Invalid Operation", errors);
        }

        var userExists = await _userRepository.GetByEmailAsync(request.Email);
        if (userExists is not null)
            return new(null, 400, "Invalid Operation", ["User already exists"]);

        var user = request.MapToEntity(_passwordHasher.HashPassword(request.Password));

        await _userRepository.CreateAsync(user);
        if (!await _uow.SaveChangesAsync())
            return new(null, 400, "Invalid Operation", ["Fail to persist data"]);

        var token = await _tokenService.GenerateJWT(request.Email);
        if (!token.IsSuccess)
            return new(null, 400, "Invalid Operation", ["Fail to authenticate user"]);

        return new(token.Data, 201);
    }
}
