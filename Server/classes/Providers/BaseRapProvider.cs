#region Using

using System.Web;
using FreestyleOnline.classes.Types;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Providers
{
    public class BaseRapProvider
    {
        #region Properties

        /// <summary>
        /// Gets the rap context.
        /// </summary>
        /// <value>
        /// The rap context.
        /// </value>
        public RapContextFacade RapContext
        {
            get { return RapContextFacade.Current; }
        }

        #endregion
    }
}