#region Using

using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Common.Models;
using FreestyleOnline.classes.Core;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class PrivateMessagesController : ApiController
    {
        #region API

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                Message.GetPMList(UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name)));
        }

        [Authorize]
        [HttpDelete]
        [ActionName("Delete")]
        public HttpResponseMessage Delete(int id)
        {
            if (this.FindMessageId(id))
            {
                Message.DeletePM(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Authorize]
        [HttpPut]
        [ActionName("Read")]
        public HttpResponseMessage Put(int id)
        {
            if (this.FindMessageId(id))
            {
                Message.MarkAsRead(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Post(PMModel message)
        {
            var to = UserData.GetUserIdFromDisplayName(message.To);
            var from = UserData.GetUserIdFromDisplayName(message.SentBy);
            //new message
            if (!message.Subject.StartsWith("Re: "))
            {
                Message.SendPmMessage(from, to, message.Subject, message.Details);
            }
                //reply
            else
            {
                Message.SendPmMessage(from, to, message.Subject, message.Details, message.MessageId);
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Finds the message identifier.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <returns></returns>
        private bool FindMessageId(int messageId)
        {
            //for secruity to make sure this message belongs to the caller
            var userId = UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name);
            return Message.GetPMList(userId).Exists(m => m.MessageId == messageId);
        }

        #endregion
    }
}