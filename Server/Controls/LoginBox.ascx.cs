#region Using

using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Secruity;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.EventProxies;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class LoginBox : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
        }
        
        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Authenticate event of the Login1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AuthenticateEventArgs" /> instance containing the event data.</param>
        protected void Login1_Authenticate([NotNull] object sender, [NotNull] AuthenticateEventArgs e)
        {
            e.Authenticated = false;

            var userData = new UserAuthentication(this.Login1.UserName, this.Login1.Password);
            var realUserName = userData.IsAuthenticated();

            if (realUserName.IsNotSet())
            {
                return;
            }

            this.Login1.UserName = realUserName;
            e.Authenticated = true;
        }

        /// <summary>
        ///     Handles the LoginError event of the Login1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Login1_LoginError([NotNull] object sender, [NotNull] EventArgs e)
        {
            bool emptyFields = false;

            var userName = this.Login1.UserName;
            var password = this.Login1.Password;

            if (userName.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("REGISTER", "NEED_USERNAME"));
                emptyFields = true;
            }

            if (password.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("REGISTER", "NEED_PASSWORD"));
                emptyFields = true;
            }

            if (!emptyFields)
            {
                this.PageContext.AddLoadMessage(this.Login1.FailureText);
            }
        }

        /// <summary>
        ///     Handles the LoggedIn event of the Login1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Login1_LoggedIn([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<IRaiseEvent>().Raise(new SuccessfulUserLoginEvent(this.PageContext.PageUserID));

            LegacyDb.user_update_single_sign_on_status(this.PageContext.PageUserID, AuthService.none);
        }

        /// <summary>
        ///     Handles the Click event of the RegisterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RegisterButton_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            Response.Redirect("~/forum/rules");
        }

        #endregion
    }
}