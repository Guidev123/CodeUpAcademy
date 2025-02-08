using System.Text.Json.Serialization;

namespace CodeUp.Common.Responses;

public class Response<TData>
{
    [JsonIgnore]
    public readonly int? StatusCode;
    public const int DEFAULT_SUCCESS_STATUS_CODE = 200;
    public const int DEFAULT_ERROR_STATUS_CODE = 400;

    public Response() { }

    protected Response(
        TData? data,
        int? code = DEFAULT_SUCCESS_STATUS_CODE,
        string? message = null,
        List<string>? errors = null)
    {
        StatusCode = code;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public bool IsSuccess =>
        StatusCode is >= DEFAULT_SUCCESS_STATUS_CODE and <= 299;

    public static Response<TData> Success(TData? data, int code = DEFAULT_SUCCESS_STATUS_CODE, string? message = null)
        => new(data, code, message);

    public static Response<TData> Failure(List<string> errors, string? message = null, int? code = DEFAULT_ERROR_STATUS_CODE)
        => new(default, code, message, errors);
}
