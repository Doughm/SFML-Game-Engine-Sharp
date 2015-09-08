using System;
using SFML.Window;
using SFML.Graphics;

namespace GameEngine
{
    class AssetLoader
    {
        private Window window;

        public AssetLoader(Window passedWindow)
        {
            window = passedWindow;
        }

        //loads in the staring assests
        public void loadBaseAssets()
        {
            window.setFont("arial.ttf");
            window.addSpriteSheet("smiley.png");
            window.addSpriteMap("smiley", new Vector2f(0, 1), 95, 98);
            window.setBackgroundColor(Color.Black);
            window.addCircle("circle1", new Vector2f(200, 50), Color.Cyan, 50);
            window.addRectangle("rectangle1", new Vector2f(50, 50), 100, 100, Color.Green);
            window.addRectangle("rectangle2", new Vector2f(350, 50), 100, 100, Color.Green, Color.Blue, Color.Yellow, Color.Magenta);
            window.addText("text1", new Vector2f(200, 200), Color.Red, 30, "test test test");
            window.addSprite("sprite1", "smiley", new Vector2f(50, 200));
        }
    }
}
