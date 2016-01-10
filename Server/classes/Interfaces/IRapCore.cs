#region Using

using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapCore
    {
        T GetCore<T>() where T : RapClass, new();
    }
}