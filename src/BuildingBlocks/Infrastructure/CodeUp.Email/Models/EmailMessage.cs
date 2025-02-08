using Microsoft.AspNetCore.Http;

namespace CodeUp.Email.Models;

public record EmailMessage(string To, string Subject, string Content, IFormFile? Attachments = null);
