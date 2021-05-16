using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : BaseUIInputController
{
    //显示引用计数，每调用一次show + 1， 调用1次hide - 1, hide时count为 0 则隐藏对话框
    private int showReferenceCount = 0;
    private Text textLabel;
    private Text nickLabel;
    public TextDisplay textDisplay;

    public Action displayCompletionAction;
    public override void Awake()
    {
        base.Awake();
        needsShowFinger = false;

        textLabel = gameObject.FindComponentByObjectName<Text>("Text");
        nickLabel = gameObject.FindComponentByObjectName<Text>("nickText");
        textDisplay = textLabel.GetComponent<TextDisplay>();

        inputs.UI.ContinueButton.performed += ctx => { if (isInteroperable) continueDiglog(); };
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
    public void showText(string text, string nick = null, Action displayCompletionAction = null)
    {
        showReferenceCount += 1;

        this.displayCompletionAction = displayCompletionAction;
        if (MMX.GameManager.Input.currentTarget != this)
        {
            //如果已经显示，就不需要再push了
            MMX.GameManager.Input.pushTarget(gameObject);
        }
        gameObject.SetActive(true);

        var nickPanel = nickLabel.gameObject.transform.parent.gameObject;
        var nickPanelRectTransform = nickPanel.GetComponent<RectTransform>();
        if (nick != null && nick.Length > 0)
        {
            nickLabel.text = nick;
            var width = Math.Max(40 + nick.GetFontlen(30), 80);
            nickPanelRectTransform.anchoredPosition = new Vector3(width / 2, 28, 0);
            nickPanelRectTransform.sizeDelta = new Vector2(width, 54);

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
        showReferenceCount -= 1;
        
        if (showReferenceCount <= 0)
        {
            gameObject.SetActive(false);
            MMX.GameManager.Input.popTarget();
        }
    }
    public void continueDiglog()
    {
        textDisplay.continueType();
    }

    private static DialogController create()
    {
        var diglog = Instantiate(Resources.Load<GameObject>("UI/Dialog"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        diglog.name = "Diglog";
        return diglog.GetComponent<DialogController>();
    }
    private static readonly Lazy<DialogController> Instancelock = new Lazy<DialogController>(() => DialogController.create());
    public static DialogController shared
    {
        get
        {
            return Instancelock.Value;
        }
    }
}
