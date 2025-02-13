using CodeUp.Common.Abstractions;

namespace Modules.Students.Application.Commands.AddProfilePicture;

public record AddProfilePictureCommand(string ProfilePicture) : Command<AddProfilePictureResponse>;
