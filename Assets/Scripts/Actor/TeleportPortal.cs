using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;

public class TeleportPortal
{
    // 传送音效
    public AudioClip soundEffect;

    // 传送目标点
    public Transform dropZone;

    // 传送后偏移量
    public Vector2 dropZoneOffset;

    //传送点自身位置
    public GameObject gameObject;

    // 没有传送点时，动态在运行时去寻找的传送信息
    public TeleportInfo teleportInfo;

    // 传送目标点事件
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