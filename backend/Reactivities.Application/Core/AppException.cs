namespace Reactivities.Application.Core;
public class AppException(int statuscode, string message, string details = null)
{
    public int StatusCode { get; set; } = statuscode;
    public string Message { get; set; } = message;
    public string Details { get; set; } = details;
}
