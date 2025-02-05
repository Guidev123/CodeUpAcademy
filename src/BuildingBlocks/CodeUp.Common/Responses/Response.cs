using System.Text.Json.Serialization;

namespace CodeUp.Common.Responses;

public class Response<TData>
{
    [JsonIgnore]
    public readonly int StatusCode;
    public const int DEFAULT_STATUS_CODE = 200;

    public Response() { }

    public Response(
        TData? data,
        int? code = null,
        string? message = null,
        List<string?>? errors = null)
    {
        StatusCode = code ?? DEFAULT_STATUS_CODE;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public TData? Data { get; set; }
    public string? Message { get; }
    public List<string?>? Errors { get; set; }
    public bool IsSuccess =>
        StatusCode is >= DEFAULT_STATUS_CODE and <= 299;

    public void AddError(string message) => Errors?.Add(message);
}
