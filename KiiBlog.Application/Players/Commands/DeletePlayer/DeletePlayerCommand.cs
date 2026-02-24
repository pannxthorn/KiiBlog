using KillBlog.DTO.Base;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class DeletePlayerCommand : IRequest<BASE_RESULT<bool>>
    {
        public int PlayerId { get; set; }

        public DeletePlayerCommand(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
