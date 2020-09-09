using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MMX;

public class TeleportExecuter : IExecute
{
    EventAction eventAction;
    private TeleportTargetInfo teleportTargetInfo;

    public bool isTeleporting
    {
        get
        {
            return teleportTargetInfo != null;
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
    public RoomInfo getParentRoom(GameObject gameObject)
    {
        return gameObject.GetComponentInParentIncludeInactive<RoomInfo>();
    }

    public void execute(EventAction eventAction)
    {
        if (isTeleporting)
        {
            Debug.Log("传送中");
            return;
        }

        TeleportPortal teleport = ConvertEventActionToTeleportPortal(eventAction);

        if (teleport == null || teleport.dropZone == null && teleport.teleportInfo.mapName.Length <= 0)
        {
            return;
        }

        EventAction dropZoneEventAction = null;
        if ( teleport.dropZone != null){
            dropZoneEventAction = teleport.dropZone.GetComponent<EventAction>();
        }
        if (teleport.dropZone != null && dropZoneEventAction != null && dropZoneEventAction.type == EventActionType.teleport)
        {
            var targetTeleport = ConvertEventActionToTeleportPortal(dropZoneEventAction);
            this.teleport(new TeleportTargetInfo(getParentRoom(dropZoneEventAction.gameObject), targetTeleport));
        }
        else
        {
            var mapName = teleport.teleportInfo.mapName;
            var roomName = teleport.teleportInfo.roomName;
            var teleportPortalName = teleport.teleportInfo.teleportPortalName;
            if (teleportPortalName != null && teleportPortalName.Length > 0)
            {
                this.teleport(new TeleportTargetInfo(mapName, roomName, teleportPortalName));
            }
            else
            {
                var postion = teleport.teleportInfo.teleportPoint;
                this.teleport(new TeleportTargetInfo(mapName, roomName, postion, null));
            }

        }
    }


    public void teleport(TeleportTargetInfo teleportTargetInfo)
    {
        if (teleportTargetInfo != null)
        {
            MMX.GameManager.transition.transitionInCompletion = () =>
            {
                teleport();
            };
            MMX.GameManager.transition.transitionOutCompletion = () =>
            {
                teleportFinished();
            };
            MMX.GameManager.transition.startScreenFade();
        }
        this.teleportTargetInfo = teleportTargetInfo;
    }
    public void teleportFinished()
    {
        teleportTargetInfo = null;
    }

    public void teleport()
    {
        if (teleportTargetInfo == null)
        {
            MMX.GameManager.transition.endScreenFade();
            return;
        }
        TeleportTargetInfo targetInfo = teleportTargetInfo;
        //切换地图
        if (MMX.GameManager.map == null || targetInfo.mapName != MMX.GameManager.map.name)
        {
            var tempMap = MMX.GameManager.map;
            if (MMX.GameManager.map != null)
            {
                MonoBehaviour.Destroy(tempMap);
            }
            MMX.GameManager.map = MonoBehaviour.Instantiate(targetInfo.map.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            //跨地图传送时，将所有人一起传送
            foreach (var item in MMX.GameManager.Queue.queue)
            {
                item.transform.position = targetInfo.position;
            }
        }
        else
        {
            MMX.GameManager.Queue.captain.gameObject.transform.position = targetInfo.position;
        }

        //播放传送音效
        if (targetInfo.soundEffect != null)
        {
            GameManager.Audio.PlaySfx(targetInfo.soundEffect);
        }


        //播放目标场景背景音乐
        if (targetInfo.backgroundMusic != null)
        {
            if (GameManager.Audio.BgmChannel.clip == null || targetInfo.backgroundMusic.name != GameManager.Audio.BgmChannel.clip.name)
            {
                GameManager.Audio.PlayBgm(targetInfo.backgroundMusic);
            }
        }

        //设置摄像机边界
        var cameraConfiner = targetInfo.cameraConfiner;
        var mainCamera = GameObject.FindWithTag("MainCamera");
        var cinemachineConfiner = mainCamera.GetComponent<CinemachineConfiner>();
        var shape2D = (PolygonCollider2D)cinemachineConfiner.m_BoundingShape2D;

        shape2D.points = cameraConfiner;
        cinemachineConfiner.InvalidatePathCache();

        //设置边界碰撞
        var edgeCollider = GameObject.FindWithTag("EdgeCollider");
        var collider = edgeCollider.GetComponent<EdgeCollider2D>();

        List<Vector2> pointsList = new List<Vector2>();
        pointsList.AddRange(shape2D.points);
        pointsList.Add(shape2D.points[0]);
        collider.points = pointsList.ToArray();

        //亮屏
        MMX.GameManager.transition.endScreenFade();
    }
}


public class TeleportTargetInfo
{
    public string mapName;
    public string roomName;

    public RoomInfo room;
    public MapInfo map;
    public TeleportPortal teleportPortal;

    public Vector2 position;

    public AudioClip soundEffect;
    public AudioClip backgroundMusic;
    public Vector2[] cameraConfiner;


    public TeleportTargetInfo(string mapName, string roomName, string teleportPortalName)
    {
        this.mapName = mapName;
        this.roomName = roomName;

        if (MMX.GameManager.map.name == mapName)
        {
            this.map = MMX.GameManager.map.GetComponent<MapInfo>();
        }
        else
        {
            this.map = Resources.Load<GameObject>("地图/" + mapName).GetComponent<MapInfo>();
        }
        foreach (var item in map.rooms)
        {
            if (item.name == roomName)
            {
                this.room = item;
                break;
            }
        }
        foreach (var item in room.gameObject.GetComponentsInChildren<EventAction>())
        {
            if (item.type == EventActionType.teleport)
            {
                if (item.name == teleportPortalName)
                {
                    this.teleportPortal = TeleportExecuter.ConvertEventActionToTeleportPortal(item);
                }
            }
        }
        this.position = (Vector2)this.teleportPortal.gameObject.transform.position + this.teleportPortal.dropZoneOffset;
        this.soundEffect = this.teleportPortal.soundEffect;
        this.backgroundMusic = this.room.backgroundMusic;
        this.cameraConfiner = this.room.cameraConfiner;
    }
    public TeleportTargetInfo(string mapName, string roomName, Vector2 position, string soundEffect)
    {
        this.mapName = mapName;
        this.roomName = roomName;
        this.position = position;
        if (MMX.GameManager.map == null || MMX.GameManager.map.name != mapName)
        {
            this.map = Resources.Load<GameObject>("地图/" + mapName).GetComponent<MapInfo>();
        }
        else
        {
            this.map = MMX.GameManager.map.GetComponent<MapInfo>();
        }
        foreach (var item in map.rooms)
        {
            if (item.name == roomName)
            {
                this.room = item;
                break;
            }
        }
        this.backgroundMusic = room.backgroundMusic;
        this.cameraConfiner = room.cameraConfiner;
        this.soundEffect = Resources.Load<AudioClip>(soundEffect);
    }

    public TeleportTargetInfo(RoomInfo room, TeleportPortal teleportPortal)
    {
        this.map = room.gameObject.GetComponentInParentIncludeInactive<MapInfo>();
        this.mapName = map.name;
        this.room = room;
        this.roomName = room.name;
        this.teleportPortal = teleportPortal;

        this.position = (Vector2)this.teleportPortal.gameObject.transform.position + teleportPortal.dropZoneOffset;
        this.soundEffect = this.teleportPortal.soundEffect;
        this.backgroundMusic = this.room.backgroundMusic;
        this.cameraConfiner = this.room.cameraConfiner;
    }


}