using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MMX;

public class TeleportController : MonoBehaviour
{
    MoveController actor;
    public bool isTeleporting
    {
        get
        {
            return _teleportPortal != null;
        }
    }

    private TeleportPortal _teleportPortal;
    void Start()
    {
        actor = this.GetComponent<MoveController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting)
        {
            Debug.Log("传送中");
            return;
        }
        var teleport = other.GetComponent<TeleportPortal>();
        if (teleport == null || teleport.dropZone == null && teleport.teleportInfo.mapName.Length <= 0)
        {
            return;
        }
        _teleportPortal = teleport;

        //黑屏，准备转场
        var screenFader = GameObject.FindWithTag("ScreenFader");
        var screenFaderAnimator = screenFader.GetComponent<Animator>();
        screenFaderAnimator.SetTrigger("FadeInTrigger");
    }

    public void teleport()
    {
        if (_teleportPortal == null)
        {
            return;
        }

        TeleportPortal targetTeleport = null;

        if (_teleportPortal.teleportInfo.mapName.Length > 0)
        {
            var map = Resources.Load<GameObject>("地图/" + _teleportPortal.teleportInfo.mapName);
            foreach (var item in map.GetComponent<MapInfo>().rooms)
            {
                foreach (var tel in item.teleportPortals)
                {
                    if (tel.name == _teleportPortal.teleportInfo.teleportPortalName)
                    {
                        Debug.Log("跨地图传送");
                        targetTeleport = tel;
                        Destroy(MMX.GameManager.map);
                        MMX.GameManager.map = map;
                        Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
                        break;
                    }
                }
                if (targetTeleport != null)
                {
                    break;
                }
            }
            if (targetTeleport == null)
            {
                Destroy(map);
            }
        }
        if (targetTeleport == null)
        {
            targetTeleport = _teleportPortal.dropZone.GetComponent<TeleportPortal>();
            this.gameObject.transform.position = (Vector2)targetTeleport.gameObject.transform.position + targetTeleport.dropZoneOffset;
        }
        else
        {
            //跨地图传送时，将所有人一起传送
            foreach (var item in MMX.GameManager.Queue.queue)
            {
                item.transform.position = (Vector2)targetTeleport.gameObject.transform.position + targetTeleport.dropZoneOffset;
            }
        }

        //播放传送音效
        GameManager.Audio.PlaySfx(_teleportPortal.soundEffect);
        
        //播放目标场景背景音乐
        if (targetTeleport.backgroundMusic != null)
        {
            if(GameManager.Audio.BgmChannel.clip == null || targetTeleport.backgroundMusic.name != GameManager.Audio.BgmChannel.clip.name) {
                GameManager.Audio.PlayBgm(targetTeleport.backgroundMusic);
            }
        }

        //设置摄像机边界
        var cameraConfiner = targetTeleport.GetComponent<TeleportPortal>().cameraConfiner;
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
        var screenFader = GameObject.FindWithTag("ScreenFader");
        var screenFaderAnimator = screenFader.GetComponent<Animator>();
        screenFaderAnimator.SetTrigger("FadeOutTrigger");
    }
    public void teleportFinished()
    {
        _teleportPortal = null;
    }
}
