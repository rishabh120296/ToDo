using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _notesService.AddNote(note);
            return CreatedAtAction("GetNote", new { id = note.ID }, note);
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetNote([FromQuery] string label = "", [FromQuery] bool isPinned = false, [FromQuery] string title = "")
        {
            Labels label1 = new Labels()
            {
                Name = label
            };
            Note note = new Note()
            {
                Labels = new List<Labels> { label1 },
                IsPinned = isPinned,
                Title = title
            };
            var a = await _notesService.GetNote(note);
            return Ok(a);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _notesService.GetNote(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // DELETE: api/Notes
        [HttpDelete]
        public async Task<IActionResult> DeleteNote([FromQuery] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _notesService.DeleteNotesByTitle(title);
            if (note == false)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _notesService.DeleteNoteById(id);
            if (note == false)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
           
            await _notesService.EditNote(id, note);

            return NoContent();
        }

    }
}
