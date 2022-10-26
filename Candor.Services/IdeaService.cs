using Candor.Data;
using Candor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
                UserId = _userId,
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

        public IEnumerable<IdeaListItem> GetIdeas()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Ideas
                    .Where(n => n.UserId == _userId)
                    .Select(n => new IdeaListItem()
                    {
                        IdeaId = n.Id,
                        Title = n.Title,
                        DateCreated = n.DateCreated,
                        Completed = n.Completed,
                        IsEditable = _userId == n.UserId
                    });


            return query.ToArray();
            }
        }

        public IEnumerable<RatingListItem> GetRatingsByIdeaId(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas
                    .Include(t => t.IdeaId)
                    .Include(t => t.Id)
                    .Include(t => t.UserId)
                    .Include(t => t.Title)
                    .Include(t => t.Content)
                    .Include(t => t.DateCreated)
                    .SingleOrDefault(t => t.IdeaId == id);

                if (idea is null)
                {
                    return null;
                }

                var model = new IdeaDetail()
                {
                    IdeaId  = idea.Id,
                    Title = idea.Title,
                    Content = idea.Content,
                    DateCreated = idea.DateCreated,
                    IsEditable = _userId == idea.UserId,
                    Ratings = idea.Ratings
                        .Select(rating => new RatingListItem()
                        {
                            RatingId = rating.Id,
                            //UserName = GetUserName(context, post),
                            IdeaId = rating.IdeaId,
                            RatingScore = rating.RatingScore,
                            Comment = rating.Comment,
                            IsEditable = rating.UserId == _userId
                        }).ToList()
                };

                return (IEnumerable<RatingListItem>)model;
            }
        }

        public IdeaDetail GetIdeaById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas
                    .Include(n => n.Ratings)
                    .SingleOrDefault(n => n.Id == id && n.UserId == _userId);

                if (idea is null)
                {
                    return null;
                }

                var model = new IdeaDetail()
                {
                    UserName = context.Users.Find(idea.UserId
                            .ToString()).UserName,
                    IdeaId = idea.Id,
                    Title = idea.Title,
                    Content = idea.Content,
                    DateCreated = idea.DateCreated,
                    LastModified = idea.LastModified,
                    AverageRating = idea.AverageRating,
                    Completed = idea.Completed,
                    Ratings = idea.Ratings
                        .OrderByDescending(Ratings => Ratings.DateCreated)
                        .Select(rating => new RatingListItem()
                        {
                            RatingId = rating.Id,
                            RatingScore = rating.RatingScore,
                            IdeaId = rating.IdeaId,
                            Comment = rating.Comment,
                            UserName = context.Users.Find(rating.UserId
                            .ToString()).UserName,
                            IsEditable = rating.UserId == _userId
                    
                        }).ToList()
                    
                };

                return model;
            }
        }

        public bool UpdateIdea(IdeaEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var idea = context.Ideas.Single(n => n.Id == model.IdeaId && n.UserId == _userId);

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
                var idea = context.Ideas.Single(n => n.Id == id && n.UserId == _userId);
                context.Ideas.Remove(idea);
                return context.SaveChanges() == 1;
            }
        }
    }
}
