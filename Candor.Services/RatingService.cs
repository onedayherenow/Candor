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
                IdeaId = model.IdeaId,
                RatingScore = model.RatingScore,
                Comment = model.Comment,
                DateCreated = DateTimeOffset.UtcNow,
            };

            using (var context = ApplicationDbContext.Create())
            {
                context.Ratings.Add(rating);
                return context.SaveChanges() == 1;
            }
        }

        public List<RatingListItem> GetRatings()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var query = context.Ratings
                    .Where(n => n.OwnerId == _userId)
                    .Select(n => new RatingListItem()
                    {
                        RatingId = n.Id,
                        RatingScore = n.RatingScore,
                        Comment = n.Comment
                    }) ;


                return query.ToList();
            }
        }

        public RatingDetail GetRatingById(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == id && n.OwnerId == _userId);
                var model = new RatingDetail()
                {
                    RatingId = rating.Id,
                    IdeaId = rating.Id,
                    RatingScore = rating.RatingScore,
                    Comment = rating.Comment
                };

                return model;
            }
        }

        public bool UpdateRating(RatingEdit model)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == model.RatingId && n.OwnerId == _userId);

                    rating.RatingScore = model.RatingScore;
                    rating.Comment = model.Comment;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var rating = context.Ratings.Single(n => n.Id == id && n.OwnerId == _userId);
                context.Ratings.Remove(rating);
                return context.SaveChanges() == 1;
            }
        }
    }
}
