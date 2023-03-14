namespace Core.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(){}

    public EmailAlreadyInUseException(string message = "Error: Email already in use!") : base(message)
    {
        
    }
}