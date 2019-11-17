using System;
using UnityEngine;
using UnityEngine.Events;
using MMX;

namespace MMX
{
    public class GameManager{
        public static AudioController Audio => Singleton<AudioController>.Instance;
        public static InputController Input => Singleton<InputController>.Instance;
    }
}