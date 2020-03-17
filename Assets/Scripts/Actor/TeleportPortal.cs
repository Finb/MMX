using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;

public class TeleportPortal : MonoBehaviour
    {
        public AudioClip soundEffect;
        public AudioClip backgroundMusic {
            get {
                return dropZone.gameObject.GetComponentInParent<RoomInfo>().backgroundMusic;
            }
        }
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        public int dropZoneSteps;
        public GameButton dropZoneLookAt = GameButton.None;

        public Vector2[] cameraConfiner {
            get {
                return dropZone.gameObject.GetComponentInParent<RoomInfo>().cameraConfiner;
            }
        }

        [HideInInspector] public Vector2 calculatedDropZone;
        [HideInInspector] public Vector2 calculatedDropZoneLookAt;
}