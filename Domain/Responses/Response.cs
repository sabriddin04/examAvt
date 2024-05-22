using System.Net;

namespace Domain.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public List<string>? Errors { get; set; } = new();
    public T? Data { get; set; }

    public Response(T data)
    {
        StatusCode = 200;
        Data = data;
    }

    public Response(HttpStatusCode statusCode, string error)
    {
        StatusCode = (int)statusCode;
        Errors!.Add(error);
    }

    public Response(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = (int)statusCode;
        Errors = errors;
    }

    public Response(T data, HttpStatusCode statusCode, List<string> error)
    {
        Data = data;
        StatusCode = (int)statusCode;
        Errors = error;
    }

    public Response(T data, HttpStatusCode statusCode, string error)
    {
        Data = data;
        StatusCode = (int)statusCode;
        Errors!.Add(error);
    }
}