using Candor.Data;
using Candor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candor.Services
{
    public class RatingService
    {
        private readonly Guid _userId;

        public RatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var rating = new Rating()
            {
                OwnerId = _userId,
                IdeaId = model.Title,
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

        public List<IdeaListItem> GetIdeas()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Ideas
                    .Where(n => n.OwnerId == _userId)
                    .Select(n => new IdeaListItem()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        DateCreated = n.DateCreated,
                        AverageRating = n.AverageRating
                    });


                return query.ToList();
            }
        }

        public IdeaDetail GetIdeaById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas.Single(n => n.Id == id && n.OwnerId == _userId);
                var model = new IdeaDetail()
                {
                    IdeaId = idea.Id,
                    Title = idea.Title,
                    Content = idea.Content,
                    DateCreated = idea.DateCreated,
                    LastModified = idea.LastModified,
                    Ratings = idea.Ratings,
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
                var idea = context.Ideas.Single(n => n.Id == model.Id && n.OwnerId == _userId);

                        idea.Id = model.Id,
                        idea.Title = model.Title,
                        idea.Content = model.Content,
                        idea.LastModified = DateTimeOffset.UtcNow,
                        idea.Ratings = model.Ratings,
                        idea.Completed = model.Completed;

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
