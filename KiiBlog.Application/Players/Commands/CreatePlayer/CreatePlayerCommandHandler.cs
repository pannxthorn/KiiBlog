using KiiBlog.Application.UnitOfWork;
using KiiBlog.Domain.Entities;
using KillBlog.DTO.Base;
using MediatR;

namespace KiiBlog.Application.Players
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, BASE_RESULT<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BASE_RESULT<int>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var res = new BASE_RESULT<int>();
            try
            {
                var param = request.Param;
                var player = new PLAYER
                {
                    PLAYER_NO = param.PLAYER_NO,
                    PLAYER_NAME = param.PLAYER_NAME,
                    PLAYER_PROFILE = param.PLAYER_PROFILE,
                    CONTRACT_TYPE_ID = param.CONTRACT_TYPE_ID,
                    CONTRACT_TYPE_CODE = param.CONTRACT_TYPE_CODE,
                    CONTRACT_TYPE_NAME = param.CONTRACT_TYPE_NAME,
                    TRANSFER_STATUS_ID = param.TRANSFER_STATUS_ID,
                    TRANSFER_STATUS_CODE = param.TRANSFER_STATUS_CODE,
                    TRANSFER_STATUS_NAME = param.TRANSFER_STATUS_NAME,
                    IS_ACTIVE = true,
                    IS_DELETE = false,
                    CREATED_BY_ID = 1,
                    CREATED_DATE = DateTime.Now,
                    LAST_UPDATE_ID = 1,
                    LAST_UPDATE_DATE = DateTime.Now
                };

                await _unitOfWork.Player.AddAsync(player);
                await _unitOfWork.SaveChangesAsync();

                res.IS_SUCCESS = true;
                res.MESSAGE = "สร้างข้อมูลสำเร็จ";
                res.RESULT = player.PLAYER_ID;
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
