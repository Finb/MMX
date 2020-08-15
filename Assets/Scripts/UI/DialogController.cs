using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : BaseUIInputController
{
    private Text textLabel;
    private Text nickLabel;
    public TextDisplay textDisplay;

    public Action disPlayCompletionAction;
    public Action willDisappearAction;
    public override void Awake(){
        base.Awake();
        needsShowFinger = false;
               
        textLabel = gameObject.FindObject("Text", true).GetComponent<Text>();
        nickLabel = gameObject.FindObject("nickText", true).GetComponent<Text>();
        textDisplay = textLabel.GetComponent<TextDisplay>();
 
        inputs.UI.ContinueButton.performed += ctx => {if (isInteroperable) continueDiglog();};  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isActiving
    {
        get
        {
            return gameObject.activeSelf;
        }
    }
    public bool isInteroperable
    {
        get
        {
            return textDisplay.arrowImage.activeSelf;
        }
    }
    public void showText(string text, string nick = null, Action disPlayCompletionAction = null)
    {
        this.disPlayCompletionAction = disPlayCompletionAction;
        MMX.GameManager.Input.pushTarget(gameObject);
        gameObject.SetActive(true);
        var nickPanel = nickLabel.gameObject.transform.parent.gameObject;
        var nickPanelRectTransform = nickPanel.GetComponent<RectTransform>();
        if (nick != null && nick.Length > 0)
        {
            nickLabel.text = nick;
            var width = Math.Max(40 + nick.GetFontlen(30), 80);
            nickPanelRectTransform.anchoredPosition = new Vector3(width / 2, 28, 0);
            nickPanelRectTransform.sizeDelta = new Vector2(width, 64);

            nickPanel.SetActive(true);
        }
        else
        {
            nickPanel.SetActive(false);
        }
        textLabel.text = text;
        textDisplay.showText();
    }
    public void hide()
    {
        gameObject.SetActive(false);
        MMX.GameManager.Input.popTarget();
        if (willDisappearAction != null)
        {
            willDisappearAction();
        }
        Destroy(gameObject);
    }
    public void continueDiglog()
    {
        textDisplay.continueType();
    }

    public static DialogController create()
    {
        var diglog = Instantiate(Resources.Load<GameObject>("UI/Dialog"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        diglog.name = "Diglog";
        return diglog.GetComponent<DialogController>();
    }
}
