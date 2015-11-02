using EloBuddy;
using EloBuddy.SDK;
using System;

// Using the config like this makes your life easier, trust me
//using Settings = GG.Diana.Config.Drawing.Combo;

namespace GG.Diana.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            // TODO: Add combo logic here
            // See how I used the Settings.UseQ here, this is why I love my way of using
            // the menu in the Config class!
            // attack target with the least amount of MR
            var _target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

            var _qMana = SpellManager.ManaCost(SpellSlot.Q);
            var _wMana = SpellManager.ManaCost(SpellSlot.W);
            var _eMana = SpellManager.ManaCost(SpellSlot.E);
            var _rMana = SpellManager.ManaCost(SpellSlot.R);
            Random rand = new Random();

            if (_target != null && _target.IsValidTarget())
            {
                if (Q.IsLearned && Q.IsReady() && Q.IsInRange(_target))
                {
                    Q.Cast(_target);
                }
                if (_target.HasBuff("dianamoonlight") && R.IsLearned)
                {
                    // R to target and only use W if you'll have enough mana for another Q+R combo
                    //Chat.Print("has buff");
                    R.Cast(_target);
                    if (Player.Instance.Distance(_target) <= W.Range && Player.Instance.Mana >= _qMana + _rMana + _wMana)
                    {
                        W.Cast();
                        // The idea is to wait until max distance of E so that you get the most damage delt.
                        // for now it'll just get a random value <= 415 so that it at least
                        if (Player.Instance.Distance(_target) <= rand.Next(415))
                        {
                            E.Cast();
                            //Chat.Print()
                        }
                    }
                }

            }

        }
    }
}
