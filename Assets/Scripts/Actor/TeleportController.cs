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
        if (teleport != null && teleport.dropZone != null)
        {
            Debug.Log("传送");
            _teleportPortal = teleport;

            var screenFader = GameObject.FindWithTag("ScreenFader");
            var screenFaderAnimator = screenFader.GetComponent<Animator>();
            screenFaderAnimator.SetTrigger("FadeInTrigger");

            //黑屏
        }
    }

    public void teleport()
    {
        //设置摄像机边界
        if (_teleportPortal == null)
        {
            return;
        }

        var teleport = _teleportPortal;
        
        //播放音效
        GameManager.Audio.PlaySfx(teleport.soundEffect);
        if (teleport.backgroundMusic != null){
            GameManager.Audio.PlayBgm(teleport.backgroundMusic);
        }

        var cameraConfiner = teleport.dropZone.GetComponent<TeleportPortal>().cameraConfiner;
        var mainCamera = GameObject.FindWithTag("MainCamera");
        var cinemachineConfiner = mainCamera.GetComponent<CinemachineConfiner>();
        var shape2D = (PolygonCollider2D)cinemachineConfiner.m_BoundingShape2D;

        shape2D.points = cameraConfiner;
        cinemachineConfiner.InvalidatePathCache();

        //设置边界碰撞
        var obj2 = GameObject.FindWithTag("EdgeCollider");
        var collider = obj2.GetComponent<EdgeCollider2D>();

        List<Vector2> pointsList = new List<Vector2>();
        pointsList.AddRange(shape2D.points);
        pointsList.Add(shape2D.points[0]);
        collider.points = pointsList.ToArray();
        this.gameObject.transform.position = (Vector2)teleport.dropZone.position + teleport.dropZoneOffset;

        var screenFader = GameObject.FindWithTag("ScreenFader");
        var screenFaderAnimator = screenFader.GetComponent<Animator>();
        screenFaderAnimator.SetTrigger("FadeOutTrigger");
        
    }
    public void teleportFinished(){
        _teleportPortal = null;
    }
}
