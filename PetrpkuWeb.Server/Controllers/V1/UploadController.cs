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
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Server.Models;
using AutoMapper;

namespace PetrpkuWeb.Server.Controllers.V1
{
    [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
    //[Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UploadController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Upload.POSTER), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPoster(IFormFile file)
        {
            try
            {
                var extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().StartsWith(".jp") || extension.ToLower().StartsWith(".png"))
                {
                    var dirPath = UploadPath();
                    var path = Path.Combine(dirPath, file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        path = Path.Combine(dirPath, Path.GetRandomFileName().Substring(0, 6), file.FileName);
                    }

                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using (MagickImage image = new MagickImage(path))
                    {
                        if(image.Width > image.Height)
                        {
                            
                            image.Crop(image.Height, image.Height, Gravity.Center);
                            image.RePage();
                        }
                        else 
                        {
                            image.Crop(image.Width, image.Width, Gravity.Center);
                            image.RePage();
                        }

                        if (image.Width > 300)
                        { 
                            image.Resize(280, 0);
                        }

                        image.Quality = 55;
                        image.Write(path);
                    }

                    return Ok(path);
                }

                return BadRequest();
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost(ApiRoutes.Upload.AVATAR), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadAvatar(IFormFile file)
        {
            try
            {
                var extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().StartsWith(".jp") || extension.ToLower().StartsWith(".png"))
                {
                    var dirPath = UploadPath();
                    var path = Path.Combine(dirPath, file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        path = Path.Combine(dirPath, Path.GetRandomFileName().Substring(0, 6), file.FileName);
                    }

                    await using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using (MagickImage image = new MagickImage(path))
                    {
                        image.AutoOrient();

                        if (image.Width > image.Height)
                        {
                            image.Crop(image.Height, image.Height, Gravity.Center);
                            image.RePage();
                        }
                        else
                        {
                            var n = new MagickGeometry(0, -image.Height / 10, image.Width, image.Width);
                            image.Crop(n,Gravity.Center);
                            image.RePage();
                        }

                        if (image.Width > 300)
                        {
                            image.Resize(240,0);
                        }

                        image.Quality = 60;
                        image.Write(path);

                    }

                    return Ok(path);
                }

                return BadRequest();
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost(ApiRoutes.Upload.FILES), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var attachments = new List<Attachment>();
                var dirPath = UploadPath();

                foreach (var file in files)
                {
                    var isImage = false;
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(dirPath, file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        path = Path.Combine(dirPath, Path.GetRandomFileName().Substring(0, 6), file.FileName);
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
                            if (image.Width > 1920)
                                image.Resize(1920, 0);

                            image.Quality = 55;
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

                    attachments.Add(attachment);
                }

                return Ok(_mapper.Map<List<AttachmentViewModel>>(attachments));
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpPut(ApiRoutes.Upload.UPDATE + "/{attachmentId:int}")]
        public async Task<ActionResult<Attachment>> UpdateAttachment(int attachmentViewModelId, AttachmentViewModel attachmentViewModel)
        {
            if (attachmentViewModelId != attachmentViewModel.AttachmentId)
            {
                return BadRequest();
            }

            var attachment = _mapper.Map<Attachment>(attachmentViewModel);

            _db.Entry(attachment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<AttachmentViewModel>(attachment));
        }

        [HttpDelete(ApiRoutes.Upload.DELETE + "/{attachmentId:int}")]
        public async Task<ActionResult> DeleteAttachment([FromRoute] int attachmentId)
        {
            var attachment = await _db.Attachments.FindAsync(attachmentId);

            if (attachment is null)
            {
                return NotFound();
            }

            _db.Attachments.Remove(attachment);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private string UploadPath()
        {
            var dirPath = Path.Combine("uploadfolder", DateTime.Now.ToString("ddMMyyyy"));

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            return dirPath;
        }
    }
}

