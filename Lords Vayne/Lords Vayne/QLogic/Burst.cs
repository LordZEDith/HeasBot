using HesaEngine.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.QLogic
{
    class Bursts
    {
        public static void Burst()
        {

            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            if (!target.IsValidTarget())
            {
                return;
            }
            else
                if (Program.Q.IsReady() && target.IsValidTarget(600)  &&
                    target.GetBuffCount("vaynesilvereddebuff") == 2)
            {
                Program.Q.Cast(Game.CursorPosition);

            }
        }

    }
}