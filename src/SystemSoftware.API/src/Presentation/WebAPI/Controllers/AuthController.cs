using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SystemSoftware.Application.Features.Auth.Commands;
using SystemSoftware.Application.Features.Auth.Models;

namespace SystemSoftware.Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken(RefreshTokenCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
