using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public interface InputEventInterface {
    // 按键事件
    void inputAction();
    // 将要获得焦点
    void willOnFocus();
    // 失去焦点
    void willLostFocus();
}

public interface PresentViewInterface {
    void show();
    void hide();
}

public abstract class BaseInputController : MonoBehaviour, InputEventInterface
{
    public abstract void inputAction();
   public virtual void willOnFocus(){
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = true;
            MMX.GameManager.Finger.SetActive(true);
        }
    }
   public virtual void willLostFocus() {
       foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
            MMX.GameManager.Finger.SetActive(false);
        }
   }
}
