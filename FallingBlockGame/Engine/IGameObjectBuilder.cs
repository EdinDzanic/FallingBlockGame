using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ts
{
    public interface IGameObjectBuilder
    {
        List<GameObject> CreateGameObjects();
    }
}
