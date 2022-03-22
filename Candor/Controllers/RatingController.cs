using Candor.Models;
using Candor.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace Candor.Controllers
{
    [System.Web.Http.Authorize]
    public class RatingController : Controller
    {
        private RatingService CreateRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new RatingService(userId);
        }


        // GET:  Rating/Create
        public ActionResult Create(int? id)
        {
            //if (id is null)
            //{
            //    return HttpNotFound();
            //}

            var model = new RatingCreate()
            {
                IdeaId = id.Value
            };
            return View(model);
        }

        // POST:  Rating/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RatingCreate model)
        {

            if (ModelState.IsValid)
            {
                var service = CreateRatingService();
                if (service.CreateRating(model))
                {
                    TempData["SaveResult"] = "Your rating was created.";
                    return RedirectToAction("Details", "Idea", new { Id = model.IdeaId});
                }
            }
            ModelState.AddModelError("", "Rating could not be created.");
            return View(model);
        }

        // GET : Rating/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var service = CreateRatingService();
            var detail = service.GetRatingById(id);
            var model = new RatingEdit()
            {
                RatingId = detail.RatingId,
                RatingScore = detail.RatingScore,
                Comment = detail.Comment
            };

            return View(model);
        }

        // GET : Rating/Details/{id}
        public ActionResult Details(int? id)
        {
            var service = CreateRatingService();
            var detail = service.GetRatingById(id);
            return View(detail);
        }

        
        // GET: api/Rating/5
        /// <summary>
        /// Gets all the ratings for a specific idea
        /// </summary>
        /// <returns>RatingListItem</returns>
        public ActionResult Get(int id)
        {
            RatingService ratingService = CreateRatingService();
            var ratings = ratingService.GetRatingsByIdeaId(id);
            return View(ratings);
        }
       

        //POST : Rating/Edit/{id}
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RatingEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (model.RatingId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRatingService();

            if (service.UpdateRating(model))
            {
                TempData["SaveResult"] = "Your rating was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your rating could not be updated.");
            return View(model);
        }

        // GET : Rating/Delete/{id}
        public ActionResult Delete(int id)
        {
            var service = CreateRatingService();
            var detail = service.GetRatingById(id);
            return View(detail);
        }

        // POST : Rating/Delete/{id}
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRatingService();
            service.DeleteRating(id);
            TempData["SaveResult"] = "Your rating was deleted";
            return RedirectToAction("Index");
        }
    }
}