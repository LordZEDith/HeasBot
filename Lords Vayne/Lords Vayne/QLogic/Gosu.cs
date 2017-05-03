using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.QLogic
{
    class Gosu
    {
        public static void Run()
        {
            if ((Program.orbwalker.ActiveMode.ToString() == "Combo") && Program.qmenu.Item("UseQC").GetValue<bool>() && !Program.qmenu.Item("FastQ").GetValue<bool>()
                   || (Program.orbwalker.ActiveMode.ToString() == "Mixed" && Program.qmenu.Item("hq").GetValue<bool>()))

            {
                if (Program.qmenu.Item("restrictq").GetValue<bool>())
                {
                    var after = ObjectManager.Player.Position
                                + Normalize(Game.CursorPosition - ObjectManager.Player.Position) * 300;
                    //Game.PrintChat("After: {0}", after);
                    var disafter = Vector3.DistanceSquared(after, Program.tar.Position);
                    //Game.PrintChat("DisAfter: {0}", disafter);
                    //Game.PrintChat("first calc: {0}", (disafter) - (630*630));
                    if ((disafter < 630 * 630) && disafter > 150 * 150)
                    {
                        Program.Q.Cast(Game.CursorPosition);


                    }

                    if (Vector3.DistanceSquared(Program.tar.Position, ObjectManager.Player.Position) > 630 * 630
                        && disafter < 630 * 630)
                    {
                        Program.Q.Cast(Game.CursorPosition);


                    }
                }
                else
                {
                    Program.Q.Cast(Game.CursorPosition);



                }
                //Q.Cast(Game.CursorPosition);
            }
        }

        public static Vector3 Normalize(Vector3 A)
        {
            double distance = Math.Sqrt(A.X * A.X + A.Y * A.Y);
            return new Vector3(new Vector2((float)(A.X / distance)), (float)(A.Y / distance));
        }

     
    }
}