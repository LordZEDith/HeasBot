using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Events
{
    class AfterAttack
    {
        private static AIHeroClient enemy;

        // AIHeroClient enemy;

        public static void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (!unit.IsMe) return;

            if (Program.orbwalker.ActiveMode.ToString() == "LaneClear"
                && 100 * (Program.Player.Mana / Program.Player.MaxMana) > Program.qmenu.Item("Junglemana").GetValue<Slider>().CurrentValue)
            {
                var mob =
                    MinionManager.GetMinions(
                        Program.Player.ServerPosition,
                        Program.E.Range,
                        MinionTypes.All,
                        MinionTeam.Neutral,
                        MinionOrderTypes.MaxHealth).FirstOrDefault();
                var Minions = MinionManager.GetMinions(
                    Program.Player.Position.Extend(Game.CursorPosition, Program.Q.Range),
                    Program.Player.AttackRange,
                    MinionTypes.All);
                var useQ = Program.qmenu.Item("UseQJ").GetValue<bool>();
                int countMinions = 0;
                foreach (var minions in
                    Minions.Where(
                        minion =>
                        minion.Health < Program.Player.GetAutoAttackDamage(minion)
                        || minion.Health < Program.Q.GetDamage(minion) + Program.Player.GetAutoAttackDamage(minion) || minion.Health < Program.Q.GetDamage(minion)))
                {
                    countMinions++;
                }

                if (countMinions >= 2 && useQ && Program.Q.IsReady() && Minions != null) Program.Q.Cast(Program.Player.Position.Extend(Game.CursorPosition, Program.Q.Range / 2));

                if (useQ && Program.Q.IsReady() && Orbwalker.InAutoAttackRange(mob) && mob != null)
                {
                    Program.Q.Cast(Game.CursorPosition);

                }
            }


            if (!(target is AIHeroClient)) return;

            Program.tar = (AIHeroClient)target;

            if (Program.menu.Item("aaqaa").GetValue<KeyBind>().Active)
            {
                if (Program.Q.IsReady())
                {

                    Program.Q.Cast(Game.CursorPosition);
                    Game.SendEmote(Emote.Dance);

                }

                Orbwalker.Orbwalk(TargetSelector.GetTarget(625, TargetSelector.DamageType.Physical), Game.CursorPosition);
            }

            // Condemn.FlashE();

            if (Program.menu.Item("zzrot").GetValue<KeyBind>().Active)
            {
                Misc.zzRotCondemn.RotE();
            }

            if (Program.emenu.Item("UseEaa").GetValue<KeyBind>().Active)
            {
                Program.E.Cast((Obj_AI_Base)target);

            }

            //QLogic
            if ((Program.qmenu.Item("UseQC").GetValue<bool>() && Program.orbwalker.ActiveMode == Orbwalker.OrbwalkingMode.Combo) || (Program.orbwalker.ActiveMode == Orbwalker.OrbwalkingMode.Harass && Program.qmenu.Item("hq").GetValue<bool>()))
            {
                var value = Program.qmenu.Item("QMode").GetValue<StringList>().DefaultValue;
                var FastQ = Program.qmenu.Item("FastQ").GetValue<bool>();
                var htarget = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);

                if (value == 0)
                {
                    QLogic.Gosu.Run();
                }

                if (value == 1)
                {
                    Program.Q.Cast(QLogic.Lords.SideQ(ObjectManager.Player.Position.To2D(), target.Position.To2D(), 65).To3D());
                }

                if (value == 2)
                {
                    Program.Q.Cast(Game.CursorPosition);
                }

                if (value == 3)
                {
                    if (ObjectManager.Player.Position.Extend(Game.CursorPosition, 700).CountEnemiesInRange(700) <= 1)
                    {
                        Program.Q.Cast(Game.CursorPosition);
                    }
                }

                if (value == 4)
                {
                    Program.Q.Cast(QLogic.Lords.SafeQ(ObjectManager.Player.Position.To2D(), target.Position.To2D(), 65).To3D());
                }

                if (value == 5)
                {
                    Program.Q.Cast(QLogic.Lords.AggresiveQ(ObjectManager.Player.Position.To2D(), target.Position.To2D(), 65).To3D());
                }

                if (value == 6)
                {
                    QLogic.Bursts.Burst();
                }
                if (value == 7)
                {
                    QLogic.Hiki.SafePositionQ(htarget);
                }

                if (value == 8)
                {

                }
            }
        }
    }
}
    


