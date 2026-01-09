using Application.Common.Errors;
using Application.Common.Mapping;

namespace NET_8._0_Projects.Common
{
    public static class ProblemDetailsFactoryCustom
    {
        public static ApiProblemDetails FromError(HttpContext context, Error error)
        {
            var status = HttpStatusMapper.Map(error.Type);

            return new ApiProblemDetails
            {
                Type = $"https://errors.myapp.com/{error.Code}",
                Title = error.Code,
                Detail = error.Message,
                Status = status,
                Instance = context.Request.Path,
                ErrorCode = error.Code,
                TraceId = context.TraceIdentifier
            };
        }
    }
}
