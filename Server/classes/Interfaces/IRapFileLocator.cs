#region Using

using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapFileLocator
    {
        string GetPath(RapResource resource, params object[] args);
    }
}