namespace Environment.Api.V1.Common.ExceptionError;

public class SchoolException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }

    public SchoolException(int code, string message, bool? global = true) : base(message)
    {
        Code = code;
        Global = global;
    }
}
