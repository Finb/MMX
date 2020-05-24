using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public interface InputEventInterface
{
    // 将要获得焦点
    void willOnFocus();
    // 失去焦点
    void willLostFocus();
}

public interface PresentViewInterface
{
    void show();
    void hide();
}

public abstract class BaseUIInputController : MonoBehaviour, InputEventInterface
{
    public InputControls inputs;
    public bool needsShowFinger = true;
    public bool fingerActive
    {
        set
        {
            if (!needsShowFinger)
            {
                return;
            }
            MMX.GameManager.Finger.SetActive(value);
        }
        get
        {
            return MMX.GameManager.Finger.activeSelf;
        }
    }
    public virtual void Awake()
    {
        inputs = new InputControls();
        inputs.Disable();
    }
    public virtual void willOnFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = true;
        }
        fingerActive = true;
        inputs.Enable();
    }
    public virtual void willLostFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
        }
        fingerActive = false;
        inputs.Disable();
    }
    public void WaifForEndOfFrameAction(System.Action action)
    {
        StartCoroutine(DoWaifForEndOfFrameAction(action));
    }
    public IEnumerator DoWaifForEndOfFrameAction(System.Action action){
        yield return new WaitForEndOfFrame();
        action();
    }
}
