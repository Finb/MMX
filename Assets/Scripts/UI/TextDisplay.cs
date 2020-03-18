using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            StartCoroutine(TypeSentence(sentences[currentDisplaySentenceIndex]));
            Debug.Log("fffff");
        }
        else
        {
            finished();
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textLabel.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
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
