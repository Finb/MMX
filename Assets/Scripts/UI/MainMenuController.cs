using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class MainMenuController : MonoBehaviour, InputEventInterface
{

    private void Awake()
    {
        MMX.GameManager.MainMenu = this;
        // gameObject.SetActive(true);
    }
    public void willOnFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = true;
        }
        setSelectedButton();
    }
    public void willLostFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
        }
    }
    public void inputAction()
    {
        if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.A))
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
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.B))
        {
            hide();
        }
    }
    public void setSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(gameObject.FindObject("包裹"));
    }
    public void show()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Finger.SetActive(true);
        gameObject.SetActive(true);
        MMX.GameManager.Input.pushTarget(gameObject);

    }
    public void hide()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Input.popTarget();
        MMX.GameManager.Finger.SetActive(false);
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}