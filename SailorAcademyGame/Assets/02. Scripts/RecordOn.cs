using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class RecordOn : MonoBehaviour
{
    Animator anim;
    TMP_Text txt;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        txt = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void TextOn(string text) {
        if (PlayerPrefs.GetString("OneRecord").Equals("1;2;3;4;5;6")) PlayerPrefs.SetString("OneRecord", ""); 
        txt.text = "- "+text;
        anim.SetTrigger("On");
        //ºº¿Ã∫Í
        PlayerPrefs.SetString("OneRecord", PlayerPrefs.GetString("OneRecord") + text + ";");
        Debug.Log("saved: "+PlayerPrefs.GetString("OneRecord"));
    }
}
