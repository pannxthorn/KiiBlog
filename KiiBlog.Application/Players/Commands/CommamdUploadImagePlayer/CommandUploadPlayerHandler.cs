using KiiBlog.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Players
{
    public class CommandUploadPlayerHandler : IRequestHandler<CommandUploadPlayer, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommandUploadPlayerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CommandUploadPlayer request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
