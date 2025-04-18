﻿using System.Text.Json.Serialization;

namespace CodeUp.Common.Responses;

public class Response<TData>
{
    [JsonIgnore]
    private readonly int _code;

    public const int DEFAULT_SUCCESS_STATUS_CODE = 200;
    public const int DEFAULT_ERROR_STATUS_CODE = 400;
    public const string DEFAULT_ERROR_MESSAGE = "Invalid Operation.";
    public const string DEFAULT_SUCCESS_MESSAGE = "Valid Operation.";

    [JsonConstructor]
    protected Response() => _code = DEFAULT_SUCCESS_STATUS_CODE;

    protected Response(
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

    public static Response<TData> Success(TData? data, int code = DEFAULT_SUCCESS_STATUS_CODE, string? message = DEFAULT_SUCCESS_MESSAGE)
        => new(data, code, message);

    public static Response<TData> Failure(List<string> errors, string? message = DEFAULT_ERROR_MESSAGE, int code = DEFAULT_ERROR_STATUS_CODE)
        => new(default, code, message, errors);
}