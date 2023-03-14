namespace Core.Exceptions;

public class UserNameAlreadyInUseException : Exception
{
    public UserNameAlreadyInUseException(){}

    public UserNameAlreadyInUseException(string message = "Error: Username already in use!") : base(message)
    {
        
    }
}