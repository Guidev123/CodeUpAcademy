﻿namespace Modules.Students.Application.Services;

public interface IBlobService
{
    Task<string> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
}