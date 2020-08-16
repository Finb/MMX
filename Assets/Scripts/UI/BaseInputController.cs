using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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

    public GameObject currentSelectedGameObject {
        get {
            return EventSystem.current.currentSelectedGameObject;
        }
    }

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

    //界面第一次出现时，默认选中的按钮
    GameObject _defaultSelectedGameObjecteAtFirst;
    GameObject defaultSelectedGameObjecteAtFirst
    {
        get
        {
            if (_defaultSelectedGameObjecteAtFirst != null)
            {
                return _defaultSelectedGameObjecteAtFirst;
            }
            var obj = gameObject.GetComponentInChildren<ButtonSelectionChangedController>()?.gameObject;
            return obj;
        }
        set
        {
            if (_defaultSelectedGameObjecteAtFirst != value)
            {
                _defaultSelectedGameObjecteAtFirst = value;
            }
        }
    }
    //UI是否第一次出现
    bool isFirstAppear = true;
    private void OnGUI()
    {
        if (isFirstAppear)
        {
            isFirstAppear = false;
            layout();
        }
    }
    public void layout()
    {
        //处理好所有的 layout group 布局.
        List<Transform> transList = GetAllLayoutGroupChilds(gameObject.transform);
        //Unity 必须从 子控件 往 父控件方向 重建布局。
        transList.Reverse();
        foreach (Transform trans in transList)
        {
            RectTransform rectTrans = trans.GetComponent<RectTransform>();
            if (null != rectTrans)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTrans);
            }
        }
        //设置默认选中按钮
        var selectedGameobject = this.defaultSelectedGameObjecteAtFirst;
        if (selectedGameobject != null)
        {
            EventSystem.current.SetSelectedGameObject(selectedGameobject);
        }
    }
    List<Transform> GetAllLayoutGroupChilds(Transform trans, List<Transform> transList = null)
    {
        if (null == transList)
        {
            transList = new List<Transform>();
        }
        if (null != trans)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                Transform child = trans.GetChild(i);
                if (null != child.GetComponent<LayoutGroup>())
                {
                    transList.Add(child);
                }
                GetAllLayoutGroupChilds(child, transList);
            }
        }
        return transList;
    }

    public virtual void Awake()
    {
        inputs = new InputControls();
        inputs.Disable();
    }
    public virtual void willOnFocus()
    {
        foreach (var item in GetComponentsInChildren<ButtonSelectionChangedController>())
        {
            if  (item.active ){
                item.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            
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
        yield return new WaitForEndOfFrame();
        action();
    }
}