using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MMX;
using System;

public class TeleportExecuter : IExecute
{
    public void execute(EventAction eventAction)
    {
        TeleportPortal teleport = TeleportPortal.ConvertEventActionToTeleportPortal(eventAction);
        this.teleport(teleport);

    }
    public void teleport(TeleportPortal teleport)
    {
        if (teleport == null || teleport.dropZone == null && teleport.teleportInfo.mapName.Length <= 0)
        {
            return;
        }

        //传送到当前地图的目标传送点
        if (teleport.dropZone != null && teleport.dropZoneEventAction != null && teleport.dropZoneEventAction.type == EventActionType.teleport)
        {
            this.teleport(new TeleportTargetInfo(teleport));
        }
        else
        {
            //传送到指定地图
            var mapName = teleport.teleportInfo.mapName;
            var roomName = teleport.teleportInfo.roomName;
            var teleportPortalName = teleport.teleportInfo.teleportPortalName;
            if (teleportPortalName != null && teleportPortalName.Length > 0)
            {
                this.teleport(new TeleportTargetInfo(mapName, roomName, teleportPortalName, teleport.soundEffect));
            }
            else
            {
                var postion = teleport.teleportInfo.teleportPoint;
                this.teleport(new TeleportTargetInfo(mapName, roomName, postion, teleport.soundEffect));
            }
        }
    }


    public void teleport(TeleportTargetInfo teleportTargetInfo)
    {
        TeleportManager.shared.teleport(teleportTargetInfo);
    }
}


public class TeleportTargetInfo
{
    public string mapName;
    public string roomName;

    public RoomInfo room;
    public MapInfo map;

    public Vector2 position;

    public AudioClip soundEffect;
    public AudioClip backgroundMusic;
    public Vector2[] cameraConfiner;


    public TeleportTargetInfo(string mapName, string roomName, string teleportPortalName, AudioClip soundEffect)
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
                    var teleportPortal = TeleportPortal.ConvertEventActionToTeleportPortal(item);
                    this.position = (Vector2)teleportPortal.gameObject.transform.position + teleportPortal.dropZoneOffset;
                }
            }
        }

        this.soundEffect = soundEffect;
        this.backgroundMusic = this.room.backgroundMusic;
        this.cameraConfiner = this.room.cameraConfiner;
    }
    public TeleportTargetInfo(string mapName, string roomName, Vector2 position, AudioClip soundEffect)
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
        this.soundEffect = soundEffect;
    }

    public TeleportTargetInfo(TeleportPortal teleportPortal)
    {
        var room = RoomInfo.getParentRoom(teleportPortal.dropZone.gameObject);

        this.map = room.gameObject.GetComponentInParentIncludeInactive<MapInfo>();
        this.mapName = map.name;
        this.room = room;
        this.roomName = room.name;

        this.position = (Vector2)teleportPortal.dropZone.transform.position + teleportPortal.dropZoneTeleportPortal.dropZoneOffset;
        this.soundEffect = teleportPortal.soundEffect;
        this.backgroundMusic = this.room.backgroundMusic;
        this.cameraConfiner = this.room.cameraConfiner;
    }


}


public class TeleportManager
{
    private TeleportManager() { }
    private static readonly Lazy<TeleportManager> Instancelock = new Lazy<TeleportManager>(() => new TeleportManager());
    public static TeleportManager shared
    {
        get
        {
            return Instancelock.Value;
        }
    }

    public void teleport(TeleportTargetInfo teleportTargetInfo)
    {
        if (teleportTargetInfo == null)
        {
            return;
        }

        //屏幕还是黑的,没准备好不允许传送。
        if (!MMX.GameManager.transition.isReady)
        {
            return;
        }

        MMX.GameManager.transition.transitionInCompletion = () =>
        {
            doTeleport(teleportTargetInfo);
        };
        MMX.GameManager.transition.startScreenFade();
    }

    private void doTeleport(TeleportTargetInfo teleportTargetInfo)
    {


        TeleportTargetInfo targetInfo = teleportTargetInfo;

        //切换地图
        if (MMX.GameManager.map == null || targetInfo.mapName != MMX.GameManager.map.name)
        {
            Debug.Log("ttt " + targetInfo.mapName + "  " + MMX.GameManager.map?.name);

            var tempMap = MMX.GameManager.map;
            if (MMX.GameManager.map != null)
            {
                MonoBehaviour.Destroy(tempMap.gameObject);
            }
            MMX.GameManager.map = MonoBehaviour.Instantiate(targetInfo.map.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            MMX.GameManager.map.name = targetInfo.mapName;
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