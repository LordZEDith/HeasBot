using HesaEngine.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace Lords_Vayne.QLogic
{
    class VHR
    {
        public static object SV { get; private set; }

        public static void Run()
        {
            var tg = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            if (Program.qmenu.Item("smartq").GetValue<bool>())
            {
                var position = SV.TumbleLogicProvider.GetSOLOVayneQPosition();
                if (position != Vector3.Zero)
                {
                    CastTumble(position, tg);
                }
            }
            else
            {
                var position = ObjectManager.Player.ServerPosition.Extend(Game.CursorPosition, 300f).To2D();
                if (position.IsSafe())
                {
                    CastTumble(position, tg);
                }
            }
        }
    }
}*/ 
