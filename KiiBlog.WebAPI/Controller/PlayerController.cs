using MediatR;
using Microsoft.AspNetCore.Mvc;
using KiiBlog.Application.Players;
using KillBlog.DTO.Base;
using KillBlog.DTO.Players;

namespace KiiBlog.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<IEnumerable<RESULT_PLAYER_DTO>> GetAll()
        {
            return await _mediator.Send(new GetAllPlayersQuery());
        }

        [HttpPost]
        public async Task<BASE_RESULT<int>> Create([FromBody] PARAM_PLAYER_DTO param)
        {
            return await _mediator.Send(new CreatePlayerCommand(param));
        }

        [HttpPut("{id}")]
        public async Task<BASE_RESULT<bool>> Update(int id, [FromBody] PARAM_PLAYER_DTO param)
        {
            return await _mediator.Send(new UpdatePlayerCommand(id, param));
        }

        [HttpDelete("{id}")]
        public async Task<BASE_RESULT<bool>> Delete(int id)
        {
            return await _mediator.Send(new DeletePlayerCommand(id));
        }

        [HttpPost("UploadPlayerImage")]
        public async Task<BASE_RESULT<string>> UploadFile([FromForm] IFormFile file, [FromQuery] string type = "general")
        {
            return await _mediator.Send(new CommandUploadPlayer(file, type));
        }
    }
}
