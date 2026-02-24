using KiiBlog.Application.UnitOfWork;
using KillBlog.DTO.Base;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, BASE_RESULT<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BASE_RESULT<bool>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
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

                var param = request.Param;
                player.PLAYER_NO = param.PLAYER_NO;
                player.PLAYER_NAME = param.PLAYER_NAME;
                player.PLAYER_PROFILE = param.PLAYER_PROFILE;
                player.CONTRACT_TYPE_ID = param.CONTRACT_TYPE_ID;
                player.CONTRACT_TYPE_CODE = param.CONTRACT_TYPE_CODE;
                player.CONTRACT_TYPE_NAME = param.CONTRACT_TYPE_NAME;
                player.TRANSFER_STATUS_ID = param.TRANSFER_STATUS_ID;
                player.TRANSFER_STATUS_CODE = param.TRANSFER_STATUS_CODE;
                player.TRANSFER_STATUS_NAME = param.TRANSFER_STATUS_NAME;
                player.IS_ACTIVE = param.IS_ACTIVE;
                player.LAST_UPDATE_ID = 1;
                player.LAST_UPDATE_DATE = DateTime.Now;

                _unitOfWork.Player.Update(player);
                await _unitOfWork.SaveChangesAsync();

                res.IS_SUCCESS = true;
                res.MESSAGE = "อัพเดทข้อมูลสำเร็จ";
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
