using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;

public class TeleportPortal
{
    public AudioClip soundEffect;

    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public GameObject gameObject;
    public TeleportInfo teleportInfo;

    public EventAction dropZoneEventAction
    {
        get
        {
            return dropZone.GetComponent<EventAction>();
        }
    }

    public TeleportPortal dropZoneTeleportPortal {
        get {
            return ConvertEventActionToTeleportPortal(this.dropZoneEventAction);
        }
    }

    public static TeleportPortal ConvertEventActionToTeleportPortal(EventAction eventAction)
    {
        TeleportPortal teleport = new TeleportPortal();
        teleport.dropZone = eventAction.transformVar1;
        teleport.dropZoneOffset = eventAction.vector2Var1;
        teleport.soundEffect = eventAction.audioClipVar1;
        if (eventAction.stringVar1.Length > 0)
        {
            teleport.teleportInfo = new TeleportInfo();
            teleport.teleportInfo.mapName = eventAction.stringVar1;
            teleport.teleportInfo.roomName = eventAction.stringVar2;
            teleport.teleportInfo.teleportPortalName = eventAction.stringVar3;
            teleport.teleportInfo.teleportPoint = eventAction.vector2Var2;
        }
        teleport.gameObject = eventAction.gameObject;
        return teleport;
    }
}

[System.Serializable]
public class TeleportInfo
{
    //传送到哪个地图，需传 prefab名称
    public string mapName;
    //传送到哪个传送点，需传 传送点名称
    public string roomName;
    public string teleportPortalName;
    //传送到地图的哪个点，传一个地图的点坐标
    public Vector2 teleportPoint;
}