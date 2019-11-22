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
        var fingerObj = GameObject.FindGameObjectWithTag("Finger");
        var fingerImage = fingerObj.FindObject("Image", true);
        fingerImage.transform.position = new Vector2(data.selectedObject.transform.position.x - 28, data.selectedObject.transform.position.y);
    }
}
