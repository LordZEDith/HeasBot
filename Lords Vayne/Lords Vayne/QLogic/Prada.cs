using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using Lords_Vayne.Condemn.Prada_Utils;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HesaEngine.SDK.ObjectManager;

namespace Lords_Vayne.QLogic
{
   static class Prada
    {
        public static Vector3 GetTumblePos(this Obj_AI_Base target)
        {
            if (target == null) return Vector3.Zero;
            //if the target is not a melee and he's alone he's not really a danger to us, proceed to 1v1 him :^ )
            if (!target.IsMelee() && Player.CountEnemiesInRange(800) == 1) return Game.CursorPosition;

            var aRC = new Condemn.Prada_Utils.Geometry.Circle(Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();
            var cursorPos = Game.CursorPosition;
            var targetPosition = target.ServerPosition;
            var pList = new List<Vector3>();
            var additionalDistance = (0.106 + Game.Ping / 2000f) * target.MovementSpeed;

            if (!cursorPos.IsDangerousPosition()) return cursorPos;

            foreach (var p in aRC)
            {
                var v3 = new Vector2(p.X, p.Y).To3D();

                if (target.IsFacing(Player))
                {
                    if (!v3.IsDangerousPosition() && v3.Distance(targetPosition) < 530) pList.Add(v3);
                }
                else
                {
                    if (!v3.IsDangerousPosition() && v3.Distance(targetPosition) < 530 - additionalDistance) pList.Add(v3);
                }
            }
            if (Player.UnderTurret() || Player.CountEnemiesInRange(800) == 1)
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance(cursorPos)).FirstOrDefault() : Vector3.Zero;
            }
            if (Program.qmenu.Item("QOrderBy").GetValue<StringList>().Equals("CLOSETOTARGET"))
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance(targetPosition)).FirstOrDefault() : Vector3.Zero;
            }
            return pList.Count > 1 ? pList.OrderByDescending(el => el.Distance(cursorPos)).FirstOrDefault() : Vector3.Zero;
        }

    }

}