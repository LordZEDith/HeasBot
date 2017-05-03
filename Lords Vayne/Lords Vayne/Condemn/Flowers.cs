using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using Lords_Vayne.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn
{
    class Flowers
    {
        public static void Run()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            var EPred = Program.E.GetPrediction(target);
            var PD = Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue;
            var PP = EPred.UnitPosition.Extend(ObjectManager.Player.Position, -PD);

            for (int i = 1; i < PD; i += (int)target.BoundingRadius)
            {
                var VL = EPred.UnitPosition.Extend(ObjectManager.Player.Position, -i);
                var J4 = ObjectManager.Get<Obj_AI_Base>()
                    .Any(f => f.Distance(PP) <= target.BoundingRadius && f.Name.ToLower() == "beacon");
                var CF = NavMesh.GetCollisionFlags(VL);

                if (CF.HasFlag(CollisionFlags.Wall) || CF.HasFlag(CollisionFlags.Building) || J4)
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