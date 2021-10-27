using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoConsole
{
    public class NoConsoleModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            Hook h = new Hook(
                typeof(ETGModGUI).GetMethod("Update", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance),
                typeof(NoConsoleModule).GetMethod("HookUpdate")
            );
        }

        public static void HookUpdate(Action<ETGModGUI> orig, ETGModGUI self)
        {
            ETGModGUI.MenuOpened menu = ETGModGUI.CurrentMenu;
            orig(self);
            if (ETGModGUI.CurrentMenu == ETGModGUI.MenuOpened.Console)
            {
                if (menu == ETGModGUI.MenuOpened.Console)
                {
                    ETGModGUI.CurrentMenu = ETGModGUI.MenuOpened.None;
                    ETGModGUI.UpdateTimeScale();
                    ETGModGUI.UpdatePlayerState();
                }
                else
                {
                    ETGModGUI.CurrentMenu = menu;
                    ETGModGUI.UpdateTimeScale();
                    ETGModGUI.UpdatePlayerState();
                }
            }
        }

        public override void Exit()
        {
        }
    }
}
