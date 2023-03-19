using Core.Constants;

namespace Core.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(){}
    
    public ItemNotFoundException(string message = RESPONSE_CONSTANTS.ERROR.NOT_FOUND) : base(message){}
}