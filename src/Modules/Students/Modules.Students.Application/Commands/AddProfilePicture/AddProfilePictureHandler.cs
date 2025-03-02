using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
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
            return Response<AddProfilePictureResponse>.Failure(GetNotifications());

        var userId = _user.GetUserId();
        if (userId is null || !userId.HasValue)
        {
            Notify("User not found.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), code: 404);
        }

        var student = await _studentRepository.GetByIdAsync(userId.Value);
        if (student is null)
        {
            Notify("Student not found.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications(), code: 404);
        }

        var imageStream = ConvertBase64ToStream(request.ProfilePicture, out string contentType);
        if (imageStream is null)
        {
            Notify("Failed to process image.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications());
        }

        var imageUrl = await _blob.UploadAsync(imageStream, contentType, cancellationToken);
        if (string.IsNullOrEmpty(imageUrl))
        {
            Notify("Something has failed to save your profile picture.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications());
        }

        student.SetProfilePicture(imageUrl);
        _studentRepository.Update(student);

        if (!await _studentRepository.SaveChangesAsync())
        {
            Notify("Fail to persist data.");
            return Response<AddProfilePictureResponse>.Failure(GetNotifications());
        }

        return Response<AddProfilePictureResponse>.Success(null);
    }

    private static Stream? ConvertBase64ToStream(string base64, out string contentType)
    {
        try
        {
            var parts = base64.Split(',');
            var metadata = parts.Length > 1 ? parts[0] : "";
            var base64Data = parts.Length > 1 ? parts[1] : parts[0];

            contentType = metadata.Contains("image/") ? metadata.Split(';')[0].Split(':')[1] : "image/png";

            byte[] imageBytes = Convert.FromBase64String(base64Data);
            return new MemoryStream(imageBytes);
        }
        catch
        {
            contentType = "image/png";
            return null;
        }
    }
}