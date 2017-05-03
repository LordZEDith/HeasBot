using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn
{
    class PRADALEAGACY
    {
        public static void Run()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            var pP = Program.Player.ServerPosition;
            var p = target.ServerPosition;
            var pD = Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue;
            // var mode = Vayne.emenu.Item("EMode", true).GetValue<StringList>().SelectedIndex;
            var prediction = Program.E.GetPrediction(target);
            for (var i = 15; i < pD; i += 75)
            {
                var posCF = NavMesh.GetCollisionFlags(
                    prediction.UnitPosition.To2D()
                        .Extend(
                            pP.To2D(),
                            -i)
                        .To3D());
                if (posCF.HasFlag(CollisionFlags.Wall) || posCF.HasFlag(CollisionFlags.Building))
                {

                    Program.E.CastOnUnit(target);

                }
            }
        }
    }
}