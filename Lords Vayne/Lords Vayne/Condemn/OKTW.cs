using HesaEngine.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lords_Vayne.Misc;

namespace Lords_Vayne.Condemn
{
    class OKTW
    {
        public static void Run()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            var prediction = Program.E.GetPrediction(target);

            if (prediction.Hitchance >= HitChance.High)
            {
                var finalPosition = prediction.UnitPosition.Extend(ObjectManager.Player.Position, -400);

                if (finalPosition.IsWall())
                {
                    Program.E.CastOnUnit(target);
                    return;
                }

                for (var i = 1; i < 400; i += 50)
                {
                    var loc3 = prediction.UnitPosition.Extend(ObjectManager.Player.Position, -i);

                    if (loc3.IsWall())
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
}