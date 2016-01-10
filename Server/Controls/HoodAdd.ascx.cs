#region Using

using System;
using System.IO;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class HoodAdd : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.CreateHood.OnClientClick += "return handlepostback({0});".FormatWith(this.CreateHood.ClientID);
        }

        /// <summary>
        /// Validates the picture upload.
        /// </summary>
        /// <returns></returns>
        private bool ValidatePictureUpload()
        {
            if (this.PictureUpload.HasFile)
            {
                if (this.PictureUpload.PostedFile.ContentType.Contains("image"))
                {
                    if (this.PictureUpload.PostedFile.ContentLength < 102400)
                    {
                        return true;
                    }
                    this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_PICTUREERROR_SIZE").FormatWith("100 Kbs"));
                    return false;
                }
                this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_PICTUREERROR_NONE"));
                return false;
            }
            this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_PICTUREERROR_NONE"));
            return false;
        }

        #endregion

        #region Event Handlers
        /// <summary>
        ///     Handles the Click event of the CreateHood control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateHood_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var hood = this.GetCore<HoodData>();
            hood.Name = this.NameTxtBox.Value;
            hood.Details = this.DescriptionTxtBox.Value;
            hood.IsPublic = this.Privacy.CheckedItems[0].InputValue == "1";
            if (!hood.ValidHood(hood))
            {
                this.PageContext.AddLoadMessage(this.Text("HOOD", "HOOD_NAMEERROR"), MessageTypes.Error);
                return;
            }
            hood.Picture = new Guid() + Path.GetFileName(this.PictureUpload.FileName);
            if (!this.ValidatePictureUpload())
            {
                return;
            }
            //TODO Optimize no need to get all the users hoods only hood ids
            if (this.GetCore<UserData>().GetUsersHoods(this.PageContext.PageUserID).Count >= 5)
            {
                this.PageContext.AddLoadMessage(this.Text("HOOD", "HOOD_MAX").FormatWith(5), MessageTypes.Error);
                return;
            }
            this.PictureUpload.PostedFile.SaveAs(this.GetService<ResourceProvider>()
                .GetPath(RapResource.HoodPictures, hood.Picture));
            hood.AddHood(hood, this.PageContext.PageUserID);
            this.AddLoadMessageSession(this.Text("HOOD", "HOOD_ADDSUCESS"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}