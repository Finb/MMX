using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;


public interface InputEventInterface {
    void inputAction();
    void willOnFocus();
    void willLostFocus();
}

public abstract class BaseInputController : MonoBehaviour, InputEventInterface
{
    public abstract void inputAction();
   public virtual void willOnFocus(){
       Debug.Log(this);
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = true;
        }
    }
   public virtual void willLostFocus() {
       foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
        }
   }
}
