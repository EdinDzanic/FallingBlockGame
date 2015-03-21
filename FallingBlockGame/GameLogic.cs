using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FallingBlockGame
{
    public class GameLogic
    {
        private const int FIELD_WIDTH = 10;
        private const int FIELD_HEIGHT = 20;
        
        private Field field;
        public Field Field { get { return field;} }

        public GameLogic()
        {
            field = new Field(FIELD_HEIGHT, FIELD_WIDTH, 20, 20);
        }
    }
}
