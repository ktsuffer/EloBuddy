using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;


namespace GG.Diana
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Targeted R { get; private set; }
        public static Spell.Targeted S { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            // W value taken from nomn's Simplistic Diana; tyty <3

            // Q skillshot seems to be larger than 830 when tested in-game
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Cone, 500, 1600, 195);
            W = new Spell.Active(SpellSlot.W, 200);
            // When testing E on minions, it seems this is the most accurate range. Jungle monsters are slightly shorter range
            // probably due to their hitbox?
            E = new Spell.Active(SpellSlot.E, 415);
            // R range is shorter than 825 it seems to be about 750; perhaps slightly shorter
            R = new Spell.Targeted(SpellSlot.R, 750);
            // S needs testing...
            S = new Spell.Targeted(SpellSlot.Summoner1, 500);
        }

        public static short ManaCost(SpellSlot spell)
        {
            // Mana cost per level for Q
            // Unfortunately due to the large amount of spellslots, we have to return a value of all of them otherwise
            // we could have an unhandled error or crash

            // Remember that Spellslot levels can only be from 1-5 it doesn't start at 0.
            // Also this is a very sloppy implimentation of checking mana values, but hopefully in the future
            // EloBuddy has their own implimentation
            switch (spell)
            {
                case SpellSlot.Unknown:
                    return -1;
                case SpellSlot.Q:
                    if(Q.IsLearned) return 55;
                    // if not learned then returns 0 since Q doesn't cost any mana
                    return 0;
                case SpellSlot.W:
                    if (W.IsLearned)
                    {
                        switch (W.Level)
                        {
                            case 1: return 60;
                            case 2: return 70;
                            case 3: return 80;
                            case 4: return 90;
                            case 5: return 100;
                            default:
                                Chat.Print("Error in SpellManager.ManaCost(); R.Level.");
                                return -1;
                        }
                    }
                    return 0;
                case SpellSlot.E:
                    if (E.IsLearned) return 70;
                    return 0;
                case SpellSlot.R:
                    if (R.IsLearned)
                    {
                        switch (R.Level)
                        {
                            case 1: return 60;
                            case 2: return 70;
                            case 3: return 80;
                            case 4: return 90;
                            case 5: return 100;
                            default:
                                Chat.Print("Error in SpellManager.ManaCost(); R.Level.");
                                return -1;
                        }
                    }
                    return 0;
                case SpellSlot.Summoner1:
                    return 0;
                case SpellSlot.Summoner2:
                    return -1;
                case SpellSlot.Item1:
                    return -1;
                case SpellSlot.Item2:
                    return -1;
                case SpellSlot.Item3:
                    return -1;
                case SpellSlot.Item4:
                    return -1;
                case SpellSlot.Item5:
                    return -1;
                case SpellSlot.Item6:
                    return -1;
                case SpellSlot.Trinket:
                    return -1;
                case SpellSlot.Recall:
                    return -1;
                case SpellSlot.OathSworn:
                    return -1;
                case SpellSlot.CapturePoint:
                    return -1;
                case SpellSlot.Internal:
                    return -1;
                default:
                    return -1;
            }
        }

        public static void Initialize()
        {
            
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
