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
    public class RatingService
    {
        private readonly Guid _userId;

        public RatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(RatingCreate model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var idea = ctx.Ideas.Single(t => t.Id == id);

                var ideaIdValue = idea.Id;

                if (idea == null)
                {
                    return false;
                }

                var rating = new Rating()
                {
                    IdeaId = ideaIdValue,
                    UserId = _userId,
                    RatingScore = model.RatingScore,
                    Comment = model.Comment,
                    DateCreated = DateTimeOffset.Now
                };

                ctx.Ratings.Add(rating);
                return ctx.SaveChanges() == 1;
            }
        }


        //public ICollection<RatingListItem> GetRatingsByIdeaId(int id)
        //{
        //    using (var context = ApplicationDbContext.Create())
        //    {

        //        var idea = context.Ideas.Where(t => t.Id == id)

        //            .Include(t => t.Ratings)
        //            .FirstOrDefault(t => t.Id == id);


        //        if (idea is null)
        //        {
        //            return null;
        //        }

        //        var rating = context.Ratings.FirstOrDefault(t => t.Id == idea.Id);

        //        var model = new RatingListItem()
        //        {

        //            RatingId = rating.Id,
        //            //UserName = GetUserName(context, post),
        //            IdeaId = idea.Id,
        //            RatingScore = rating.RatingScore,
        //            Comment = rating.Comment,
        //            IsEditable = rating.UserId == _userId
        //        };

        //        return (ICollection<RatingListItem>)model;
        //    }
        //}

        public ICollection<RatingListItem> GetRatingsByIdeaId(int id)
        {
            

            using (var context = ApplicationDbContext.Create())
           
            {
                var idea = context.Ideas.Single(n => n.Id == id);

            //public IEnumerable<Waiver> Waivers => context.Waivers.Include(o => o.Office);


        //.Include(t => t.Ratings)
        //.FirstOrDefault(t => t.Id == id);


        //below is for the second return
        ICollection <RatingListItem> rates = (ICollection<RatingListItem>)idea.Ratings;

                if (idea is null)
                {
                    return null;
                }

                var model = new IdeaDetail()
                {
                    //IdeaId = idea.Id,
                    //Title = idea.Title,
                    //Content = idea.Content,
                    //DateCreated = idea.DateCreated,
                    //IsEditable = _userId == idea.UserId,
                    Ratings = idea.Ratings
                        .Select(rating => new RatingListItem()
                        {
                            RatingId = rating.Id,
                            //UserName = GetUserName(context, post),
                            IdeaId = idea.IdeaId,
                            RatingScore = rating.RatingScore,
                            Comment = rating.Comment,
                            IsEditable = rating.UserId == _userId
                        }).ToList()
                };

                var ratingIndex = model.Ratings.ToList();
             
                return ratingIndex;
                //return rates.ToArray();
                
            }
        }


        //public IEnumerable<RatingListItem> GetRatingsByIdeaId(int id)
        //{
        //    using (var context = ApplicationDbContext.Create())
        //    {
        //        var rating = context.Ratings
        //            .Include(t => t.IdeaId)
        //            .Include(t => t.Id)
        //            .Include(t => t.UserId)
        //            .Include(t => t.RatingScore)
        //            .Include(t => t.Comment)
        //            .SingleOrDefault(t => t.IdeaId == id);

        //        if (rating is null)
        //        {
        //            return null;
        //        }

        //        var model = new RatingListItem()
        //        {
        //            RatingId = rating.Id,
        //            IdeaId = rating.IdeaId,
        //            RatingScore = rating.RatingScore,
        //            Comment = rating.Comment,
        //            IsEditable = _userId == rating.UserId,
        //            Ratings = idea.Ratings
        //                .Select(item => new RatingListItem()
        //                {
        //                    PostId = item.Id,
        //                    UserName = GetUserName(context, post),
        //                    Content = post.Content,
        //                    CreatedUtc = post.CreatedUtc,
        //                    ModifiedUtc = post.ModifiedUtc,
        //                    IsEditable = post.UserId == _userId
        //                }).ToList()
        //        };

        //        return model;
        //    }
        //}


        //public IEnumerable<RatingListItem> GetRatingsByIdeaId(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        //var rating = ctx.Ratings.Single(n => n.Id == id && n.UserId == _userId);
        //        var ideaa = ctx.Ideas.Single(n => n.Id == id);

        //        var idea = ctx.Ideas.Find(ideaa.Id);

        //        var query =
        //            ctx
        //                .Ratings
        //                .Where(e => e.IdeaId == id && e.UserId == _userId)
        //                .Select(
        //                    e =>
        //                        new RatingListItem
        //                        {
        //                            //UserName = GetUserName(ctx, query),
        //                            IdeaId = idea.Id,
        //                            RatingId = e.Id,
        //                            RatingScore = e.RatingScore,
        //                            Comment = e.Comment,
        //                            IsEditable = _userId == e.UserId
        //                        }
        //                );
        //        return query.ToArray();
        //    }
        //}




        //public IEnumerable<RatingListItem> GetRatingsByIdeaId(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        //var rating = ctx.Ratings.Single(n => n.Id == id && n.UserId == _userId);
        //        //var ideaa = ctx.Ideas.FirstOrDefault(n => n.Id == id);

        //        //var idea = ctx.Ideas.Find(ideaa.Id);

        //        var idea = ctx.Ideas
        //       .Include(n => n.Ratings)
        //       .FirstOrDefault(n => n.Id == id && n.UserId == _userId);

        //        if (idea is null)
        //        {
        //            return null;
        //        }

        //        var query =
        //            ctx
        //                .Ratings
        //                .Where(e => e.IdeaId == id && e.UserId == _userId)
        //                .Select(
        //                    e =>
        //                        new RatingListItem
        //                        {
        //                            //UserName = GetUserName(ctx, query),
        //                            IdeaId = idea.IdeaId,
        //                            RatingId = e.Id,
        //                            RatingScore = e.RatingScore,
        //                            Comment = e.Comment,
        //                            IsEditable = _userId == e.UserId
        //                        }
        //                );
        //        return query.ToArray();
        //    }
        //}

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
