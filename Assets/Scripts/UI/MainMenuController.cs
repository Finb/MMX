using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuController : MonoBehaviour, InputEventInterface
{

    private void Awake()
    {
        MMX.GameManager.MainMenu = this;
        gameObject.SetActive(false);
    }
    public void willOnFocus()
    {

    }
    public void willLostFocus()
    {

    }
    public void inputAction()
    {
        if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.A))
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.B))
        {
            hide();
        }
    }
    public void show()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Input.pushTarget(gameObject);
        MMX.GameManager.Finger.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameObject.FindObject("包裹"));
        gameObject.SetActive(true);
    }
    public void hide()
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3E-Enter"));
        MMX.GameManager.Input.popTarget();
        MMX.GameManager.Finger.SetActive(false);
        gameObject.SetActive(false);
    }
}