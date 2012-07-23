using System;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using Raven.Client;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    [BasicAuthorize]
    public class CommentsController : FatApiController
    {        
        // C'tor
        public CommentsController(IDocumentSession documentSession, Func<IPrincipal> user)
            : base(documentSession, user)
        { }

        // POST /Api/v1/LogBooks/33/Entries/1/Comments
        public HttpResponseMessage Post([FromUri]int? logBookId, [FromUri]int? entryId, CommentInput commentInput)
        {            
            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            var comment = new Comment<Entry>();
            commentInput.MapToInstance(comment);
            entry.AddComment(comment);

            base.RavenSession.Store(logBook);

            var commentView = comment.MapTo<LogBookView.CommentView>();

            return Created(commentView);
        }


        // PUT /Api/v1/LogBooks/33/Entries/13/Comments/7
        public HttpResponseMessage Put([FromUri]int? logBookId, [FromUri]int? entryId, [FromUri]int? commentId, CommentInput commentInput)
        {
            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            var comment = entry.GetComments()
                               .SingleOrDefault(x=> x.Id == commentId);

            if (comment == null)
                return NotFound();

            commentInput.MapToInstance(comment);
            
            var commentView = comment.MapTo<LogBookView.CommentView>();

            return Created(commentView);
        }


        // DELETE /Api/v1/LogBooks/5/Entries/27/Comments/2
        public HttpResponseMessage Delete(int logBookId, int entryId, int commentId)
        {
            if (entryId <= 0)
                return BadRequest("Entry Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();
                                                
            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            var comment = entry.GetComments()
                               .SingleOrDefault(x => x.Id == commentId);

            if (comment == null)
                return NotFound();

            entry.RemoveComment(comment);

            return Ok();
        }
    }
}
