using MediatR;
using Microsoft.AspNetCore.Mvc;
using KiiBlog.Application.Players;

namespace KiiBlog.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator) { _mediator = mediator; }

        [HttpPost("UploadPlayerImage")]
        public async Task<string> UploadFile([FromForm] IFormFile file, [FromQuery] string type = "general")
        {
            return await _mediator.Send(new CommandUploadPlayer(file, type));
        }
    }
}
