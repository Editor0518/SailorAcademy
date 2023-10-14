using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueShow : MonoBehaviour
{
    [Header("Background")]
    public SpriteRenderer backgroundImg;

    //[Header("Character Standing")]
    //public Image imgStanding;   //스탠딩일러스트

    [Header("Texts")]
    public TMP_Text txtName;    //이름 텍스트
    public TMP_Text txtContent; //대사 텍스트

    string backgroundPath = "background/";

    [SerializeField] Image midItemImg;
    string midItemPath = "mid-item/";


    void Start()
    {
        
    }


    public void ShowNext() {
        
    }


    public void LoadMidItem(string sprName) {
        
        sprName = sprName.Replace("c_", "");
        Debug.Log(sprName);
        Sprite spr= Resources.Load<Sprite>(midItemPath+sprName);
        midItemImg.sprite = spr;
        midItemImg.enabled = spr!=null;
    }

    public void HideMidItem() {
        midItemImg.enabled = false;
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
