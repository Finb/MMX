using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionChangedController : MonoBehaviour, ISelectHandler
{
    public int tag = 0;

    private bool _active = true;
    public bool active
    {
        set
        {
            _active = value;
            var button = GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                button.interactable = _active;
            }
        }
        get
        {
            return _active;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnSelect(BaseEventData data)
    {
        MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x4D-Shift"));
        MMX.GameManager.Input.currentSelectedGameObject = gameObject;
        MMX.GameManager.Input.selectedButtonChanged();
        placeFinger();
    }
    public void placeFinger()
    {
        var fingerObj = MMX.GameManager.Finger;
        var fingerImage = fingerObj.FindObject("Image", true);
        var selectedData = EventSystem.current.currentSelectedGameObject;
        var selectedDataRectTransform = selectedData.GetComponent<RectTransform>();
        var fingerImageRectTransform = fingerImage.GetComponent<RectTransform>();
        var x = selectedData.transform.position.x
        - selectedDataRectTransform.lossyScale.x * selectedDataRectTransform.sizeDelta.x / 2
        - fingerImageRectTransform.lossyScale.x * fingerImageRectTransform.sizeDelta.x / 2;
        fingerImage.transform.position = new Vector2(x, selectedData.transform.position.y);

    }
}
