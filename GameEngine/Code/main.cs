using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace GameEngine
{
    class Program
    {
        Window window = new Window();
        Audio audio = new Audio();

        private void assetLoader(string asset)
        {
            switch (asset)
            {
                //test shapes
                case "test":
                    window.setBackgroundColor(Color.Black);
                    window.addRectangle("testShape1" , new Vector2f(50, 50), 100, 100, Color.Green);
                    window.addCircle("testShape2", new Vector2f(200, 50), Color.Cyan, 50);
                    window.addRectangle("testShape3", new Vector2f(350, 50), 100, 100, Color.Green, Color.Blue, Color.Yellow, Color.Magenta);
                    break;
            }
        }

        public void start()
        {
            assetLoader("test");
            GameTimer logicTimer = new GameTimer();
            logicTimer.restartWatch();
            float logicSpeed = 1000 / 45;

            while (window.isOpen())
            {
                //logic update
                if (logicTimer.getTimeMilliseconds() >= logicSpeed)
                { 
                
                }

                //window update
                window.drawAll();
            }
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            Program game = new Program();
            game.start();
        }
    }
}
