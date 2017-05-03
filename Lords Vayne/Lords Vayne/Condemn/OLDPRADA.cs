﻿using HesaEngine.SDK;
using Lords_Vayne.Condemn.Prada_Utils;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords_Vayne.Condemn
{
    class OLDPRADA
    {
        public static void Run()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, TargetSelector.DamageType.Physical);
            var pP = Program.Player.ServerPosition;
            var p = target.ServerPosition;
            var pD = Program.emenu.Item("PushDistance").GetValue<Slider>().CurrentValue;
            // var mode = Vayne.emenu.Item("EMode", true).GetValue<StringList>().SelectedIndex;
            if (!target.CanMove ||
                    (target.IsWindingUp))
            {
                Program.E.CastOnUnit(target);
            }

            var hitchance = Program.emenu.Item("EHitchance").GetValue<Slider>().CurrentValue;
            var angle = 0.20 * hitchance;
            const float travelDistance = 0.5f;
            var alpha = new Vector2((float)(p.X + travelDistance * Math.Cos(Math.PI / 180 * angle)),
                (float)(p.X + travelDistance * Math.Sin(Math.PI / 180 * angle)));
            var beta = new Vector2((float)(p.X - travelDistance * Math.Cos(Math.PI / 180 * angle)),
                (float)(p.X - travelDistance * Math.Sin(Math.PI / 180 * angle)));

            for (var i = 15; i < pD; i += 100)
            {
                if (pP.To2D().Extend(alpha,
                        i)
                    .To3D().IsCollisionable() || pP.To2D().Extend(beta, i).To3D().IsCollisionable())
                {
                    Program.E.CastOnUnit(target);
                }
            }
        }
    }
}