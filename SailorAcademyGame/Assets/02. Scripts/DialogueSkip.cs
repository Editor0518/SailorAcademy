using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSkip : MonoBehaviour
{
    public Button toolOnButton;

    public Animator anim;

    public Button autoButton;
    public Sprite[] autoSprite;
    public Button skipButton;
    public Sprite[] skipSprite;
    public GameObject bigStopObj;

    public int value = 0;//0=none, 1=auto, 2=skip all
    public DialogSystem dialog;

    bool isDown = false;

    public void PointerEnter() {
        
        if (!isDown) {
            isDown = true;
            toolOnButton.gameObject.SetActive(false);
            anim.SetTrigger("Down");
        }
    }

    public void PointerExit()
    {
       
        if (isDown)
        {
            isDown = false;
            toolOnButton.gameObject.SetActive(true);
            anim.SetTrigger("Up");
        }
    }


    public void AutoSkip() {
        value = value != 1 ? 1 : 0;
        UpdateSprite();
    }

    public void AllSkip() {
        value = value != 2 ? 2 : 0;
        bigStopObj.SetActive(true);
        UpdateSprite();
    }

    public void NoSkip() {
        value = 0;
        if (bigStopObj.activeInHierarchy) bigStopObj.SetActive(false);
        UpdateSprite();
    }

    void UpdateSprite() {
        autoButton.interactable = false;
        autoButton.interactable = true;
        skipButton.interactable = false;
        skipButton.interactable = true;
        autoButton.image.sprite = autoSprite[0];
        skipButton.image.sprite = skipSprite[0];
        if (value==1) autoButton.image.sprite = autoSprite[1];
        if (value==2) skipButton.image.sprite = skipSprite[1];
        dialog.ChangeIsSkip(value);
    }

}
