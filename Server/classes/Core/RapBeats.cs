#region Using

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Models;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapBeats : RapClass
    {
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        public List<BeatModel> Get()
        {
            var directoryInfo = new DirectoryInfo(new ResourceProvider().GetPath(RapResource.Beats, null));
            var sortedFiles = from f in directoryInfo.EnumerateFiles() orderby f.CreationTime select f;
            return sortedFiles.Select(item => new BeatModel {Name = item.Name}).ToList();
        }
    }
}