using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine
{
    public class MenuItem
    {
        public string Text { get; set; }
        public int Index { get; set; }

        public MenuItem()
        {
            Text = "";
            Index = 0;
        }

        public MenuItem(string text)
        {
            Text = text;
            Index = 0;
        }

        public event EventHandler Click;

        protected void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }

    }
}
