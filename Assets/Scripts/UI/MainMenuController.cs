using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : BaseUIInputController
{

     public override void Awake(){
        base.Awake();
        MMX.GameManager.MainMenu = this;

        inputs.UI.A.performed += ctx =>
        {
            if (EventSystem.current.currentSelectedGameObject.name == "包裹")
            {
                var boxController = SelectButtonBoxController.Create();
                boxController.addButton("道具",null);
                boxController.addButton("回复道具",null);
                boxController.addButton("战斗道具",null);
                boxController.addButton("装备",null);
                boxController.show();


            }
            else if (EventSystem.current.currentSelectedGameObject.name == "乘降")
            {
                hide();
                StationMenuController stationMenu = StationMenuController.Create();
                stationMenu.show();
            }
    
        };

        inputs.UI.B.performed += ctx => hide();
    }
    public override void willOnFocus()
    {
        base.willOnFocus();
        setSelectedButton();
    }
    public void setSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(gameObject.FindObject("包裹"));
    }
    public void show()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        gameObject.SetActive(true);
        MMX.GameManager.Input.pushTarget(gameObject);

    }
    public void hide()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Input.popTarget();
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}