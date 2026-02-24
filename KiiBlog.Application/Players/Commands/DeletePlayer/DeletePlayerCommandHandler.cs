using KiiBlog.Application.UnitOfWork;
using KillBlog.DTO.Base;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, BASE_RESULT<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BASE_RESULT<bool>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var res = new BASE_RESULT<bool>();
            try
            {
                var player = await _unitOfWork.Player.GetAsync(f => f.PLAYER_ID == request.PlayerId && f.IS_DELETE == false);
                if (player == null)
                {
                    res.MESSAGE = "ไม่พบข้อมูล Player";
                    return res;
                }

                // Soft delete
                player.IS_DELETE = true;
                player.IS_ACTIVE = false;
                player.LAST_UPDATE_ID = 1;
                player.LAST_UPDATE_DATE = DateTime.Now;

                _unitOfWork.Player.Update(player);
                await _unitOfWork.SaveChangesAsync();

                res.IS_SUCCESS = true;
                res.MESSAGE = "ลบข้อมูลสำเร็จ";
                res.RESULT = true;
            }
            catch (Exception ex)
            {
                res.IS_SUCCESS = false;
                res.MESSAGE = ex.Message;
            }

            return res;
        }
    }
}
