using KillBlog.DTO.Base;
using KillBlog.DTO.Players;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class CreatePlayerCommand : IRequest<BASE_RESULT<int>>
    {
        public PARAM_PLAYER_DTO Param { get; set; }

        public CreatePlayerCommand(PARAM_PLAYER_DTO param)
        {
            Param = param;
        }
    }
}
