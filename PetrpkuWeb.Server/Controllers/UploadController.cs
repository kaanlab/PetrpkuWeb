using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            //string fullPath = Path.Combine("UploadFiles", fileName);
            var tempFileName = @"UploadFiles/2.jpeg";
            await using (var writer = System.IO.File.OpenWrite(tempFileName))
            {
                await Request.Body.CopyToAsync(writer);
            }
            return Ok(tempFileName);
        }
    }
}