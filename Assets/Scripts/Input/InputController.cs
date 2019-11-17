using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [Tooltip("接受输入操作的目标，数组最后一位就是当前接收操作的Target")]
    public List<GameObject> targets;
    public GameObject currentTarget
    {
        get
        {
            if (targets.Count > 0)
            {
                return targets[targets.Count - 1];
            }
            return null;
        }
    }

    public void pushTarget(GameObject target){
        targets.Add(target);
    }
    public void popTarget(){
        targets.RemoveAt(targets.Count -1);
    }
    private void Start()
    {
        targets = new List<GameObject>();
        targets.Add(GameObject.FindGameObjectWithTag("Player"));
    }
    private void Update() {
        currentTarget.SendMessage("invokeInputAction",SendMessageOptions.DontRequireReceiver);
    }
}
