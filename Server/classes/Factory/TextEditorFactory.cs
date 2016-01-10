using CKEditor.NET;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Types;
using YAF.Types;

namespace FreestyleOnline.classes.Factory
{
    public class TextEditorFactory : BaseFactory, IRapConfigFactory<CKEditorControl, CKEditorControl>
    {
        #region Members

        [NotNull]
        private readonly bool _isMobile = RapContextFacade.Current.IsMobile;
        [NotNull]
        private readonly bool _isGuest = RapContextFacade.Current.IsGuest;

        #endregion

        #region Methods

        /// <summary>
        ///     Builds the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public CKEditorControl Build([NotNull] CKEditorControl config)
        {
            var ckEditor = config;
            ckEditor.ResizeEnabled = false;
            if (!this._isMobile)
            {
                ckEditor.Toolbar = "|Bold|Italic|Underline|Strike|-|NumberedList|BulletedList|Outdent|Indent|-|" +
                                   "JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|Link|Unlink" +
                                   "|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord" +
                                   "|-|Find|Replace|SelectAll|-|Image|Table|HorizontalRule|SpecialChar|";
            }
            else
            {
                ckEditor.Toolbar = "|Bold|Italic|Underline|";
            }
            //TODO: removed -|Smiley| until fix icon issue not showing
            ckEditor.Skin = "BootstrapCK-Skin";
            ckEditor.Visible = !this._isGuest;
            return ckEditor;
        }

        #endregion
    }
}