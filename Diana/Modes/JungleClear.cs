using EloBuddy;
using EloBuddy.SDK;
using System.Linq;

namespace GG.Diana.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var _jungleMobs = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.ServerPosition, Q.Range, false);
            // assumes jungle item is on slot 1
            var _item = new InventorySlot((uint)Player.Instance.NetworkId, 1);
            
            foreach (var _monster in _jungleMobs.Where(x => x.IsValidTarget(S.Range)))
            {
                var _skinName = _monster.BaseSkinName;
                var _rDmg = DamageLibrary.GetSpellDamage(Player.Instance, _monster, SpellSlot.R, DamageLibrary.SpellStages.Default);

                switch (_skinName)
                {

                    case "SRU_Baron":
                    case "SRU_Dragon":
                        // R+Smite steal
                        if (_rDmg + SmiteManager.SmiteDamage >= _monster.Health)
                        {
                            R.Cast(_monster);
                            S.Cast(_monster);
                        }
                        if (_monster.Health <= SmiteManager.SmiteDamage && S.IsInRange(_monster) && S.IsReady())
                        {
                            S.Cast(_monster);
                        }
                        if (Q.IsLearned && Q.IsReady())
                        {
                            Q.Cast(_monster);
                        }
                        if (W.IsInRange(_monster))
                        {
                            W.Cast();
                        }
                        break;
                    case "SRU_Crab":
                    case "SRU_Murkwolf":
                    case "SRU_Razorbeak":
                    case "SRU_Krug":
                    case "SRU_Gromp":
                    case "SRU_Blue":
                    case "SRU_Red":
                        if (_monster.Health <= SmiteManager.SmiteDamage && S.IsInRange(_monster) && S.IsReady())
                        {
                            S.Cast(_monster);
                        }
                        if (Q.IsLearned && Q.IsReady())
                        {
                            Q.Cast(_monster);
                        }
                        if (W.IsInRange(_monster))
                        {
                            W.Cast();
                        }

                        // smite buff if next auto attack will kill you
                        if (_monster.GetAutoAttackDamage(Player.Instance) >= Player.Instance.Health)
                        {
                            S.Cast(_monster);
                        }
                        break;
                    default: break;
                }
            }
        }
    }
}
