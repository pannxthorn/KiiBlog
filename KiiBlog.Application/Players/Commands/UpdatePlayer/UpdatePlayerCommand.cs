using KillBlog.DTO.Base;
using KillBlog.DTO.Players;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class UpdatePlayerCommand : IRequest<BASE_RESULT<bool>>
    {
        public int PlayerId { get; set; }
        public PARAM_PLAYER_DTO Param { get; set; }

        public UpdatePlayerCommand(int playerId, PARAM_PLAYER_DTO param)
        {
            PlayerId = playerId;
            Param = param;
        }
    }
}
