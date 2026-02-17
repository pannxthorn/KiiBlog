using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Services
{
    public interface IImageProcessingService
    {
        Task<byte[]> ResizeImageAsync(Stream inputStream, string type = "general");
    }
}
