using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamQueue 
{
    public static TeamQueue shared = new TeamQueue();
    //当前队列对象
    public List<GameObject> queue = new List<GameObject>();

    //当前队列人类
    List<GameObject> humans = new List<GameObject>();
    
    // 拖车
    GameObject trailer;

    public GameObject captain
    {
        get
        {
            if (queue.Count > 0)
            {
                return queue[0];
            }
            return null;
        }
    }

    private TeamQueue()
    {
        humans.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Role/主角")));
        humans.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Role/主角")));
        humans.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Role/主角")));
        humans.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Role/主角")));
        buildFollowChain();
    }
    //构建队伍follow链
    public void buildFollowChain()
    {
        resetQueue();
        foreach (GameObject player in humans)
        {
            enqueue(player);
        }
    }
    public void resetQueue(){
        //重置queue队列中的数据

        //清空 TeleportController 
        foreach(var item in queue){
            MonoBehaviour.Destroy(item.GetComponent<TeleportController>());
        }
        //移除所有队员
        queue.RemoveRange(0,queue.Count);
    }

    public void setupCaptain(GameObject player)
    {
        player.AddComponent<TeleportController>();

        MMX.GameManager.Input.setRootTarget(player);

        var mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
        GameObject.FindWithTag("ScreenFader").GetComponent<TransitionController>().messageReceiver = player;
    }
    public void setupMember(GameObject player)
    {
        //清空传送脚本
        MonoBehaviour.Destroy(player.GetComponent<TeleportController>());
    }

    //进队
    public bool enqueue(GameObject obj)
    {
        return enqueue(queue.Count, obj);
    }
    public bool enqueue(int index, GameObject obj)
    {
        var movement = obj.GetComponent<Movement>();
        if (movement == null)
        {
            return false;
        }

        index = System.Math.Max(0, System.Math.Min(index, queue.Count));

        if (queue.Count <= 0)
        {
            queue.Insert(0, obj);
            setupCaptain(obj);
        }
        else
        {
            queue[index - 1].GetComponent<Movement>().followerMovement = movement;
            if (index < queue.Count)
            {
                movement.followerMovement = queue[index].GetComponent<Movement>();
            }
            queue.Insert(index, obj);
            setupMember(obj);
        }

        return true;
    }
    //出队
    public bool dequeue(GameObject obj)
    {
        var index = queue.FindIndex(item => item == obj);
        if (index > 0)
        {
            queue[index - 1].GetComponent<Movement>().followerMovement = null;
            if (index < queue.Count - 1)
            {
                queue[index - 1].GetComponent<Movement>().followerMovement = queue[index + 1].GetComponent<Movement>();
            }
        }
        else
        {
            setupCaptain(queue[index + 1]);
        }
        queue.Remove(obj);
        return true;
    }
}
