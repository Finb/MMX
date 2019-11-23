using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionChangedController : MonoBehaviour, ISelectHandler
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnSelect(BaseEventData data){
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x4D-Shift"));
        placeFinger();
    }
    public void placeFinger(){
        var fingerObj = GameObject.FindGameObjectWithTag("Finger");
        var fingerImage = fingerObj.FindObject("Image", true);
        var selectedData = EventSystem.current.currentSelectedGameObject;
        fingerImage.transform.position = new Vector2(selectedData.transform.position.x - 28, selectedData.transform.position.y);
    }
}
