using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Events
{
    class OnCreates
    {
        public static void OnCreate(GameObject sender, EventArgs Args)
        {
            var Rengar = ObjectManager.Heroes.Enemies.Find(heros => heros.ChampionName.Equals("Rengar"));
            var Khazix = ObjectManager.Heroes.Enemies.Find(heros => heros.ChampionName.Equals("Khazix"));

            if (Rengar != null && Program.imenu.Item("AntiRengar").GetValue<bool>())
            {
                if (sender.Name == "Rengar_LeapSound.troy" && sender.Position.Distance(ObjectManager.Player.Position) < Program.E.Range)
                {
                    Program.E.CastOnUnit(Rengar);
                }
            }

            if (Khazix != null && Program.imenu.Item("AntiKhazix").GetValue<bool>())
            {
                if (sender.Name == "Khazix_Base_E_Tar.troy" && sender.Position.Distance(ObjectManager.Player.Position) <= 300)
                {
                    Program.E.CastOnUnit(Khazix);
                }
            }
        }
    }
}