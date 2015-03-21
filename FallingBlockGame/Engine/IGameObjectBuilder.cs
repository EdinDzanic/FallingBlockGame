﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine
{
    public interface IGameObjectBuilder
    {
        List<GameObject> CreateGameObjects();
    }
}
