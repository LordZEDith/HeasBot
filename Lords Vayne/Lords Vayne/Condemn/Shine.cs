using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn
{
    class Shine
    {
        public static void Run()
        {
            foreach (var target in ObjectManager.Heroes.Enemies.Where(h => h.IsValidTarget(Program.E.Range)))
            {

                var pushDistance = Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue;
                var targetPosition = Program.E.GetPrediction(target).UnitPosition;
                var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                float checkDistance = pushDistance / 40f;
                for (int i = 0; i < 40; i++)
                {
                    Vector3 finalPosition = targetPosition + (pushDirection * checkDistance * i);
                    var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                    if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building)) //not sure about building, I think its turrets, nexus etc
                    {
                        Program.E.CastOnUnit(target);
                    }
                }
            }
        }
    }
}
