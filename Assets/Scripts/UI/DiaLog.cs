using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Dialog
{
    public static Dialog shared
    {
        get
        {
            return Dialog.Instance();
        }
    }
    public bool isActiving {
        get {
            return digLogGameObject.activeSelf;
        }
    }
    public bool isInteroperable {
        get {
            return textDisplay.arrowImage.activeSelf;
        }
    }
    private GameObject digLogGameObject;
    private Text textLabel;
    private Text nickLabel;
    public TextDisplay textDisplay;
    private Dialog()
    {
        digLogGameObject = GameObject.Find("UI").FindObject("Diglog");
        textLabel = digLogGameObject.FindObject("Text", true).GetComponent<Text>();
        nickLabel = digLogGameObject.FindObject("nickText", true).GetComponent<Text>();
        textDisplay = textLabel.GetComponent<TextDisplay>();
    }

    private static Dialog instance;
    public static Dialog Instance()
    {
        if (instance == null)
        {
            instance = new Dialog();
        }

        return instance;
    }

    public void showText(string text, string nick = null)
    {
        MMX.GameManager.Input.pushTarget(digLogGameObject);
        digLogGameObject.SetActive(true);
        var nickPanel = nickLabel.gameObject.transform.parent.gameObject;
        var nickPanelRectTransform = nickPanel.GetComponent<RectTransform>();
        if (nick != null){
            nickLabel.text = nick;
            var width = Math.Max(40 + nick.GetFontlen(30), 80);
            nickPanelRectTransform.anchoredPosition = new Vector3(width / 2, 28 ,0);
            nickPanelRectTransform.sizeDelta = new Vector2(width, 64);
            
            nickPanel.SetActive(true);
        }
        else{
            nickPanel.SetActive(false);
        }
        textLabel.text = text;
        textDisplay.showText();
    }
    public void hide()
    {
        digLogGameObject.SetActive(false);
        MMX.GameManager.Input.popTarget();
    }
    public void continueDiglog(){
        textDisplay.continueType();
    }
}
