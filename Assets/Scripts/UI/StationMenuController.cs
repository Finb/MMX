using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ConfirmButtonType
{
    done = 0,
    off,
    on
}

public class StationMenuController : BaseUIInputController
{
    public GameObject yButtonText;
    public GameObject confirmButton;
    public GameObject traction;

    public GameObject leftPanel;
    public GameObject rightPanel;

    //菜单栏左边的角色信息
    List<GameObject> roles = new List<GameObject>();
    //菜单栏右边的车辆信息
    List<GameObject> vehicles = new List<GameObject>();
    List<VehicleInfo> aboardVehicles = new List<VehicleInfo>();

    //角色周围的车辆
    public List<VehicleInfo> aroundVehicles = new List<VehicleInfo>();

    //记录失去焦点时，指针所在的按钮
    GameObject selectedGameObjectAtLostFocus;

    public override void Awake()
    {
        base.Awake();
        inputs.UI.B.performed += ctx => hide();
        inputs.UI.A.performed += ctx =>
        {
            var currentSelectedObj = EventSystem.current.currentSelectedGameObject;
            if (currentSelectedObj == confirmButton)
            {
                confirm();
                //确定
                return;
            }
            if (currentSelectedObj == traction)
            {
                //牵引
                return;
            }
            var index = roles.IndexOf(currentSelectedObj);
            if (index < 0)
            {
                return;
            }
            var boxController = SelectButtonBoxController.Create();
            foreach (var vehicle in aroundVehicles)
            {
                boxController.addButton(vehicle.vehicleName, vehicle.gameObject.GetComponent<SpriteRenderer>().sprite, () =>
                {
                    setVehicles(index, vehicle);
                });
            }
            boxController.addButton("不乘坐", null, () =>
               {
                   setVehicles(index, null);
               });
            boxController.show();

        };
    }

    private ConfirmButtonType confirmButtonType
    {
        set
        {
            switch (value)
            {
                case ConfirmButtonType.done:
                    confirmButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "就这样";
                    break;
                case ConfirmButtonType.off:
                    confirmButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "全部下车";
                    break;
                case ConfirmButtonType.on:
                    confirmButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "全部上车";
                    break;

            }
        }
    }

    private void confirm()
    {
        //验证是否超载
        Dictionary<int, int> passengerCountDict = new Dictionary<int, int>();
        foreach (var item in aboardVehicles)
        {
            if (item == null) continue;

            if (passengerCountDict.ContainsKey(item.id))
            {
                passengerCountDict[item.id] += 1;
            }
            else
            {
                passengerCountDict[item.id] = 1;
            }
        }
        foreach (var item in aboardVehicles)
        {
            if (item == null) continue;
            var passengerCount = passengerCountDict[item.id];
            if (passengerCount > item.busload)
            {
                //超载
                DialogController.create().showText(item.vehicleName + "最多\n只能乘坐" + item.busload + "人");
                return;
            }
        }

        aboardVehicles.ForEach(vehicle =>
        {
            if (vehicle != null)
            {
                vehicle.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        });
        TeamQueue.shared.enterVehicle(aboardVehicles);
        // DialogController.create().showText("只有狗\n是无法驾驶战车的");
        hide();
    }
    private void done()
    {
        EventSystem.current.SetSelectedGameObject(confirmButton);
        confirmButtonType = ConfirmButtonType.done;
    }

    public void addRoleInfo(RoleInfo roleInfo)
    {
        var button = Resources.Load<GameObject>("UI/prefabs/IconButton");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = roleInfo.name;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredWidth = 180;
        button.GetComponent<UnityEngine.UI.LayoutElement>().preferredHeight = 40;
        button.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = roleInfo.gameObject.GetComponent<SpriteRenderer>().sprite;

        var obj = Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("LeftPanel").transform);
        //牵引按钮排在最后面
        obj.transform.SetSiblingIndex(obj.transform.GetSiblingIndex() - 1);

        roles.Add(obj);
    }

    public void AddVehicleInfo(string name)
    {
        var iconTextImage = Resources.Load<GameObject>("UI/prefabs/IconTextImage");
        iconTextImage.GetComponentInChildren<UnityEngine.UI.Text>().text = name;
        iconTextImage.FindObject("Image").GetComponent<UnityEngine.UI.Image>().enabled = false;
        var obj = Instantiate(iconTextImage, Vector3.zero, Quaternion.identity, gameObject.FindObject("RightPanel").transform);
        vehicles.Add(obj);
    }

    private void setVehicles(int index, VehicleInfo vehicle)
    {
        vehicles[index].GetComponentInChildren<UnityEngine.UI.Text>().text = vehicle?.vehicleName ?? "无乘坐";
        vehicles[index].FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = vehicle?.gameObject.GetComponent<SpriteRenderer>().sprite;
        vehicles[index].FindObject("Image").GetComponent<UnityEngine.UI.Image>().enabled = vehicle != null;

        aboardVehicles[index] = vehicle;

        done();
    }


    #region delegate
    public override void willOnFocus()
    {
        base.willOnFocus();
        if (selectedGameObjectAtLostFocus != null)
        {
            var colors = selectedGameObjectAtLostFocus.GetComponent<UnityEngine.UI.Button>().colors;
            colors.disabledColor = new Color(200 / 255, 200 / 255, 200 / 255, 128 / 255);
            selectedGameObjectAtLostFocus.GetComponent<UnityEngine.UI.Button>().colors = colors;

            EventSystem.current.SetSelectedGameObject(selectedGameObjectAtLostFocus);
            selectedGameObjectAtLostFocus = null;
        }
    }

    public override void willLostFocus()
    {
        base.willLostFocus();
        selectedGameObjectAtLostFocus = MMX.GameManager.Input.currentSelectedGameObject;
        var colors = selectedGameObjectAtLostFocus.GetComponent<UnityEngine.UI.Button>().colors;
        colors.disabledColor = new Color(1, 1, 1, 1);
        selectedGameObjectAtLostFocus.GetComponent<UnityEngine.UI.Button>().colors = colors;
    }
    #endregion

    #region Control Method
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

    public static StationMenuController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/StationMenu"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());

        //添加角色行
        var stationMenu = obj.GetComponent<StationMenuController>();
        foreach (var item in TeamQueue.shared.humans)
        {
            stationMenu.addRoleInfo(item.GetComponent<RoleInfo>());
        }

        //生成对应的 right button
        for (var i = 0; i < stationMenu.leftPanel.transform.childCount - 2; i++)
        {
            stationMenu.AddVehicleInfo("无乘坐");

            //添加相应数量的空位，用于存放选择好的车辆
            stationMenu.aboardVehicles.Add(null);
        }
        stationMenu.AddVehicleInfo("不牵引");


        //查找周围的车辆信息
        Collider2D[] colliders = Physics2D.OverlapCircleAll(MMX.GameManager.Queue.captain.transform.position, 2, 1 << 8);
        foreach (var collider in colliders)
        {
            var info = collider.GetComponent<VehicleInfo>();
            if (info != null)
            {
                stationMenu.aroundVehicles.Add(info);
            }
        }

        return stationMenu;

    }
    #endregion

}