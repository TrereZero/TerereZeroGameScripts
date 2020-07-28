using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public string[] sentences;

    public string[] englishSentences;

    private int index = 0;

    public GameObject dialogContainer;

    public GameObject indicadorDeDialogo;

    public void nextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = sentences[index];
        }
        else
        {
            closeDialog();
        }
    }

    public void showDialog(string[] objectSentences)
    {
        index = 0;

        if (dialogContainer.activeInHierarchy)
        {
            sentences = objectSentences;
            textDisplay.text = sentences[index];
        }
    }

    public void closeDialog()
    {
        index = 0;
        textDisplay.text = "";
        dialogContainer.SetActive(false);
    }
}
