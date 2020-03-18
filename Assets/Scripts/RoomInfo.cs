using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomEnemyInfo {
    public string name2;
    public string test2;    
}

public class RoomInfo : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public Vector2[] cameraConfiner = new Vector2[4];

    public TeleportPortal[] teleportPortals {
        get {
            return GetComponentsInChildren<TeleportPortal>();
        }
    }
}
