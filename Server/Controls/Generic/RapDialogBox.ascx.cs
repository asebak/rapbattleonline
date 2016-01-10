#region Using

using System;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Controls;
using YAF.Core;
using YAF.Core.Services;
using YAF.Modules;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using PopupDialogNotification = FreestyleOnline.classes.Types.Modules.PopupDialogNotification;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    public partial class RapDialogBox : RapUserControl
    {
        #region Members
        /// <summary>
        ///   The _error popup.
        /// </summary>
        private classes.Types.Modules.PopupDialogNotification _errorPopup;
        #endregion

        #region Events

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.RegisterLoadString();
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.AddErrorPopup();
        }


        #endregion

        #region Methods

        /// <summary>
        /// The register load string.
        /// </summary>
        protected void RegisterLoadString()
        {
            var message = this.PageContext.LoadMessage.GetMessage();

            if (message == null)
            {
                return;
            }
            message.Message = message.Message.ToJsString();

            if (string.IsNullOrEmpty(message.Message))
            {
                return;
            }
            this.PageContext.PageElements.RegisterJsBlockStartup(
                this.NotificationPlace.Page,
                "modalNotification",
                "var fpModal = function() {{ {2}('{0}', '{1}'); Sys.Application.remove_load(fpModal); }}; Sys.Application.add_load(fpModal);"
                    .FormatWith(message.Message, message.MessageType.ToString().ToLower(), this._errorPopup.ShowModalFunction));
        }

        /// <summary>
        /// Sets up the Modal Error Popup Dialog
        /// </summary>
        private void AddErrorPopup()
        {
            try
            {
                if (this.NotificationPlace.FindControl("YafForumPageErrorPopup1") == null)
                {
                    this._errorPopup = new PopupDialogNotification
                    {
                        ID = "YafForumPageErrorPopup1",
                        Title = this.GetText("COMMON", "MODAL_NOTIFICATION_HEADER")
                    };
                    this.NotificationPlace.Controls.Add(this._errorPopup);
                }
                else
                {
                    this._errorPopup =
                        (PopupDialogNotification) this.NotificationPlace.FindControl("YafForumPageErrorPopup1");
                    this._errorPopup.Title = this.GetText("COMMON", "MODAL_NOTIFICATION_HEADER");
                }
            }
            catch (Exception e)
            {
                
            }
        }

        #endregion
    }
}