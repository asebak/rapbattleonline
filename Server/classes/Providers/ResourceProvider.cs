#region Using

using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.classes.Providers
{
    public class ResourceProvider : BaseRapProvider, IRapFileLocator
    {
        #region Methods

        /// <summary>
        ///     Gets the path.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public string GetPath([NotNull] RapResource resource, [CanBeNull] params object[] args)
        {
            var path = this.RapContext.MapPath;
            switch (resource)
            {
                case RapResource.Languages:
                    return "rapbattleonline/english.xml";
                case RapResource.ForumIcons:
                    return path("~/forum/themes/BlackGrey/{0}".FormatWith(args));
                case RapResource.MusicTracks:
                    return args.Length > 0
                        ? path("~/Uploads/MusicTracks/{0}".FormatWith(args))
                        : path("~/Uploads/MusicTracks/");
                case RapResource.MusicTracksPictures:
                    return args.Length > 0
                        ? path("~/Uploads/MusicTracksPictures/{0}".FormatWith(args))
                        : path("~/Uploads/MusicTracksPictures/");
                case RapResource.RapBattleAudio:
                    return args.Length > 0
                        ? path("~/Uploads/RapBattleAudio/{0}".FormatWith(args))
                        : path("~/Uploads/RapBattleAudio/");
                case RapResource.Beats:
                    return args.Length > 0
                        ? path("~/Uploads/Beats/{0}".FormatWith(args))
                        : path("~/Uploads/Beats/");
                case RapResource.Exceptions:
                    return path("~/Resources/Exceptions.xml");
                case RapResource.HoodPictures:
                    return args.Length > 0
                        ? path("~/Uploads/HoodPictures/{0}".FormatWith(args))
                        : path("~/Uploads/HoodPictures/");
                case RapResource.HeaderPictures:
                    return args.Length > 0
                        ? path("~/Uploads/ProfilePictures/{0}".FormatWith(args))
                        : path("~/Uploads/ProfilePictures/");
                case RapResource.AudioVerses:
                    return args.Length > 0
                        ? path("~/Uploads/AudioVerses/{0}".FormatWith(args))
                        : path("~/Uploads/AudioVerses/");
                default:
                    return null;
            }
        }

        public string GetClientPath([NotNull] RapResource resource, [CanBeNull] params object[] args)
        {
            switch (resource)
            {
                case RapResource.Languages:
                    return "rapbattleonline/english.xml";
                case RapResource.MusicTracksPictures:
                    return ("/Uploads/MusicTracksPictures/{0}".FormatWith(args));
                case RapResource.HoodPictures:
                    return ("/Uploads/HoodPictures/{0}".FormatWith(args));
                case RapResource.HeaderPictures:
                    return ("/Uploads/ProfilePictures/{0}".FormatWith(args));
                default:
                    return null;
            }
        }

        #endregion
    }
}