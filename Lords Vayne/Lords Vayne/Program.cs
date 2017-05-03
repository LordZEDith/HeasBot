using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;
using Lords_Vayne.Events;

namespace Lords_Vayne
{
    public class Program : IScript
    {
        public string Name => ("Lord's Vayne");
        public string Version => ("1.0");
        public string Author => ("LordZEDith");


        public static Spell E;
        public static Spell E2;
        public static Spell Q;
        public static Spell W;
        public static Spell R;




        public static Vector3 TumblePosition = Vector3.Zero;


        // public static SebbyLib.Orbwalking.Orbwalker orbwalker;
        public static Orbwalker.OrbwalkerInstance orbwalker;

        private static string News = "Added Smart E";

        public static Menu menu;

        public static Dictionary<string, SpellSlot> spellData;
        public static Item zzrot = new Item(3512, 400);

        public static AIHeroClient tar;
        public const string ChampName = "Vayne";
        public static AIHeroClient Player;

        //public static Menu Itemsmenu;
        public static Menu qmenu;
        public static Menu emenu;
        public static Menu gmenu;
        public static Menu imenu;
        public static Menu rmenu;




        public static float LastMoveC;

        public static void MoveToLimited(Vector3 where)
        {
            if (Environment.TickCount - LastMoveC < 80)
            {
                return;
            }

            LastMoveC = Environment.TickCount;
            Player.IssueOrder(GameObjectOrder.MoveTo, where);
        }

        public void OnInitialize()
        {
            try
            {
                Game.OnGameLoaded += Game_OnGameLoad;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }
        #region GameOnLoad

        public static void Game_OnGameLoad()
        {
            Program.Q = new Spell(SpellSlot.Q, 300f);
            Program.W = new Spell(SpellSlot.W);
            Program.E2 = new Spell(
               SpellSlot.E, (uint)(650 + ObjectManager.Player.BoundingRadius));
            Program.E = new Spell(SpellSlot.E, 550f);
            Program.E.SetTargetted(0.25f, 1600f);
            Program.R = new Spell(SpellSlot.R);

            Program.Player = ObjectManager.Player;


            if (Program.Player.ChampionName != ChampName) return;
            Program.spellData = new Dictionary<string, SpellSlot>();

            Program.menu = Menu.AddMenu("Lord's Vayne");

           // Program.menu.AddSubMenu(new Menu("Orbwalker"));
           // Program.orbwalker = new SebbyLib.Orbwalking.Orbwalker(Program.menu.SubMenu("Orbwalker"));

           // var TargetSelectorMenu = new Menu("Target Selector", "Target Selector");
           // TargetSelector.AddToMenu(TargetSelectorMenu);

           // Program.menu.AddSubMenu(TargetSelectorMenu);

            Program.menu.Add(
                new MenuKeybind("aaqaa", "Auto -> Q -> AA",SharpDX.DirectInput.Key.X,MenuKeybindType.OnRelease));

            Program.qmenu = Program.menu.AddSubMenu("Tumble");
            Program.qmenu.Add(new MenuCheckbox("FastQ", "Fast Q")).SetValue(true).SetValue(true).SetTooltip("Q Animation Cancelation"); //.SetTooltip("Q Animation Cancelation"));
            Program.qmenu.Add(new MenuCheckbox("FastQs", "Cancel Q on Manual Click")).SetValue(true).SetValue(true).SetTooltip("Cancel Your Q when you manually cast it(Need to have \"Fast Q\" on too)"); //.Permashow(true, "Vayne | Vayne Manul Cancel", Color.Aqua);
            Program.qmenu.Add(new MenuCheckbox("UseQC", "Use Q Combo")).SetValue(true);
            Program.qmenu.Add(new MenuCombo("QMode", "Use Q Mode:", new[] { "Gosu", "Side", "Cursor", "SmartQ", "SafeQ", "AggroQ", "Burst", "Hiki" }));
            /*if (Program.qmenu.Item("QMode", true).GetValue<StringList>().SelectedIndex == 8)
            {
                Program.qmenu.AddItem(new MenuCheckbox("QOrderBy", "Q to position").SetValue(new StringList(new[] { "CLOSETOMOUSE", "CLOSETOTARGET" })));
            }*/
         /*   if (Program.qmenu.Item("QMode").GetValue<StringList>().DefaultValue == 8)
            {
                Program.qmenu.Add(new MenuCheckbox("smartq", "Use Smart Q").SetValue(true));
                Program.qmenu.Add(new MenuCheckbox("qspam", "Ignore Checks AKA Spam Q").SetValue(true));

            }*/
            Program.qmenu.Add(new MenuCheckbox("hq", "Use Q Harass")).SetValue(true);
            Program.qmenu.Add(new MenuCheckbox("restrictq", "Restrict Q usage?").SetValue(true));
            Program.qmenu.Add(new MenuCheckbox("UseQJ", "Use Q Farm").SetValue(true));
            Program.qmenu.Add(new MenuSlider("Junglemana", "Minimum Mana to Use Q Farm").SetValue(new Slider(60, 1, 100)));
            Program.qmenu.Add(new MenuCheckbox("AntiMQ", "Use Anti - Melee [Q]").SetValue(true));
            Program.qmenu.Add(new MenuCheckbox("FocusTwoW", "Focus 2 W Stacks").SetValue(true));
            //qmenu.AddItem(new MenuCheckbox("DrawQ", "Draw Q Arrow").SetValue(true));


            Program.emenu = Program.menu.AddSubMenu("Condemn");
            Program.emenu.Add(new MenuCheckbox("UseEC", "Use E Combo").SetValue(true));
            Program.emenu.Add(new MenuCheckbox("he", "Use E Harass").SetValue(true));
            Program.emenu.Add(new MenuKeybind("UseCF", "Use Flash Condemn", SharpDX.DirectInput.Key.U,MenuKeybindType.OnRelease));
            Program.emenu.Add(new MenuKeybind("UseCFA", "Use Flash Condemn (Toggle)", SharpDX.DirectInput.Key.O,MenuKeybindType.Toggle)).SetTooltip("Auto Flash Condemn when you have % HP");
            Program.emenu.Add(new MenuSlider("UseCFHP", "Use Flash Condemn if % HP").SetValue(new Slider(25)));
            Program.emenu.Add(new MenuKeybind("UseET", "Use E (Toggle)",SharpDX.DirectInput.Key.T,MenuKeybindType.Toggle));
            Program.emenu.Add(new MenuKeybind("zzrot", "[Beta] ZZrot Condemn",SharpDX.DirectInput.Key.I,MenuKeybindType.Toggle));
            // emenu.AddItem(new MenuCheckbox("FlashE", "Flash E").SetValue(true).SetValue(new KeyBind("Y".ToCharArray()[0], KeyBindType.Press)));


            //emenu.AddItem(new MenuCheckbox("Gap_E", "Use E To Gabcloser").SetValue(true));
            // emenu.AddItem(new MenuCheckbox("GapD", "Anti GapCloser Delay").SetValue(new Slider(0, 0, 1000)).SetTooltip("Sets a delay before the Condemn for Antigapcloser is casted."));
            Program.emenu.Add(new MenuCombo("EMode", "Use E Mode:", new[] { "Lord's", "Gosu", "Flowers", "VHR", "Marksman", "Sharpshooter", "OKTW", "Shine", "PRADASMART", "PRADAPERFECT", "OLDPRADA", "PRADALEGACY", "Lord's Smart E" }));
            Program.emenu.Add(new MenuSlider("PushDistance", "E Push Distance").SetValue(new Slider(415, 475, 300)));
            Program.emenu.Add(new MenuSlider("EHitchance", "E Hitchance")).SetValue(new Slider(50, 1, 100)).SetTooltip("Only use this for Prada Condemn Methods");
            Program.emenu.Add(new MenuKeybind("UseEaa", "Use E after auto",SharpDX.DirectInput.Key.M,MenuKeybindType.OnRelease));


            Program.rmenu = Program.menu.AddSubMenu("Ult");
            Program.rmenu.Add(new MenuCheckbox("visibleR", "Smart Invisible R")).SetValue(true).SetTooltip("Wether you want to set a delay to stay in R before you Q");
            Program.rmenu.Add(new MenuSlider("Qtime", "Duration to wait").SetValue(new Slider(700, 0, 1000)));

            Program.imenu = Program.menu.AddSubMenu("Interrupt Settings");
            Program.imenu.Add(new MenuCheckbox("Int_E", "Use E To Interrupt").SetValue(true));
            Program.imenu.Add(new MenuCheckbox("Interrupt", "Interrupt Danger Spells", true).SetValue(true));
            Program.imenu.Add(new MenuCheckbox("AntiAlistar", "Interrupt Alistar W", true).SetValue(true));
            Program.imenu.Add(new MenuCheckbox("AntiRengar", "Interrupt Rengar Jump", true).SetValue(true));
            Program.imenu.Add(new MenuCheckbox("AntiKhazix", "Interrupt Khazix R", true).SetValue(true));
            Program.imenu.Add(new MenuCheckbox("AntiKhazix", "Interrupt Khazix R", true).SetValue(true));


            Program.gmenu = Program.menu.AddSubMenu("Gap Closer");
            Program.gmenu.Add(new MenuCheckbox("Gapcloser", "Anti Gapcloser", true).SetValue(false));
            foreach (var target in ObjectManager.Heroes.Enemies)
            {
                Program.gmenu.Add(
                    new MenuCheckbox("AntiGapcloser" + target.ChampionName.ToLower(), target.ChampionName, true)
                        .SetValue(false));
            }



            Program.menu.Add(new MenuCheckbox("useR", "Use R Combo").SetValue(false));
            Program.menu.Add(new MenuSlider("enemys", "If Enemys Around >=").SetValue(new Slider(2, 1, 5)));



            Q = new Spell(SpellSlot.Q, 0f);
            R = new Spell(SpellSlot.R, float.MaxValue);
            E = new Spell(SpellSlot.E, 650f);
            E.SetTargetted(0.25f, 1600f);



            E.SetTargetted(0.25f, 2200f);
            Obj_AI_Base.OnProcessSpellCast += Game_SpellProcess.Game_ProcessSpell;
            Obj_AI_Base.OnProcessSpellCast += OnSpellProcess.Obj_AI_Base_OnProcessSpellCast;
           // Game.OnUpdate += GameUpdate.Game_OnGameUpdate;
            Orbwalker.AfterAttack += AfterAttack.Orbwalking_AfterAttack;
           // Orbwalker.BeforeAttack += BeforeAttack.Orbwalking_BeforeAttack;
           // AntiGapcloser.OnEnemyGapcloser += AntiGapCloser.AntiGapcloser_OnEnemyGapcloser;
           // InterruptableSpell.On += Interrupters.OnInterruptableTarget;
           // GameObject.OnCreate += OnCreates.OnCreate;
            //  Drawing.OnDraw += DrawingOnOnDraw;


            //Game.PrintChat("<font color='#881df2'>Blm95 Vayne Reborn by LordZEDith</font> Loaded.");
           Chat.Print("<font size='30'>Lord's Vayne</font> <font color='#b756c5'>by LordZEDith</font>");
            Chat.Print("<font color='#b756c5'>NEWS: </font>" + Program.News);
            //Game.PrintChat(
            // "<font color='#f2f21d'>Do you like it???  </font> <font color='#ff1900'>Drop 1 Upvote in Database </font>");
            //  Game.PrintChat(
            //  "<font color='#f2f21d'>Buy me cigars </font> <font color='#ff1900'>ssssssssssmith@hotmail.com</font> (10) S");
           // Program.menu.;
        }


    }
    #endregion
}
