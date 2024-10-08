﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /*private readonly DbSnrTodoContext _context;

        public NotesController(DbSnrTodoContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }*/

        public readonly INoteRepository _noteRepo;
        public NotesController(INoteRepository repo)
        {
            _noteRepo = repo;
        }

        //GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            try
            {
                return Ok(await _noteRepo.getAllNotesAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            try
            {
                var note = await _noteRepo.getNoteAsync(id);
                return note == null ? NotFound() : Ok(note);
            }
            catch
            {
                return BadRequest();
            }
        }
        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote(NoteModel model)
        {
            try
            {
                var newNoteId = await _noteRepo.postNoteAsync(model);
                var note = await _noteRepo.getNoteAsync(newNoteId);
                return note == null ? NotFound() : Ok(note);

            }
            catch
            {
                return BadRequest();
            }
        }
        //PUT: api/Notes
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, [FromBody] NoteModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }
                await _noteRepo.updateNoteAsync(id, model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        //DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteNote([FromRoute] int id)
        {
            try
            {
                var note = await _noteRepo.getNoteAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                await _noteRepo.deleteNoteAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
