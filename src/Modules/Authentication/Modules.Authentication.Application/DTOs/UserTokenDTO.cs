namespace Modules.Authentication.Application.DTOs;

public class UserTokenDTO
{
    public UserTokenDTO(Guid id, string email, IEnumerable<ClaimDTO> claims)
    {
        Id = id;
        Email = email;
        Claims = claims;
    }

    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public IEnumerable<ClaimDTO> Claims { get; set; } = [];
}
