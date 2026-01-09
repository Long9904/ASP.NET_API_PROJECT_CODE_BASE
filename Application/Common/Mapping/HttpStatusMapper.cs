using Application.Common.Errors;

namespace Application.Common.Mapping
{
    public static class HttpStatusMapper
    {
        public static int Map(ErrorType type) => type switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            _ => 500
        };
    }
}
