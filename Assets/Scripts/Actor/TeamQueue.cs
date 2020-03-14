using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamQueue: MonoBehaviour
{
    public static TeamQueue shared = new TeamQueue();
    //当前队列对象
    List<GameObject> queue = new List<GameObject>();

    //当前队列人类
    GameObject[] humans;

    // 拖车
    GameObject trailer;

    private TeamQueue()
    {
        buildFolloweChain();
    }
    //构建队伍follow链
    public void buildFolloweChain()
    {
        var player = Instantiate(Resources.Load<GameObject>("Role/主角"));
        enqueue(player);
        player.AddComponent<TeleportController>();
        player.name = "no1";
        MMX.GameManager.Input.pushTarget(player);
        var mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;

        GameObject.FindWithTag("ScreenFader").GetComponent<TransitionController>().messageReceiver = player;

        var player2 = Instantiate(Resources.Load<GameObject>("Role/主角"));
        enqueue(player2);
    }
    //进队
    public bool enqueue(GameObject obj)
    {
        return enqueue(queue.Count,obj);
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
            queue.Insert(0,obj);
        }
        else if (index == 0){
            movement = queue[0].GetComponent<Movement>();
            queue.Insert(0,obj);
            
        }
        else {
            queue[index - 1].GetComponent<Movement>().followerMovement = movement;
            queue.Insert(index,obj);
        }

        return true;
    }
    //出队
    public bool dequeue(GameObject obj)
    {
        var index = queue.FindIndex(item => item == obj);
        if (index > 0){
            queue[index - 1].GetComponent<Movement>().followerMovement = null;
        }
        if (index < queue.Count - 1){
            queue[index - 1].GetComponent<Movement>().followerMovement = queue[index + 1].GetComponent<Movement>();
        }
        queue.Remove(obj);
        return true;
    }
}
