﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("file"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            //var file = Request.Form.Files[0];
            //string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var tempFileName = Path.GetTempFileName();
            string fileName = Path.GetRandomFileName().Substring(0,8) + ".jpg";
            string fullPath = $@"uploadfolder/{fileName}";

            //var tempFileName = @"UploadFiles/2.jpeg";
            await using (var writer = System.IO.File.OpenWrite(tempFileName))
            {
                await Request.Body.CopyToAsync(writer);               
            }

            using (Image image = Image.Load(tempFileName))
            {
                if (image.Width < 600)
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

                image.Save(fullPath);
            }

            return Ok(fullPath);
        }
    }
}