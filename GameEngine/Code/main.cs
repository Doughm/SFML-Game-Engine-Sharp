//SFML 2D C# Game Engine 1.3
//Programer Douglas Harvey-Marose
//
// - Version Changes 1.31 -
//   added network classes for both TCP and UDP
//   split engine.cs up into smaller organized files to bring it more in line with the C++ version
//   added setSoundOn in addition to toggle in audio
//   added get soundOn to return if the sound is on or off
//
// - Version Changes 1.3 -
//   added the text edit class, alowing you to edit a string with the keyboard
//   changed the string bindings for A-Z keys to make sorting strings easier
//   fixed right bracket key detection
//
// - Version Changes 1.2 -
//   replaced the thread sleep timer with a more accurate timing functionality
//   moved all timing related methods into a class called Timer
//   added the ability to uncap the frame rate with the INI entry FPSCapped
//   added the ability to set the frame rate with the INI entry FPSCap
//   added the ability to turn the sound off or on in the Audio class
//   added a value to store the name of the program
//   added a new methods to audio to control the volume
//   added a new method to LoadINI to get the file name
//   the batchIswithin method now returns more than one entity
//   fixed many small bugs, and cleaned the code up
//   added methods to find if an entity is in an area, or within the window
//
// - Version Changes 1.1 -
//   added in a new class to load in an INI file, and save and load information to it
//   added an class to play audio files
//   added in animation entities, functionality for animating sprites
//   added in a ticker class, which keeps trak of how many loops go by 
//   added custom bounds detection options box and circle
//   added in an input method to detect if a mouse or keyboard button is clicked/tapped
//   added in an input methods to take in only one key or mouse click
//   basic program parameters are now loaded into the system thourgh an INI, rather than hardcoded
//   added rotate around center method
//   changed the text entity to take a single font rather than setting it once per entity
//   changed the deleteAll method so that it deletes everything
//   added method deleteAllEntitys that only deletes entitys
//   fixed minor errors, typos, and bugs in several classes/methods
//
// - Known isues -
//   collison detection for entities is bounding box only, can be inaccurate
//   if the 0,0 spot on a given sprite sheet is any color but white, the rectangle or quad shapes won't show right
//

using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace GameEngine
{
    class main
    {
        static void Main(string[] args)
        {
            Window window = new Window();
            AssetLoader assetLoader = new AssetLoader(window);
            assetLoader.loadBaseAssets();
            GameTimer logicTimer = new GameTimer();
            logicTimer.restartWatch();
            float logicSpeed = 1000 / 45;
            TextEdit edit = new TextEdit(125, 350, 50, "test test test");
            string keyboard;

            while (window.isOpen())
            {
                //logic update
                if (logicTimer.getTimeMilliseconds() >= logicSpeed)
                {
                    keyboard = window.inputKeyboard();
                    window.setText("text1", edit.takeInput(keyboard));

                    //restarts the logic timer 
                    logicTimer.restartWatch();
                }

                //window update
                window.drawAll();
            }
        }
    }
}
