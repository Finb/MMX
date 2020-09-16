using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : BaseUIInputController
{

    public override void Awake()
    {
        base.Awake();
        MMX.GameManager.MainMenu = this;

        gameObject.FindObject("包裹").GetComponent<ButtonController>().clickEvent = () =>
        {
            var packageController = PackageMenuController.Create();
            packageController.show();
        };
        gameObject.FindObject("强度").GetComponent<ButtonController>().clickEvent = () =>
        {
            var controller = HumanStatusController.Create();
            controller.show();
        };
        gameObject.FindObject("装备").GetComponent<ButtonController>().clickEvent = () =>
        {
            var roleMenu = RoleMenuController.Create();
            roleMenu.selectRoleCompletionAction = (humanInfo) =>
            {
                var humanStatusController = HumanStatusController.Create();
                humanStatusController.viewType = HumanStatusController.HumanStatusViewType.equipment;
                humanStatusController.currentHumanIndex = TeamQueue.shared.humans.IndexOf(humanInfo.gameObject);
                humanStatusController.show();
            };
            roleMenu.show();
        };
        gameObject.FindObject("乘降").GetComponent<ButtonController>().clickEvent = () =>
        {
            StationMenuController stationMenu = StationMenuController.Create();
            stationMenu.show();
        };


        inputs.UI.A.performed += ctx =>
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<ButtonController>().clickEvent?.Invoke();
        };

        inputs.UI.B.performed += ctx => hide();
    }
    public override void willOnFocus()
    {
        base.willOnFocus();
    }
    public void show()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        gameObject.SetActive(true);
        MMX.GameManager.Input.pushTarget(gameObject);

        EventSystem.current.SetSelectedGameObject(gameObject.FindObject("包裹"));
    }
    public void hide()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Input.popTarget();
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}