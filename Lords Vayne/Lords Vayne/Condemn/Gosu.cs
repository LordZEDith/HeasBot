using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn
{
    class Gosu
    {
        public static void Run()
        {
            foreach (var hero in from hero in ObjectManager.Get<AIHeroClient>().Where(hero => hero.IsValidTarget(550f))
                                 let prediction = Program.E.GetPrediction(hero)
                                 where NavMesh.GetCollisionFlags(
                                     prediction.UnitPosition.To2D()
                                         .Extend(ObjectManager.Player.ServerPosition.To2D(),
                                             -Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue)
                                         .To3D())
                                     .HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(
                                         prediction.UnitPosition.To2D()
                                             .Extend(ObjectManager.Player.ServerPosition.To2D(),
                                                 -(Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue / 2))
                                             .To3D())
                                         .HasFlag(CollisionFlags.Wall)
                                 select hero)
            {

                Program.E.CastOnUnit(hero);
            }
        }
    }
}