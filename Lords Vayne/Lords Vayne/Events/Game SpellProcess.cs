using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Events
{
    class Game_SpellProcess
    {
            public static void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
            {

                if (hero != null)
                    if (args.Target != null)
                        if (args.Target.IsMe)
                            if (hero.ObjectType == GameObjectType.AIHeroClient)
                                if (hero.IsEnemy)
                                    if (hero.IsMelee())
                                        if (args.SData.IsAutoAttack())
                                            if (Program.qmenu.Item("AntiMQ").GetValue<bool>())
                                                if (Program.Q.IsReady())
                                                    Program.Q.Cast(ObjectManager.Player.Position.Extend(hero.Position, - Program.Q.Range));

            }
        }
    }