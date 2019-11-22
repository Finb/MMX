using UnityEngine;
using System.Collections.Generic;
namespace MMX.Input
{
    public enum GameButton
    {
        None,
        A,
        B,
        X,
        Y,
        Start,
        Select,
        Up,
        Down,
        Left,
        Right
    }

    public class GameButtonPressRecognition
    {
        public static bool getKeyDown(GameButton gameButton)
        {
            switch (gameButton)
            {
                case GameButton.A:
                    return UnityEngine.Input.GetKeyDown(KeyCode.J);
                case GameButton.B:
                    return UnityEngine.Input.GetKeyDown(KeyCode.K);
                case GameButton.X:
                    return UnityEngine.Input.GetKeyDown(KeyCode.U);
                case GameButton.Y:
                    return UnityEngine.Input.GetKeyDown(KeyCode.Y);
            }
            return false;
        }
    }
}
