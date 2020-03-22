using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;

public class TeleportPortal : MonoBehaviour
{
    public AudioClip soundEffect;
    public AudioClip backgroundMusic
    {
        get
        {
            var music = gameObject?.GetComponentInParentIncludeInactive<RoomInfo>()?.backgroundMusic;
            if (music == null)
            {
                music = gameObject?.GetComponentInParentIncludeInactive<MapInfo>()?.backgroundMusic;
            }
            return music;
        }
    }
    public Vector2[] cameraConfiner
    {
        get
        {
            return gameObject.GetComponentInParentIncludeInactive<RoomInfo>().cameraConfiner;
        }
    }
    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public TeleportInfo teleportInfo;

    public RoomInfo parentRoom {
        get {
            return gameObject.GetComponentInParentIncludeInactive<RoomInfo>();
        }
    }
}