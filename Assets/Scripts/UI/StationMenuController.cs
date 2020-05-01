using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StationMenuController : BaseUIInputController
{
    public GameObject yButtonText;
    public GameObject confirmButton;
    public GameObject traction;

    public GameObject leftPanel;
    public GameObject rightPanel;

    public override void Awake()
    {
        base.Awake();
        inputs.UI.B.performed += ctx => hide();
    }

    public static StationMenuController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/StationMenu"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());

        Collider2D[] colliders = Physics2D.OverlapCircleAll(MMX.GameManager.Queue.captain.transform.position, 2, 1 << 8);

        List<VehicleInfo> vehicles = new List<VehicleInfo>();
        foreach (var collider in colliders)
        {
            var info = collider.GetComponent<VehicleInfo>();
            if (info != null)
            {
                vehicles.Add(info);
            }
        }
        Debug.Log(vehicles.Count);

        //添加角色行



        var stationMenu = obj.GetComponent<StationMenuController>();
        foreach (var item in TeamQueue.shared.queue){
            stationMenu.addRoleInfo(item.GetComponent<RoleInfo>());
            stationMenu.AddVehicleInfo(item.GetComponent<RoleInfo>());
        }
        stationMenu.AddVehicleInfo(TeamQueue.shared.queue[0].GetComponent<RoleInfo>());
        return stationMenu;

    }

    public void addRoleInfo(RoleInfo roleInfo){
        var button = Resources.Load<GameObject>("UI/prefabs/IconButton");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = roleInfo.name;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = 180;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredHeight = 40;
        button.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = roleInfo.gameObject.GetComponent<SpriteRenderer>().sprite;

        var obj = Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("LeftPanel").transform);
        //牵引按钮排在最后面
        obj.transform.SetSiblingIndex(obj.transform.GetSiblingIndex() - 1);
    }

    public void AddVehicleInfo(RoleInfo vehicleInfo){
        var iconTextImage = Resources.Load<GameObject>("UI/prefabs/IconTextImage");
        iconTextImage.GetComponentInChildren<UnityEngine.UI.Text>().text = vehicleInfo.name;
        iconTextImage.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = vehicleInfo.gameObject.GetComponent<SpriteRenderer>().sprite;

        var obj = Instantiate(iconTextImage, Vector3.zero, Quaternion.identity, gameObject.FindObject("RightPanel").transform);
    }


    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("LeftPanel").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("RightPanel").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("BorderImage").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("Panel").GetComponent<RectTransform>());

        EventSystem.current.SetSelectedGameObject(confirmButton);
    }
    public void hide()
    {
        MMX.GameManager.PlayCancelButtonSfx();
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
