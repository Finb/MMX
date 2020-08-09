using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HumanStatusController : BaseUIInputController
{

    public override void Awake()
    {
        base.Awake();
        inputs.UI.B.performed += ctx => hide();

    }
    public static HumanStatusController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/HumanStatusView"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<HumanStatusController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
