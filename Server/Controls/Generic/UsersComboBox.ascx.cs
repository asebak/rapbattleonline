#region Using

using System;
using System.Linq;
using System.Web.Security;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    public partial class UsersComboBox : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets the user identifier selected.
        /// </summary>
        /// <value>
        ///     The user identifier selected.
        /// </value>
        [CanBeNull]
        public int UserIdSelected
        {
            get { return UserData.GetUserIdFromDisplayName(this.UserComboxBoxList.SelectedItem.Value); }
        }

        /// <summary>
        ///     Gets the user name selected.
        /// </summary>
        /// <value>
        ///     The user name selected.
        /// </value>
        [CanBeNull]
        public string UserNameSelected
        {
            get { return this.UserComboxBoxList.SelectedItem.Value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [is visible].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is visible]; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.UserAutoStore.DataSource = UserMembershipHelper.GetAllUsers()
                .Cast<MembershipUser>().ToList().ConvertAll(x => new[] {x.UserName}).ToArray();
            this.UserAutoStore.DataBind();
            IsVisible = true;
        }

        #endregion

        #region Direct Methods

        /// <summary>
        ///     Handles the user selection.
        /// </summary>
        [DirectMethod]
        public void HandleUserSelection()
        {
            if (this.PageContext.IsAdmin)
            {
                return;
            }
            if (this.UserComboxBoxList.Text == this.PageContext.PageUserName)
            {
                this.UserComboxBoxList.Text = "";
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(350, 60, true, this.Text("COMMON", "COMMON_ERRORSELF"), this.UserComboxBoxList);
            }
        }

        #endregion
    }
}