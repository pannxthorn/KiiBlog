using KiiBlog.Application.UnitOfWork;
using KillBlog.DTO.Players;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Players
{
    public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<RESULT_PLAYER_DTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPlayersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RESULT_PLAYER_DTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var res = new List<RESULT_PLAYER_DTO>();
            try
            {
                var listPlayer = await _unitOfWork.Player.GetManyAsync(f => f.IS_ACTIVE == true && f.IS_DELETE == false);
                res.Capacity = listPlayer.Count();
                foreach (var player in listPlayer)
                {
                    var result = new RESULT_PLAYER_DTO();
                    result.PLAYER_ID = player.PLAYER_ID;
                    result.PLAYER_NO = player.PLAYER_NO;
                    result.PLAYER_NAME = player.PLAYER_NAME;
                    result.PLAYER_PROFILE = player.PLAYER_PROFILE;
                    result.CONTRACT_TYPE_ID = player.CONTRACT_TYPE_ID;
                    result.CONTRACT_TYPE_CODE = player.CONTRACT_TYPE_CODE;
                    result.CONTRACT_TYPE_NAME = player.CONTRACT_TYPE_NAME;
                    result.TRANSFER_STATUS_ID = player.TRANSFER_STATUS_ID;
                    result.TRANSFER_STATUS_CODE = player.TRANSFER_STATUS_CODE;
                    result.TRANSFER_STATUS_NAME = player.TRANSFER_STATUS_NAME;

                    result.IS_ACTIVE = player.IS_ACTIVE;
                    result.IS_DELETE = player.IS_DELETE;

                    res.Add(result);
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
