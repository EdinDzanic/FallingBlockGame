﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine
{
    interface IDrawableComponent
    {
        IDrawable CreateDrawable();
    }
}
