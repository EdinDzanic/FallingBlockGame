#region Using statemants

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace Ts
{
    public class GameObject
    {
        public PositionComponent PositionComponent { get; set; }
        public DrawableComponent DrawableComponent { get; set; }

        public GameObject(){}
        public GameObject(PositionComponent positionComponent, DrawableComponent drawableComponent)
        {
            PositionComponent = positionComponent;
            DrawableComponent = drawableComponent;
        }
    }
}
