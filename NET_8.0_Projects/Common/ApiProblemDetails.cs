using Microsoft.AspNetCore.Mvc;

namespace NET_8._0_Projects.Common
{
    public class ApiProblemDetails : ProblemDetails
    {
        public string? ErrorCode { get; set; }
        public string? TraceId { get; set; }
    }
}
