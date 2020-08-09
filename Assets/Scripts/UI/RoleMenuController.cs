using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoleMenuController : BaseUIInputController
{
    public GameObject humanPanel;
    public GameObject vehiclePanel;
    public System.Action selectRoleCompletionAction;
    public override void Awake()
    {
        base.Awake();
        inputs.UI.A.performed += ctx =>
        {
            if(this.selectRoleCompletionAction != null){
                this.selectRoleCompletionAction();
            }
        };
        inputs.UI.B.performed += ctx => hide();


        //清除多余的控件
        List<GameObject> destoryGameObject = new List<GameObject>();
        for (var i = TeamQueue.shared.humans.Count; i < humanPanel.transform.childCount; i++)
        {
            destoryGameObject.Add(humanPanel.transform.GetChild(i).gameObject);
            destoryGameObject.Add(vehiclePanel.transform.GetChild(i).gameObject);
        }
        destoryGameObject.ForEach((item) => DestroyImmediate(item));

        var existVehicle = false;
        for (var i = 0; i < TeamQueue.shared.humans.Count; i++)
        {
            var human = TeamQueue.shared.humans[i].GetComponent<RoleInfo>();
            humanPanel.transform.GetChild(i).gameObject.FindObject("Text").GetComponent<UnityEngine.UI.Text>().text = human.gameObject.GetComponent<RoleInfo>().nick;
            humanPanel.transform.GetChild(i).gameObject.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = human.gameObject.GetComponent<SpriteRenderer>().sprite; ;

            var vehicleButton = vehiclePanel.transform.GetChild(i);
            if (TeamQueue.shared.humans[i].GetComponent<HumanInfo>().currentTakedVehicle != null)
            {
                existVehicle = true;
                var vehicle = TeamQueue.shared.humans[i].GetComponent<HumanInfo>().currentTakedVehicle;
                vehicleButton.gameObject.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = vehicle.avatar;
                vehicleButton.gameObject.FindObject("Text").GetComponent<UnityEngine.UI.Text>().text = vehicle.name;
            }
            else
            {
                vehicleButton.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                vehicleButton.gameObject.GetComponent<CanvasGroup>().interactable = false;
                vehicleButton.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        //没有坦克，隐藏 vehiclePanel
        if (!existVehicle)
        {
            DestroyImmediate(vehiclePanel);
        }
    }
    // Start is called before the first frame update
    public static RoleMenuController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/RoleMenu"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<RoleMenuController>();
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
