using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Dawn.Engine;

namespace Dawn.Engine.Others
{
    static class GameTool
    {
        public static void SetWindowPosition(GameWindow window, int x, int y)
        {
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(window.Handle);
            form.Location = new System.Drawing.Point(x, y);
        }


    }

}
