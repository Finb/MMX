using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [Tooltip("接受输入操作的目标，数组最后一位就是当前接收操作的Target")]
    public List<GameObject> targets = new List<GameObject>();
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
    public void setRootTarget(GameObject target)
    {
        if (targets.Count > 0)
        {
            targets.RemoveAt(0);
        }
        targets.Insert(0, target);
    }
    public void pushTarget(GameObject target)
    {
        var oldTarget = currentTarget;
        targets.Add(target);
        invokeWillLostFocus(oldTarget);
        invokeWillOnFocus(currentTarget);
    }
    public void popTarget()
    {
        var oldTarget = currentTarget;
        targets.RemoveAt(targets.Count - 1);
        invokeWillLostFocus(oldTarget);
        invokeWillOnFocus(currentTarget);
    }
    private void Start()
    {
    }
    private void Update()
    {
        invokeInputAction(currentTarget);
    }

    private void invokeInputAction(GameObject target)
    {
        if (target == null)
        {
            return;
        }
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.inputAction();
        }
    }
    private void invokeWillLostFocus(GameObject target)
    {
        if (target == null)
        {
            return;
        }
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.willLostFocus();
        }
    }
    private void invokeWillOnFocus(GameObject target)
    {
        if (target == null)
        {
            return;
        }
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.willOnFocus();
        }
    }
}
