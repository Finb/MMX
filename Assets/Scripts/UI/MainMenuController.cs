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
        
        MMX.GameManager.Finger.SetActive(true);

        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameObject.FindObject("包裹"));
        // gameObject.FindObject("包裹").GetComponent<ButtonSelectionChangedController>().OnSelect(null);
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