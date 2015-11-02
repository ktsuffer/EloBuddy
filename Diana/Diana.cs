using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Settings = GG.Diana.Config.Drawing;

namespace GG.Diana
{
    public static class Diana
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Diana";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            // TODO: Switch statement or something instead of ifs; too rusty to remember

            if(Settings.DrawQ)
            {
                Circle.Draw(Color.DodgerBlue, SpellManager.Q.Range, Player.Instance.Position);
            }
            if (Settings.DrawW)
            {
                Circle.Draw(Color.AliceBlue, SpellManager.W.Range, Player.Instance.Position);
            }
            if (Settings.DrawE)
            {
                Circle.Draw(Color.AliceBlue, SpellManager.E.Range, Player.Instance.Position);
            }
            if (Settings.DrawR)
            {
                Circle.Draw(Color.AliceBlue, SpellManager.R.Range, Player.Instance.Position);
            }
        }
    }
}
