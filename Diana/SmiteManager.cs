using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace GG.Diana
{
    public static class SmiteManager
    {
        static SmiteManager()
        {
           
        }

        public static int SmiteDamage
        {
            get
            {
                switch (Player.Instance.Level)
                {
                    case 1: return 390;
                    case 2: return 410;
                    case 3: return 430;
                    case 4: return 450;
                    case 5: return 480;
                    case 6: return 510;
                    case 7: return 540;
                    case 8: return 570;
                    case 9: return 600;
                    case 10: return 640;
                    case 11: return 680;
                    case 12: return 720;
                    case 13: return 760;
                    case 14: return 800;
                    case 15: return 850;
                    case 16: return 900;
                    case 17: return 950;
                    case 18: return 1000;
                    default:
                        Chat.Print("Error occured in SmiteManager.SmiteDamage.");
                        return 0;
                        
                }
            }
        }
        public static void Initialize()
        {

            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
