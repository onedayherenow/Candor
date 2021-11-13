using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Candor.Models;
using Microsoft.AspNet.Identity;
using Candor.Services;

namespace Candor.Controllers
{
    [Authorize]
    public class IdeaController : Controller
    {
		private IdeaService CreateIdeaService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new IdeaService(userId);
        }

        // GET: Note
        public ActionResult Index()
        {
            var service = CreateIdeaService();
            var ideas = service.GetIdeas();
            return View(ideas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdeaCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateIdeaService();
                if (service.CreateIdea(model))
                {
                    TempData["SaveResult"] = "Your idea was created.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Idea could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateIdeaService();
            var detail = service.GetIdeaById(id);
            return View(detail);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateIdeaService();
            var detail = service.GetByIdeaId(id);
            var model = new IdeaEdit()
            {
                Id = detail.Id,
                Title = detail.Title,
                Content = detail.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IdeaEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateIdeaService();

            if (service.UpdateIdea(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your idea could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var service = CreateIdeaService();
            var model = service.GetIdeaById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateIdeaService();
            service.DeleteIdea(id);
            TempData["SaveResult"] = "Your idea was deleted";
            return RedirectToAction("Index");
        }
    }
}