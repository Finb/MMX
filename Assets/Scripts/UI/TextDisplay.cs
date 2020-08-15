using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;

public class TextDisplay : MonoBehaviour
{
    private string contentText;
    private List<string> sentences;
    private int currentDisplaySentenceIndex = 0;
    private float letterPause = 0.01f;
    public GameObject arrowImage;

    private DialogController dialogController;
    private Text textLabel
    {
        get
        {
            return GetComponent<Text>();
        }
    }
    void Start()
    {
        dialogController = gameObject.GetComponentInParentIncludeInactive<DialogController>();
    }
    public void showText()
    {
        contentText = textLabel.text;
        sentences = new List<string>();

        string line = "";
        var lines = contentText.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            line += lines[i];
            if (i < lines.Length - 1)
            {
                line += "\n";
            }
            else
            {
                sentences.Add(line);
            }
            if (i > 0 && (i + 1) % 3 == 0)
            {
                //每次显示三行
                sentences.Add(line);
                line = "";
            }
        }

        currentDisplaySentenceIndex = 0;
        startType();

    }
    void startType()
    {
        textLabel.text = "";



        if (currentDisplaySentenceIndex < sentences.Count)
        {
            var sentence = sentences[currentDisplaySentenceIndex];
            var sentenceLength = Regex.Replace(sentence, "/<[^>]+>/g","").Length;
            textLabel.DOText(sentence, sentenceLength * 0.01f, true).OnComplete(delegate ()
            {
                currentDisplaySentenceIndex++;
                if (currentDisplaySentenceIndex >= sentences.Count)
                {
                    if (dialogController.disPlayCompletionAction != null)
                    {
                        dialogController.disPlayCompletionAction();
                    }
                }
                //全部显示完毕
                arrowImage.SetActive(true);
            });
        }
        else
        {
            finished();
        }
    }

    public void continueType()
    {
        if (arrowImage.activeSelf)
        {
            arrowImage.SetActive(false);
            startType();
        }
    }
    void finished()
    {
        dialogController.hide();
    }
}
