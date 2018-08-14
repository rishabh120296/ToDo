using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Services
{
    public interface INotesService
    {
        Task<Note> GetNote(int id);
        Task<IEnumerable<Note>> GetNote(Note note);
        Task<Note> AddNote(Note note);
        Task<bool> DeleteNotesByTitle(string title);
        Task<bool> DeleteNoteById(int id);
        Task<bool> EditNote(int id, Note note);
    }
}



