using KiiBlog.Application.Services;
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
        private readonly IBlobStorageService _blobStorage;
        public CommandUploadPlayerHandler(IBlobStorageService blobStorage)
        {
            _blobStorage = blobStorage;
        }
        public async Task<string> Handle(CommandUploadPlayer request, CancellationToken cancellationToken)
        {
            string url = string.Empty;
            try
            {
                var file = request.File;
                if (file == null)
                    return string.Empty;

                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                using var stream = file.OpenReadStream();
                var result = await _blobStorage.UploadPublicFileAsync(stream, fileName);
                if (result != null && result.IS_SUCCESS)
                {
                    url = _blobStorage.GetPublicFileUrl(fileName);
                }
            }
            catch (Exception ex)
            {
                // Log here
            }

            return url;
        }
    }
}
