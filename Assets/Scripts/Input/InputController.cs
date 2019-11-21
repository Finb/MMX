﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputEventInterface
{
    void inputAction();
    void willOnFocus();
    void willLostFocus();
}

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
        targets = new List<GameObject>();
        targets.Add(GameObject.FindGameObjectWithTag("Player"));
    }
    private void Update()
    {
        invokeInputAction(currentTarget);
    }

    private void invokeInputAction(GameObject target)
    {
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.inputAction();
        }
    }
    private void invokeWillLostFocus(GameObject target){
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.willLostFocus();
        }
    }
    private void invokeWillOnFocus(GameObject target){
        var components = target.GetComponentsInChildren<InputEventInterface>();
        foreach (var item in components)
        {
            item.willOnFocus();
        }
    }
}
