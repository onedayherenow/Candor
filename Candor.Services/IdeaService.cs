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
                Completed = model.Completed
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Ideas.Add(idea);
                return context.SaveChanges() == 1;
            }
        }

        public List<IdeaListItem> GetIdeas()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Ideas
                    .Where(n => n.OwnerId == _userId)
                    .Select(n => new IdeaListItem()
                    {
                        IdeaId = n.Id,
                        Title = n.Title,
                        DateCreated = n.DateCreated,
                        AverageRating = n.AverageRating,
                        Completed = n.Completed
            });


            return query.ToList();
            }
        }

        public IdeaDetail GetIdeaById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas
                    .SingleOrDefault(n => n.Id == id && n.OwnerId == _userId);

                var model = new IdeaDetail()
                {
                    IdeaId = idea.Id,
                    Title = idea.Title,
                    Content = idea.Content,
                    DateCreated = idea.DateCreated,
                    LastModified = idea.LastModified,
                    AverageRating = idea.AverageRating,
                    Completed = idea.Completed
                };

                return model;
            }
        }

        public bool UpdateIdea(IdeaEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas.Single(n => n.Id == model.IdeaId && n.OwnerId == _userId);

                idea.Title = model.Title;
                idea.Content = model.Content;
                idea.Completed = model.Completed;
                idea.LastModified = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteIdea(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas.Single(n => n.Id == id && n.OwnerId == _userId);
                context.Ideas.Remove(idea);
                return context.SaveChanges() == 1;
            }
        }
    }
}
