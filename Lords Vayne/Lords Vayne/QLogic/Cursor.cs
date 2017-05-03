using HesaEngine.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.QLogic
{
    class Cursor
    {
        public static void Run()
        {
            Program.Q.Cast(Game.CursorPosition);
        }
    }
}