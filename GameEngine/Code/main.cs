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
		            window.setFont("arial.ttf");
		            window.addSpriteSheet("smiley.png");
		            window.addSpriteMap("smiley", new Vector2f(0,1), 95, 98);
		            window.setBackgroundColor(Color.Black);
		            window.addCircle("circle1", new Vector2f(200, 50), Color.Cyan, 50);
		            window.addRectangle("rectangle1" , new Vector2f(50,50), 100, 100, Color.Green);
		            window.addRectangle("rectangle2", new Vector2f(350,50), 100, 100, Color.Green, Color.Blue, Color.Yellow, Color.Magenta);
		            window.addText("text1", new Vector2f(200,200), Color.Red, 30, "test test test");
		            window.addSprite("sprite1", "smiley", new Vector2f(50,200));
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
