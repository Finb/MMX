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
                boxController.addButton("道具");
                boxController.addButton("回复道具");
                boxController.addButton("战斗道具");
                boxController.addButton("装备");
                boxController.show();


            }
            else if (EventSystem.current.currentSelectedGameObject.name == "乘降")
            {
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

                // vehicles[0].gameObject.GetComponent<SpriteRenderer>().sprite.texture

                Debug.Log(vehicles.Count);
            }
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
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