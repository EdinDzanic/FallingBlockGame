using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace engine
{
    public class Menu
    {
        private List<MenuItem> menuItems;
        private int selectedIndex;
        private SpriteFont spriteFont;

        public Vector2 Position { get; set; }
        public Color NormalColor { get; set; }
        public Color SelectedColor { get; set; }
        public List<MenuItem> MenuItems { get { return menuItems; } }
        public int SelectedIndex { get { return selectedIndex; } }

        public Menu(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            menuItems = new List<MenuItem>();
            selectedIndex = 0;
            NormalColor = Color.White;
            SelectedColor = Color.Red;
        }

        public void Update()
        {
            if (InputManager.KeyReleased(Keys.Down))
            {
                selectedIndex++;

                if (selectedIndex >= MenuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (InputManager.KeyReleased(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = MenuItems.Count - 1;
                }
            }

            if (InputManager.KeyReleased(Keys.Enter))
            {
                MenuItems[selectedIndex].PerformClick();
            }
        }

        public void Draw(Graphics graphics)
        {
            Vector2 menuPosition = Position;
            graphics.SpriteBatch.Begin();
            for (int i = 0; i < MenuItems.Count; i++)
            {
                if (i == selectedIndex)
                    graphics.SpriteBatch.DrawString(spriteFont, MenuItems[i].Text, menuPosition, SelectedColor);
                else
                    graphics.SpriteBatch.DrawString(spriteFont, MenuItems[i].Text, menuPosition, NormalColor);
                menuPosition.Y += spriteFont.LineSpacing;
            }
            graphics.SpriteBatch.End();
        }

    }
}
