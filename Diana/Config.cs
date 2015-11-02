using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace GG.Diana
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "Diana";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to this AddonTemplate!");
            Menu.AddLabel("To change the menu, please have a look at the");
            Menu.AddLabel("Config.cs class inside the project, now have fun!");

            // Initialize the modes
            Drawing.Initialize();
            Skin.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Drawing
        {
            private static Menu Menu { get; set; }

            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
            public static bool DrawW
            {
                get { return _drawW.CurrentValue; }
            }
            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }

            static Drawing()
            {
                Menu = Config.Menu.AddSubMenu("Drawing");

                Menu.AddGroupLabel("Spell Ranges");

                // Make a loop to do this instead of calling Menu.Add so many times seperately.
                _drawQ = Menu.Add("drawQ", new CheckBox("Q Range"));
                _drawW = Menu.Add("drawW", new CheckBox("W Range"));
                _drawE = Menu.Add("drawE", new CheckBox("E Range"));
                _drawR = Menu.Add("drawR", new CheckBox("R Range", false));
                
            }
            public static void Initialize()
            {
                
            }
        }

        public static class Skin
        {
            private static Menu Menu { get; set; }

            private static readonly CheckBox _enableSkin;
            private static readonly Slider _skinId;

            private static readonly string[] _skinNames = { "Dark Valkyrie", "Lunar Goddess" };

            
            static Skin()
            {
                Menu = Config.Menu.AddSubMenu("Skin Selector");

                Menu.AddGroupLabel("Skin Selector");

                _enableSkin = Menu.Add("enableSkin", new CheckBox("Enable Skin", false));
                _skinId = Menu.Add("skinId", new Slider("Classic", 1, 1, 2));
            }
            public static void Initialize()
            {
                // Because of the way saving works, we want to make sure that if you enabled a skin the last
                // time you used this plugin that it's still enabled and to the correct skin
                if (_enableSkin.CurrentValue)
                {
                    Player.SetSkinId(_skinId.CurrentValue);
                    _skinId.DisplayName = _skinNames[_skinId.CurrentValue - 1];
                }
                
                // Call onValueChange functions for real-time changes
                _skinId.OnValueChange += _skinId_OnValueChange;
                _enableSkin.OnValueChange += _enableSkin_OnValueChange;
            }

            private static void _enableSkin_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
            {
                // Set skin to default and Display name to Classic when disabled
                if (!sender.CurrentValue) {
                    Player.SetSkinId(0);
                    _skinId.DisplayName = "Classic";
                }
                // Set skin and display name based on slider value
                if (sender.CurrentValue) {
                    Player.SetSkinId(_skinId.CurrentValue);
                    _skinId.DisplayName = _skinNames[_skinId.CurrentValue - 1];
                }
            }

            private static void _skinId_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                // Change Skin and Display name if the slider changes
                if (_enableSkin.CurrentValue) {
                    Player.SetSkinId(sender.CurrentValue);
                    sender.DisplayName = _skinNames[sender.CurrentValue-1];
                }
            }
        }
    }
}
