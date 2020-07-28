using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateScript : MonoBehaviour
{

    public string translatedText;

    public TextMeshProUGUI textElement;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("language") == "english"){
            textElement.text = translatedText;
        }
    }

}
