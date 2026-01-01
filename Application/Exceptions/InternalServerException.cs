namespace Application.Exceptions
{
    public class InternalServerException(string message) : BaseException(message, 500)
    {
    }
}
