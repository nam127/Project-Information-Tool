namespace PIMTool.Core.Exceptions;

public class ConcurrencyUpdateException : Exception
{
    public ConcurrencyUpdateException(string? message) : base(message)
    {
    }

    public ConcurrencyUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}