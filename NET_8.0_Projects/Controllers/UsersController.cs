using Application.Features.UserFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NET_8._0_Projects.Common;


namespace NET_8._0_Projects.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return StatusCode(StatusCodes.Status201Created, result.Value);

            var problem = ProblemDetailsFactoryCustom.FromError(HttpContext, result.Error!);
            return StatusCode(problem.Status!.Value, problem);
        }
    }
}