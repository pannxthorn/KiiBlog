using KillBlog.DTO.Players;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Players
{
    public class GetAllPlayersQuery : IRequest<IEnumerable<RESULT_PLAYER_DTO>>
    {
    }
}
