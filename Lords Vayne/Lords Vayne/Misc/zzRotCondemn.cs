using HesaEngine.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Misc
{
    class zzRotCondemn
    {
        public static void RotE()
        {
            var target = TargetSelector.GetTarget(Program.zzrot.Range, TargetSelector.DamageType.Physical);
            if (Program.E.IsReady() && Program.zzrot.IsInRange(target) && Program.zzrot.IsReady() && target != null)
            {
              /*  if (Program.zzrot.Cast(target.ServerPosition.To2D()))
                {
                    Program.E2.CastOnUnit(target);
                }*/
            }
        }
    }
}