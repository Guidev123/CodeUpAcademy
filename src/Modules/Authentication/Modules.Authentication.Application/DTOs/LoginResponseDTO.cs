namespace Modules.Authentication.Application.DTOs;

public class LoginResponseDTO
{
    public LoginResponseDTO(string accessToken, Guid refreshToken, UserTokenDTO userToken, double expiresIn)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserToken = userToken;
        ExpiresIn = expiresIn;
    }

    public string AccessToken { get; set; } = string.Empty;
    public Guid RefreshToken { get; set; }
    public UserTokenDTO UserToken { get; set; } = null!;
    public double ExpiresIn { get; set; }
}
