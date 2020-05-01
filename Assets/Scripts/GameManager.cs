using System;
using UnityEngine;
using UnityEngine.Events;
using MMX;

namespace MMX
{
    public class GameManager
    {
        public static AudioController Audio => Singleton<AudioController>.Instance;
        public static InputController Input => Singleton<InputController>.Instance;

        public static MainMenuController MainMenu;
        public static GameObject Finger = GameObject.Find("UI").FindObject("Finger");

        public static TeamQueue Queue = TeamQueue.shared;

        public static GameObject map;

        public static void PlayEnterButtonSfx()
        {
            MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        }
        public static void PlayCancelButtonSfx()
        {
            MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        }

    }
}