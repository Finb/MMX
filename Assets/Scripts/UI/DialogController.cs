using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class DialogController : BaseInputController
{
    // Start is called before the first frame update
    private Text textLabel;
    private Text nickLabel;
    public TextDisplay textDisplay;
    private void Awake()
    {
        MMX.GameManager.Dialog = this;
        textLabel = gameObject.FindObject("Text", true).GetComponent<Text>();
        nickLabel = gameObject.FindObject("nickText", true).GetComponent<Text>();
        textDisplay = textLabel.GetComponent<TextDisplay>();
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
    public override void inputAction()
    {
        if (isInteroperable && MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.A))
        {
            continueDiglog();
        }
    }
    public void showText(string text, string nick = null)
    {
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
    }
    public void continueDiglog()
    {
        textDisplay.continueType();
    }
}
