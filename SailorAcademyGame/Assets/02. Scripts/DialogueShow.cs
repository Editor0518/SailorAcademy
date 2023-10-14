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
    //public Image imgStanding;   //���ĵ��Ϸ���Ʈ

    [Header("Texts")]
    public TMP_Text txtName;    //�̸� �ؽ�Ʈ
    public TMP_Text txtContent; //��� �ؽ�Ʈ

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
