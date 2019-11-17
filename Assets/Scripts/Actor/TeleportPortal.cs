using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;

public class TeleportPortal : MonoBehaviour
    {
        public AudioClip soundEffect;
        public AudioClip backgroundMusic;
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        public int dropZoneSteps;
        public GameButton dropZoneLookAt = GameButton.None;

        public Vector2[] cameraConfiner = new Vector2[4];

        [HideInInspector] public Vector2 calculatedDropZone;
        [HideInInspector] public Vector2 calculatedDropZoneLookAt;
}