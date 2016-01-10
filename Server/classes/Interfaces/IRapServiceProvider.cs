#region Using

using FreestyleOnline.classes.Providers;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapServiceProvider
    {
        T GetService<T>() where T : BaseRapProvider, new();
    }
}