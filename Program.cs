using System;
using SplashKitSDK;

namespace SplashKitSDK
{
    public class Program
    {
        public static void Main()
        {
            Window window;
            window = new Window("Shape Dodge", 1000, 800);
            Player player = new Player(window);
            RobotDodge robotDodge = new RobotDodge(window, player);

            do
            {
                SplashKit.ProcessEvents();
                robotDodge.HandleInput(window);
                robotDodge.StayOnWindow(window);
                robotDodge.Update();
                robotDodge.Draw(window);
            }
            while (!window.CloseRequested && !player.Quit);

            if (player.Quit)
            {
                robotDodge.GameOver(window);
            }

        }
    }
}
