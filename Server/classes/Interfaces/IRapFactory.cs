using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreestyleOnline.classes.Base;

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapFactory
    {
        T GetFactory<T>() where T : BaseFactory, new();
    }
}
