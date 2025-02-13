using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Microsoft.AspNetCore.Http;
using Modules.Students.Application.Helpers;
using Modules.Students.Application.Services;
using Modules.Students.Domain.Repositories;

namespace Modules.Students.Application.Commands.AddProfilePicture;

public sealed class AddProfilePictureHandler(INotificator notificator,
                                             IBlobService blob,
                                             IUserService user,
                                             IStudentRepository studentRepository)
                                           : CommandHandler<AddProfilePictureCommand, AddProfilePictureResponse>(notificator)
{
    private readonly IBlobService _blob = blob;
    private readonly IUserService _user = user;
    private readonly IStudentRepository _studentRepository = studentRepository;
    public override async Task<Response<AddProfilePictureResponse>> Handle(AddProfilePictureCommand request, CancellationToken cancellationToken)
    {
        if (!ExecuteValidation(new AddProfilePictureValidation(), request))
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), "Invalid Operation");

        var userId = _user.GetUserId();
        if(userId is null || !userId.HasValue)
        {
            Notify("User not found.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        var student = await _studentRepository.GetByIdAsync(userId.Value);
        if(student is null)
        {
            Notify("Student not found.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        var file = Base64FileConverter.ConvertBase64ToIFormFile(request.ProfilePicture).Data;
        if(file is null)
        {
            Notify("Something has failed to save your profile picture.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), "Invalid Operation");

        }

        var imageUrl = await UploadImageAsync(file);
        if (string.IsNullOrEmpty(imageUrl))
        {
            Notify("Something has failed to save your profile picture.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), "Invalid Operation");

        }

        student.SetProfilePicture(imageUrl);

        return Response<AddProfilePictureResponse>.Success(null);
    }

    private async Task<string> UploadImageAsync(IFormFile formFile)
    {
        using Stream stream = formFile.OpenReadStream();

        return await _blob.UploadAsync(stream, formFile.ContentType);
    }
}
