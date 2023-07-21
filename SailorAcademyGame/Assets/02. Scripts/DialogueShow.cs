using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueShow : MonoBehaviour
{
    [Header("Background")]
    public Image backgroundImg;

    [Header("Character Standing")]
    public Image imgStanding;   //���ĵ��Ϸ���Ʈ

    [Header("Texts")]
    public TMP_Text txtName;    //�̸� �ؽ�Ʈ
    public TMP_Text txtContent; //��� �ؽ�Ʈ

    string backgroundPath = "background/";

    void Start()
    {
        
    }


    public void ShowNext() {
        
    }



    public void LoadBackground(string background) {
        /*string[] cmd=  background.Split(".");

        if (cmd[1] != null) {
            
        }*/
        if (background == "") return;

        Sprite sprite = Resources.Load<Sprite>(backgroundPath + background);
        if (sprite != null) {
            backgroundImg.sprite = sprite;
        }

    }



}
