using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ts
{
    interface IDrawableComponent
    {
        IDrawable CreateDrawable();
    }
}
