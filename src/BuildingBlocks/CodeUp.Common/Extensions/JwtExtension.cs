namespace CodeUp.Common.Extensions;

public class JwtExtension
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
    public double RefreshTokenExpirationInHours { get; set; }
}
