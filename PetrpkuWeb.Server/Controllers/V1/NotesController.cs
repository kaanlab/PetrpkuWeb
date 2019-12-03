using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Shared.Views;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Contracts.V1;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/articles")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ITypeService<Note> _noteTypeService;
        private readonly IMapper _mapper;

        public NotesController(ITypeService<Note> noteTypeService, IMapper mapper)
        {
            _noteTypeService = noteTypeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Note.ALL)]
        public async Task<ActionResult> GetNotes()
        {
            var notes = await _noteTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<NoteAppUserCssTypeView>>(notes));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPost(ApiRoutes.Note.CREATE)]
        public async Task<ActionResult> CreateNote(NoteAppUserCssTypeView noteView)
        {
            if (noteView is null)
                return BadRequest();

            var note = _mapper.Map<Note>(noteView);
            var created = await _noteTypeService.Create(note);
            
           if(created)
                return Ok(_mapper.Map<NoteAppUserCssTypeView>(note));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Note.SHOW + "/{noteId:int}")]
        public async Task<ActionResult> GetNote(int noteId)
        {
            var note = await _noteTypeService.GetOne(noteId);

            if (note is null)
                return NotFound();

            return Ok(_mapper.Map<NoteAppUserCssTypeView>(note));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPut(ApiRoutes.Note.UPDATE + "/{noteViewId:int}")]
        public async Task<ActionResult> UpdateNoteAsync(int noteViewId, NoteAppUserCssTypeView noteView)
        {
            var note = await _noteTypeService.GetOne(noteViewId);

            if (note.NoteId == noteView.NoteId)
            {
                var updatedNote = _mapper.Map(noteView, note);
                var updated = await _noteTypeService.Update(updatedNote);

                if(updated)
                    return Ok(_mapper.Map<NoteAppUserCssTypeView>(updatedNote));
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpDelete(ApiRoutes.Note.DELETE + "/{noteId:int}")]
        public async Task<ActionResult> Delete(int noteId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _noteTypeService.Delete(noteId);

                if(deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}