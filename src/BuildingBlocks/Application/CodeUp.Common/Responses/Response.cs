﻿using System.Text.Json.Serialization;

namespace CodeUp.Common.Responses;

public class Response<TData>
{
    [JsonIgnore]
    public const int DEFAULT_SUCCESS_STATUS_CODE = 200;
    public const int DEFAULT_ERROR_STATUS_CODE = 400;
    private readonly int _code;

    [JsonConstructor]
    public Response() => _code = DEFAULT_SUCCESS_STATUS_CODE;

    public Response(
        TData? data,
        int code = DEFAULT_SUCCESS_STATUS_CODE,
        string? message = null,
        List<string>? errors = null)
    {
        Data = data;
        Message = message;
        Errors = errors;
        _code = code;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public bool IsSuccess => _code is >= 200 and <= 299;

    public static Response<TData> Success(TData? data, int code = DEFAULT_SUCCESS_STATUS_CODE, string? message = null)
        => new(data, code, message);

    public static Response<TData> Failure(List<string> errors, string? message = null, int code = DEFAULT_ERROR_STATUS_CODE)
        => new(default, code, message, errors);
}
