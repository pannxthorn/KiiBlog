using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.Players
{
    public class CommandUploadPlayer : IRequest<string>
    {
        public IFormFile File;
        public string Type { get; set; }

        public CommandUploadPlayer(IFormFile file, string type)
        {
            File = file;
            Type = type;
        }
    }
}
