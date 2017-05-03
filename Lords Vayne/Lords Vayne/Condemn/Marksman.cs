using HesaEngine.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lords_Vayne.Misc;

namespace Lords_Vayne.Condemn
{
    class Marksman
    {
        public static void Run()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            for (var i = 1; i < 8; i++)
            {
                var targetBehind = target.Position +
                                   Vector3.Normalize(target.ServerPosition - ObjectManager.Player.Position) * i * 50;

                if (targetBehind.IsWall() && target.IsValidTarget(Program.E.Range))
                {
                    if (CheckTargets.CheckTarget(target, Program.E.Range))
                    {
                        Program.E.CastOnUnit(target);
                        return;
                    }
                }
            }
        }
    }
}