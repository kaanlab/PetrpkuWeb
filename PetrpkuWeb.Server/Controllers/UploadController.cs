using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost("avatar"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var tempFileName = Path.GetTempFileName();
            var fileName = Path.GetRandomFileName().Substring(0, 8) + ".jpg";
            var path = Path.Combine("uploadfolder/avatars", fileName);

            await using (var writer = System.IO.File.OpenWrite(tempFileName))
            {
                await Request.Body.CopyToAsync(writer);
            }

            using (Image image = Image.Load(tempFileName))
            {
                if (image.Width > 400 && image.Width < 600)
                {
                    image.Mutate(x => x.Resize(image.Width / 4, image.Height / 4));
                }
                else if (image.Width > 600 && image.Width < 1000)
                {
                    image.Mutate(x => x.Resize(image.Width / 5, image.Height / 5));
                }
                else if (image.Width > 1000 && image.Width < 1400)
                {
                    image.Mutate(x => x.Resize(image.Width / 6, image.Height / 6));
                }
                else if (image.Width > 1400 && image.Width < 3000)
                {
                    image.Mutate(x => x.Resize(image.Width / 8, image.Height / 8));
                }
                else if (image.Width > 3000 && image.Width < 5000)
                {
                    image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));
                }
                else if (image.Width > 5000)
                {
                    image.Mutate(x => x.Resize(image.Width / 20, image.Height / 20));
                }

                image.Save(path);
            }

            return Ok(path);
        }

        [HttpPost("files"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var result = new List<FileUploadViewModel>();
                var dirPath = CreateRandomNameDirectory();

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(dirPath, file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    };

                    if (extension.StartsWith(".jp") || extension.StartsWith(".pn"))
                    {
                        using (Image image = Image.Load(path))
                        {
                            if (image.Width > 400 && image.Width < 600)
                            {
                                image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                            }
                            else if (image.Width > 600 && image.Width < 1000)
                            {
                                image.Mutate(x => x.Resize(image.Width / 3, image.Height / 3));
                            }
                            else if (image.Width > 1000 && image.Width < 1400)
                            {
                                image.Mutate(x => x.Resize(image.Width / 4, image.Height / 4));
                            }
                            else if (image.Width > 1400 && image.Width < 3000)
                            {
                                image.Mutate(x => x.Resize(image.Width / 5, image.Height / 5));
                            }
                            else if (image.Width > 3000 && image.Width < 5000)
                            {
                                image.Mutate(x => x.Resize(image.Width / 6, image.Height / 6));
                            }
                            else if (image.Width > 5000)
                            {
                                image.Mutate(x => x.Resize(image.Width / 7, image.Height / 7));
                            }

                            image.Save(path);
                        }
                    }

                    result.Add(new FileUploadViewModel() { Name = file.FileName, Length = file.Length, Path = path, Extension = extension });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        private string CreateRandomNameDirectory()
        {
            var uniqueDir = Path.Combine("uploadfolder", Path.GetRandomFileName().Substring(0, 6));
            Directory.CreateDirectory(uniqueDir);
            return uniqueDir;
        }
    }
}

