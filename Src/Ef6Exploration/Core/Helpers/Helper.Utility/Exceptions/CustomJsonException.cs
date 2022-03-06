namespace Helper.Utility.Exceptions;

public sealed class CustomJsonException : ApplicationException
{
    public CustomJsonException()
    {
    }

    public CustomJsonException(string message) : base(message)
    {
    }

    public CustomJsonException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public CustomJsonException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public CustomJsonException(string message, int statusCode, Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}