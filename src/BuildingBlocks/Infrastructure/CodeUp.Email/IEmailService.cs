using CodeUp.Email.Models;

namespace CodeUp.Email;

public interface IEmailService
{
    Task SendAsync(EmailMessage email);
}
