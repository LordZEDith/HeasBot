using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn.Lords_Utils
{
    static class HeroExtension
    {
        public static List<AIHeroClient> GetLhEnemiesNear(this Vector3 position, float range, float healthpercent)
        {
            return ObjectManager.Heroes.Enemies.Where(hero => hero.IsValidTarget(range, true, position) && hero.HealthPercent <= healthpercent).ToList();
        }
    }
}
