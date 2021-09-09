using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Service.Helper.Interfaces
{
    public interface IFileSaveService
    {
        string SaveFile(out string fileName, IFormFile file, string localPath);
        string SaveEmpAttachment(out string fileName, IFormFile file);
        string SaveImage(out string fileName, IFormFile img);
        string SaveUserImage(out string fileName, IFormFile img);
    }
}
