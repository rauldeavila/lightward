using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localization : MonoBehaviour {

	private TextMeshProUGUI text;

    [TextArea(3, 10)]
    public string englishText;
    [TextArea(3, 10)]
    public string portugueseText;
    private bool flag1 = false;
    private bool flag2 = false;

    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if(PlayerPrefs.GetInt("Language", 0) == 0 && flag1 == false){
            flag1 = true;
            flag2 = false;
            text.text = englishText;
        } else if(PlayerPrefs.GetInt("Language", 0) == 1 && flag2 == false){
            flag2 = true;
            flag1 = false;
            text.text = portugueseText;
        }
    }





}
