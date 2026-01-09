namespace Application.Common.Errors
{
    public class Error
    {
        public  string Code { get; set; } // User.NotFound, User.InvalidPassword, etc.
        public  string Message { get; set; } // User not found, Invalid password, etc.

        public ErrorType Type { get; set; } // Validation, NotFound, Unauthorized, etc.

        public Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }
        
    }
}
