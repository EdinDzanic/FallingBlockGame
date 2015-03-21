using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace engine
{
    public static class InputManager
    {
        #region Field and Property Region

        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        public static KeyboardState KeyboardState 
        {
            get { return keyboardState; } 
        }
        public static KeyboardState LastKeyboardState 
        {
            get { return lastKeyboardState; }
        }

        #endregion

        #region Method Region

        public static void Update()
        {
            lastKeyboardState = KeyboardState;
            keyboardState = Keyboard.GetState();
        }

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        #endregion
    }
}
