﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectButtonBoxController : BaseInputController
{
    // Start is called before the first frame update
    List<Action> onClicks = new List<Action>();
    List<GameObject> buttons = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void inputAction()
    {
        if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.A))
        {
            var index = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);
            if (index >= 0){
                hide();
                onClicks[index]();
            }
        }
        else if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.B))
        {
            hide();
        }
    }
    public void addButton(string text, Action onClick = null)
    {
        var button = Resources.Load<GameObject>("UI/按钮");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        
        buttons.Add(Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("ImageBox").transform));
        onClicks.Add(onClick);
    }

    public static SelectButtonBoxController Create(){
        var obj = Instantiate(Resources.Load<GameObject>("UI/SelectButtonBox"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<SelectButtonBoxController>();
    }

    public void show()
    {
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