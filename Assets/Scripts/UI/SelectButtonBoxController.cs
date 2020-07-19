using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectButtonBoxController : BaseUIInputController
{
    // Start is called before the first frame update
    List<Action> onClicks = new List<Action>();
    List<GameObject> buttons = new List<GameObject>();
     public override void Awake(){
        base.Awake();
        inputs.UI.A.performed += ctx =>
        {
            var index = buttons.IndexOf(MMX.GameManager.Input.currentSelectedGameObject);
            if (index >= 0)
            {
                hide();
                onClicks[index]();
            }
        };
        inputs.UI.B.performed += ctx => hide();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addButton(string text, Action onClick = null)
    {
        var button = Resources.Load<GameObject>("UI/按钮");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = text;

        buttons.Add(Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("ImageBox").transform));
        onClicks.Add(onClick);
    }
    public void addButton(string text, UnityEngine.Sprite sprite = null,  Action onClick = null)
    {
        var button = Resources.Load<GameObject>("UI/prefabs/IconButton");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        button.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        button.FindObject("Image").GetComponent<UnityEngine.UI.Image>().enabled = sprite != null;
        
        buttons.Add(Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("ImageBox").transform));
        onClicks.Add(onClick);
    }

    public static SelectButtonBoxController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/SelectButtonBox"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<SelectButtonBoxController>();
    }

    public void show()
    {
        // LayoutRebuilder.MarkLayoutForRebuild
        MMX.GameManager.Input.pushTarget(gameObject);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("ImageBox").GetComponent<RectTransform>());
        EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
