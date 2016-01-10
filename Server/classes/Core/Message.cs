#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Models;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.RealTime.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types.Flags;

#endregion

namespace FreestyleOnline.classes.Core
{
    public abstract class Message : RapClass
    {
        private static readonly MessageNotifier _notifer = new MessageNotifier();

        #region Methods

        /// <summary>
        ///     Sends the pm message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <param name="messageId">The message identifier.</param>
        public static void SendPmMessage(int? sender, int? receiver, string title, string body, int messageId = -1)
        {
            Contract.Requires<NullReferenceException>(sender != null);
            Contract.Requires<NullReferenceException>(receiver != null);
            Contract.Requires<NullReferenceException>(!string.IsNullOrEmpty(title));
            Contract.Requires<NullReferenceException>(!string.IsNullOrEmpty(body));
            var messageFlags = new MessageFlags
            {
                IsHtml = true,
                IsBBCode = true
            };

            LegacyDb.pmessage_save(
                sender,
                receiver,
                title,
                body,
                messageFlags.BitValue,
                messageId);
            try
            {
                _notifer.NotifyPrivateMessage((int) receiver, (int) sender, "You received a private message now.");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Gets the total inbox messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static int GetTotalUnreadInboxMessages(int userId)
        {
            return Db.get_pm_count(userId);
        }

        /// <summary>
        ///     Gets the pm list.
        /// </summary>
        public static List<PMModel> GetPMList(int userId)
        {
            var pmListDataTable = LegacyDb.pmessage_list(userId, null, null);
            var usersmessages = (from r in pmListDataTable.AsEnumerable()
                select new PMModel
                {
                    MessageId = Convert.ToInt32(r["UserPMessageID"]),
                    UserId = Convert.ToInt32(r["FromUserID"]),
                    Subject = Convert.ToString(r["Subject"]),
                    SentBy = UserMembershipHelper.GetDisplayNameFromID(Convert.ToInt32(r["FromUserID"])),
                    To = UserMembershipHelper.GetDisplayNameFromID(Convert.ToInt32(r["ToUserID"])),
                    DateSent = (DateTime.Now - Convert.ToDateTime(r["Created"])).Days,
                    Details = Convert.ToString(r["Body"]),
                    IsArchived = Convert.ToBoolean(r["IsArchived"]),
                    IsRead = Convert.ToBoolean(r["IsRead"]),
                    IsReply = Convert.ToBoolean(r["IsReply"]),
                    IsInOutBox = Convert.ToBoolean(r["IsInOutbox"]),
                    IsDeleted = Convert.ToBoolean(r["IsDeleted"])
                }).ToList();
            return usersmessages.Where(m => !m.IsDeleted && !m.IsArchived).ToList();
        }

        /// <summary>
        ///     Deletes the pm.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        public static void DeletePM(int messageId)
        {
            LegacyDb.pmessage_delete(messageId);
        }

        /// <summary>
        ///     Marks as read.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        public static void MarkAsRead(int messageId)
        {
            LegacyDb.pmessage_markread(messageId);
        }

        /// <summary>
        ///     Deletes the message.
        /// </summary>
        /// <param name="id">The identifier of the database table source</param>
        /// <param name="userId">The user identifier.</param>
        public abstract void DeleteMessage(int id, int userId);

        /// <summary>
        ///     Gets the message data source.
        /// </summary>
        /// <returns></returns>
        public abstract DataTable GetMessageDataSource();

        #endregion
    }
}