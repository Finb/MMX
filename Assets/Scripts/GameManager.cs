using System;
using UnityEngine;
using UnityEngine.Events;
using MMX;

namespace MMX
{
    public class GameManager{
        public static AudioController Audio => Singleton<AudioController>.Instance;
        public static InputController Input => Singleton<InputController>.Instance;

        public static MainMenuController MainMenu;
        public static GameObject Finger = GameObject.Find("UI").FindObject("Finger");

        public static TeamQueue Queue = TeamQueue.shared;

        public static GameObject map;

    }
}