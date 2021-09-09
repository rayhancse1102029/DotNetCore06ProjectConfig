using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using DotNetCore06ProjectConfig.Service.Helper.Interfaces;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DotNetCore06ProjectConfig.Service.Helper
{
    public class FileSaveService : IFileSaveService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileSaveService(IHostingEnvironment _hostingEnvironment)
        {
            this._hostingEnvironment = _hostingEnvironment;
        }


        public string SaveEmpAttachment(out string fileName, IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".xlsx", ".csv", ".docx" };
            string message = "success";

            var extention = Path.GetExtension(file.FileName);
            fileName = Path.Combine("EmpAttachment", DateTime.Now.Ticks + extention);

            if (file.Length > 10000000)
            {
                message = "Select file must be less than 10Μ";
            }
            else if (!allowedExtensions.Contains(extention.ToLower()))
            {
                message = "Must be jpg, jpeg, png, pdf, xlsx, csv or docx types";
            }
            else
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, fileName);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch
                {
                    message = "can not upload image";
                }
            }
            return message;
        }

        public string SaveFile(out string fileName, IFormFile file, string localPath)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".xlsx", ".csv", ".docx" };
            string message = "success";

            var extention = Path.GetExtension(file.FileName);
            fileName = Path.Combine("Upload", DateTime.Now.Ticks + extention);

            if (file.Length > 10000000)
            {
                message = "Select file must be less than 10Μ";
            }
            else if (!allowedExtensions.Contains(extention.ToLower()))
            {
                message = "Must be jpg, jpeg, png, pdf, xlsx, csv or docx types";
            }
            else
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, fileName);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch
                {
                    message = "can not upload image";
                }
            }
            return message;
        }

        public string SaveImage(out string fileName, IFormFile img)
        {
            
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png",".gif", ".webp", };
            string message = "success";

            var extention = Path.GetExtension(img.FileName);
            fileName = Path.Combine("Upload", DateTime.Now.Ticks + extention);

            if (img.Length > 10000000)
            {
                message = "Select file must be less than 10Μ";
            }
            else if (!allowedExtensions.Contains(extention.ToLower()))
            {
                message = "Must be jpg, jpeg, png, gif or webp types";
            }
            else
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, fileName);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                }
                catch
                {
                    message = "can not upload image";
                }
            }
            return message;
        }

        public string SaveUserImage(out string fileName, IFormFile img)
        {
            
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png",".gif", ".webp", };
            string message = "success";

            var extention = Path.GetExtension(img.FileName);
            fileName = Path.Combine("UploadUserImage", DateTime.Now.Ticks + extention);

            if (img.Length > 10000000)
            {
                message = "Select file must be less than 10Μ";
            }
            else if (!allowedExtensions.Contains(extention.ToLower()))
            {
                message = "Must be jpg, jpeg, png, gif or webp types";
            }
            else
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, fileName);
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                }
                catch
                {
                    message = "can not upload image";
                }
            }
            return message;
        }
    }
}
