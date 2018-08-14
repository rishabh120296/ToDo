using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Services
{
    public class NotesService : INotesService
    {
        private readonly NotesContext _context = null;

        public NotesService(IOptions<Settings> settings)
        {
            _context = new NotesContext(settings);
        }

        public async Task<Note> GetNote(int id)
        {
            return await _context.Notes.Find(note => note.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Note> AddNote(Note note)
        {
            await _context.Notes.InsertOneAsync(note);
            return note;
        }

        public async Task<bool> DeleteNoteById(int id)
        {
            DeleteResult actionResult = await _context.Notes.DeleteOneAsync(Builders<Note>.Filter.Eq("ID", id));
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> DeleteNotesByTitle(string title)
        {
            DeleteResult actionResult = await _context.Notes.DeleteManyAsync(Builders<Note>.Filter.Eq("Title", title));
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> EditNote(int id, Note note)
        {
            ReplaceOneResult actionResult = await _context.Notes.
                ReplaceOneAsync(n => n.ID.Equals(id), note, new UpdateOptions { IsUpsert = true });
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;

        }

        public async Task<IEnumerable<Note>> GetNote(Note note)
        {

            var query = _context.Notes.Find(m =>
                (String.IsNullOrEmpty(note.Title) || m.Title == note.Title)
                && (string.IsNullOrEmpty(note.Labels[0].Name) || m.Labels.Any(b => b.Name == note.Labels[0].Name))
                && (!note.IsPinned || m.IsPinned == note.IsPinned));
            return await query.ToListAsync();
        }
    }
}
