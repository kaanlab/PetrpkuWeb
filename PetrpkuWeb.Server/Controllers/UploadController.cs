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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using PetrpkuWeb.Shared.ViewModels;
using PetrpkuWeb.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UploadController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("files"), DisableRequestSizeLimit]
        public async Task<ActionResult<List<Attachment>>> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var result = new List<Attachment>();
                var dirPath = CreateRandomNameDirectory();

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(dirPath, file.FileName);

                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                        if (extension.StartsWith(".jp") || extension.StartsWith(".png"))
                        {
                            stream.Position = 0;
                            using (Image image = Image.Load(stream))
                            {
                                switch (image.Width)
                                {
                                    case var expression when (image.Width >= 400 && image.Width < 600):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                    case var expression when (image.Width >= 600 && image.Width < 1000):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                    case var expression when (image.Width >= 1000 && image.Width < 1400):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                    case var expression when (image.Width >= 1400 && image.Width < 3000):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                    case var expression when (image.Width >= 3000 && image.Width < 5000):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                    case var expression when (image.Width >= 5000):
                                        image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                                        break;
                                }

                                if (extension.StartsWith(".jp"))
                                {
                                    image.SaveAsJpeg(stream);
                                }
                                else
                                {
                                    image.SaveAsPng(stream);
                                }
                            }
                        }
                    }

                    var attachment = new Attachment()
                    {
                        Name = file.FileName,
                        Length = file.Length,
                        Path = path,
                        Extension = extension
                    };

                    _db.Attachments.Add(attachment);
                    await _db.SaveChangesAsync();

                    result.Add(attachment);
                }
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new FileUploadViewModel() { Extension = ex.Message });
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

            if (attachmentInfo == null)
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

