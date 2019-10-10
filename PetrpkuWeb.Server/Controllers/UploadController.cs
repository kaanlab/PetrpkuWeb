using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;
using PetrpkuWeb.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ImageMagick;

namespace PetrpkuWeb.Server.Controllers
{
    [Authorize(Roles = "admin_webportal, kadry_webportal, user_webportal")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UploadController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("avatar"), DisableRequestSizeLimit]
        public async Task<ActionResult<List<Attachment>>> UploadAvatar(IFormFile file)
        {
            try
            {
                var dirPath = Path.Combine("uploadfolder", DateTime.Now.ToString("MMyyyy"));

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(dirPath, file.FileName);

                if (System.IO.File.Exists(path))
                {
                    dirPath = CreateRandomNameDirectory();
                    path = Path.Combine(dirPath, file.FileName);
                }

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                
                using (MagickImage image = new MagickImage(path))
                {
                    image.AutoOrient();

                    if (image.Width > 450)
                    {
                        image.Resize(450,0);
                        image.Crop(420, 420, Gravity.North);
                        image.RePage();
                    }

                    image.Quality = 40;
                    image.Write(path);
                    
                }

                var length = new System.IO.FileInfo(path).Length;

                var attachment = new Attachment()
                {
                    Name = file.FileName,
                    Length = length,
                    Path = path,
                    Extension = extension,
                    IsImage = true
                };

                _db.Attachments.Add(attachment);
                await _db.SaveChangesAsync();

                return Ok(attachment);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost("files"), DisableRequestSizeLimit]
        public async Task<ActionResult<List<Attachment>>> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var result = new List<Attachment>();
                var dirPath = Path.Combine("uploadfolder", DateTime.Now.ToString("MMyyyy"));

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                foreach (var file in files)
                {
                    var isImage = false;
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(dirPath, file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        dirPath = CreateRandomNameDirectory();
                        path = Path.Combine(dirPath, file.FileName);
                    }

                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (extension.ToLower().StartsWith(".jp") || extension.ToLower().StartsWith(".png"))
                    {
                        isImage = true;
                        using (MagickImage image = new MagickImage(path))
                        {
                            image.AutoOrient();
                            image.Quality = 40;

                            if (image.Width > 1920)
                                image.Resize(1920, 0);
                            
                            image.Write(path);
                        }
                    }

                    var length = new System.IO.FileInfo(path).Length;

                    var attachment = new Attachment()
                    {
                        Name = file.FileName,
                        Length = length,
                        Path = path,
                        Extension = extension,
                        IsImage = isImage
                    };

                    _db.Attachments.Add(attachment);
                    await _db.SaveChangesAsync();

                    result.Add(attachment);
                }
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpPut("updateinfo/{attachmentId:int}")]
        public async Task<ActionResult<Attachment>> PutAttachmentInfoAsync(int attachmentId, Attachment attachment)
        {
            if (attachmentId != attachment.AttachmentId)
            {
                return BadRequest();
            }

            _db.Entry(attachment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(attachment);
        }

        [HttpDelete("deleteinfo/{attachmentId:int}")]
        public async Task<ActionResult> DeleteAttachmentInfoAsync([FromRoute] int attachmentId)
        {
            var attachmentInfo = await _db.Attachments.FindAsync(attachmentId);

            if (attachmentInfo is null)
            {
                return NotFound();
            }

            _db.Attachments.Remove(attachmentInfo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private string CreateRandomNameDirectory()
        {
            var uniqueDir = Path.Combine("uploadfolder", Path.GetRandomFileName().Substring(0, 6));
            Directory.CreateDirectory(uniqueDir);
            return uniqueDir;
        }
    }
}

