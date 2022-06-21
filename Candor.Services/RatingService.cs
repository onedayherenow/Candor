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
            using (var ctx = new ApplicationDbContext())
            {
                var idea = ctx.Ideas.Find(model.IdeaId);

                if (idea == null)
                {
                    return false;
                }

                var rating = new Rating()
                {
                    IdeaId = idea.Id,
                    UserId = _userId,
                    RatingScore = model.RatingScore,
                    Comment = model.Comment,
                    DateCreated = DateTimeOffset.Now
                };

                ctx.Ratings.Add(rating);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RatingListItem> GetRatingsByIdeaId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var rating = ctx.Ratings.Single(n => n.Id == id && n.UserId == _userId);
                var ideaa = ctx.Ideas.FirstOrDefault(n => n.Id == id);

                var idea = ctx.Ideas.Find(ideaa.Id);

                var query =
                    ctx
                        .Ratings
                        .Where(e => e.IdeaId == id && e.UserId == _userId)
                        .Select(
                            e =>
                                new RatingListItem
                                {
                                    //UserName = GetUserName(ctx, query),
                                    IdeaId = ideaa.IdeaId,
                                    RatingId = e.Id,
                                    RatingScore = e.RatingScore,
                                    Comment = e.Comment,
                                    IsEditable = _userId == e.UserId
                                }
                        );
                return query.ToArray();
            }
        }

        public RatingDetail GetRatingById(int? id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == id && n.UserId == _userId);
                var model = new RatingDetail()
                {
                    RatingId = rating.Id,
                    IdeaId = rating.Id,
                    RatingScore = rating.RatingScore,
                    Comment = rating.Comment,
                    UserName = GetUserName(context, rating),
                    IsEditable = rating.UserId == _userId

                };

                return model;
            }
        }

        public bool UpdateRating(RatingEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == model.RatingId && n.UserId == _userId);

                    rating.RatingScore = model.RatingScore;
                    rating.Comment = model.Comment;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == id && n.UserId == _userId);
                context.Ratings.Remove(rating);
                return context.SaveChanges() == 1;
            }
        }

        private string GetUserName(ApplicationDbContext context, Rating rating)
        {
            string userId = rating.UserId.ToString();
            var user = context.Users.Find(userId);
            return user.UserName;
        }
    }
}
