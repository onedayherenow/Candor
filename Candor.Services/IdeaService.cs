using Candor.Data;
using Candor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Services
{
    public class IdeaService
    {
        private readonly Guid _userId;

        public IdeaService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateIdea(IdeaCreate model)
        {
            var idea = new Idea()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                DateCreated = DateTimeOffset.UtcNow,
                Ratings = model.Ratings,
                AverageRating = model.AverageRating,
                Completed = model.Completed
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Ideas.Add(idea);
                return context.SaveChanges() == 1;
            }
        }

        public List<IdeaListItem> GetNotes()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Ideas
                    .Where(n => n.OwnerId == _userId)
                    .Select(n => new IdeaListItem()
                    {
                        IdeaId = n.IdeaId,
                        Title = n.Title,
                        IsStarred = n.IsStarred,
                        CreatedUtc = n.CreatedUtc
                    });

                return query.ToList();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var note = context.Notes.Single(n => n.Id == id && n.OwnerId == _userId);
                var model = new NoteDetail()
                {
                    NoteId = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    IsStarred = note.IsStarred,
                    CreatedUtc = note.CreatedUtc,
                    ModifiedUtc = note.ModifiedUtc
                };

                return model;
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var note = context.Notes.Single(n => n.Id == model.NoteId && n.OwnerId == _userId);
                note.Title = model.Title;
                note.Content = model.Content;
                note.IsStarred = model.IsStarred;
                note.ModifiedUtc = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var note = context.Notes.Single(n => n.Id == id && n.OwnerId == _userId);
                context.Notes.Remove(note);
                return context.SaveChanges() == 1;
            }
        }
    }
}
