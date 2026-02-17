using KiiBlog.Application.Services;
using KiiBlog.Application.UnitOfWork;
using KillBlog.DTO.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Players
{
    public class CommandUploadPlayerHandler : IRequestHandler<CommandUploadPlayer, BASE_RESULT<string>>
    {
        private readonly IBlobStorageService _blobStorage;
        private readonly IImageProcessingService _imageProcessingService;
        public CommandUploadPlayerHandler(IBlobStorageService blobStorage, IImageProcessingService imageProcessingService)
        {
            _blobStorage = blobStorage;
            _imageProcessingService = imageProcessingService;
        }
        public async Task<BASE_RESULT<string>> Handle(CommandUploadPlayer request, CancellationToken cancellationToken)
        {
            BASE_RESULT<string> res = new BASE_RESULT<string>();
            try
            {
                var file = request.File;
                if (file == null)
                    return new BASE_RESULT<string>() { MESSAGE = "null" };

                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                using var originalStream = file.OpenReadStream();
                var resizedBytes = await _imageProcessingService.ResizeImageAsync(originalStream, request.Type);

                using var resizedStream = new MemoryStream(resizedBytes);
                var result = await _blobStorage.UploadPublicFileAsync(resizedStream, fileName);
                if (result != null && result.IS_SUCCESS)
                {
                    res.IS_SUCCESS = true;
                    res.MESSAGE = "สำเร็จ";
                    res.RESULT = _blobStorage.GetPublicFileUrl(fileName, false);
                }
            }
            catch (Exception ex)
            {
                // Log here
            }

            return res;
        }
    }
}
