using Candor.Models;
using Candor.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace Candor.Web.Controllers.WebApi
{
    [Authorize]
    [RoutePrefix("api/Idea")]
    public class IdeaController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new IdeaService(userId);
            var detail = service.GetIdeaById(noteId);

            var updated = new IdeaEdit()
            {
                IdeaId = detail.IdeaId,
                Title = detail.Title,
                Content = detail.Content,
                LastModified = DateTimeOffset.UtcNow,
                Completed = newState
            };

            return service.UpdateIdea(updated);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id)
        {
            return SetStarState(id, true);
        }

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id)
        {
            return SetStarState(id, false);
        }
    }
}
