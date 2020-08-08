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
public interface InputButtonEventInterface
{
    //当前页面按钮切换事件
    void selectedButtonChanged();
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

    //失去焦点时，当前选中的按钮
    public GameObject currentSelectedGameObjectWhenLostFocus;
    //是否自动在回到焦点时，指针恢复选中上次选中的按钮
    public bool needsResetLastSelectedGameObject = true;
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
        if (needsResetLastSelectedGameObject && currentSelectedGameObjectWhenLostFocus != null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelectedGameObjectWhenLostFocus);
            currentSelectedGameObjectWhenLostFocus = null;
        }
    }
    public virtual void willLostFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
        }
        fingerActive = false;
        inputs.Disable();
        if (needsResetLastSelectedGameObject)
        {
            foreach (var button in this.gameObject.GetComponentsInChildren<UnityEngine.UI.Button>())
            {
                if (button.gameObject == MMX.GameManager.Input.currentSelectedGameObject)
                {
                    currentSelectedGameObjectWhenLostFocus = button.gameObject;
                }
            }
        }
    }
    public void WaifForEndOfFrameAction(System.Action action)
    {
        StartCoroutine(DoWaifForEndOfFrameAction(action));
    }
    public IEnumerator DoWaifForEndOfFrameAction(System.Action action)
    {
        yield return new WaitForSeconds(1);
        action();
    }
}
