#region Using

using System.Data;
using System.Diagnostics.Contracts;
using FreestyleOnline.classes.Database;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class ContactMessage : Message
    {
        /// <summary>
        ///     Deletes the message.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public override void DeleteMessage(int id, int userId)
        {
            Db.delete_contactmsg(id);
        }

        /// <summary>
        ///     Submits the contact form.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="titleHeader">The title header.</param>
        /// <param name="messageContent">Content of the message.</param>
        public void SubmitContactForm(int userId, string titleHeader, string messageContent)
        {
            Contract.Requires(!string.IsNullOrEmpty(titleHeader));
            Contract.Requires(!string.IsNullOrEmpty(messageContent));
            Db.submit_contactmsg(userId, titleHeader, messageContent);
        }

        /// <summary>
        ///     Gets the message data source.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetMessageDataSource()
        {
            return Db.get_all_contactmsgs();
        }
    }
}