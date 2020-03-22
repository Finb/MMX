using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public AudioClip backgroundMusic;

    public RoomInfo[] rooms {
        get {
            return gameObject.GetComponentsInChildren<RoomInfo>();
        }
    }
}

[System.Serializable]
public class TeleportInfo {
    //传送到哪个地图，需传 prefab名称
    public string mapName;
    //传送到哪个传送点，需传 传送点名称
    public string roomName;
    public string teleportPortalName;
    //传送到地图的哪个点，传一个地图的点坐标
    public Vector2 teleportPoint;
}