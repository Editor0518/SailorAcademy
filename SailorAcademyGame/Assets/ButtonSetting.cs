using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonSetting : MonoBehaviour
{
    public TMP_Text text;
    public Button button;
    AudioSource audiosource;
    public AudioClip clipPointer;
    public AudioClip clipNext;


    public bool isSelected=false;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Selected(false);
        OnPointerOff();
        button.interactable = false;
        button.interactable = true;
    }

    public void ReverseText() {
       // text.color = button.;
    }

    void PointerOn() {
        if (!button.interactable) return;
        audiosource.Stop();
        audiosource.clip = clipPointer;
        audiosource.Play();
    }

    public void WhenClicked() {
        if (!button.interactable) return;
        audiosource.Stop();
        audiosource.clip = clipNext;
        audiosource.Play();
    }


    public void Selected(bool isSelected) {    
        if (!button.interactable) return;
        this.isSelected = isSelected;
    }

    public void OnPointerOn() {
        if (!button.interactable) return;
        if (!isSelected) PointerOn();
        text.color = button.colors.normalColor;//blue
        
        //if (isSelected) text.color = button.colors.selectedColor;
    }
    public void OnPointerOff()
    {
        if (!button.interactable) return;
        text.color = button.colors.selectedColor;
        
        if (isSelected) { text.color = button.colors.normalColor; }
    }


    public void WhenDown()
    {
        if (!button.interactable) return;
        button.transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    public void WhenUp() {
        if (!button.interactable) return;
        button.transform.localScale = Vector3.one;
    }

}
