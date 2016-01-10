#region Using

using System;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    [Obsolete("Use YafLogger")]
    internal interface IRapLogger
    {
        void Log(string format, params object[] args);
    }
}