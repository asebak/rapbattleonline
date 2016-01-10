using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapValidate
    {
        bool Validate(HttpRequestMessage request);
    }
}
