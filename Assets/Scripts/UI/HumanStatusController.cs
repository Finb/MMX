using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HumanStatusController : BaseUIInputController
{

    public static HumanStatusController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/HumanStatusView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<HumanStatusController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        WaifForEndOfFrameAction(()=> {
            Debug.Log("tttttttttttttt");
            Debug.Log(gameObject.FindObject("EquipmentSelectedButton"));
            EventSystem.current.SetSelectedGameObject(gameObject.FindObject("EquipmentSelectedButton"));
        });
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
