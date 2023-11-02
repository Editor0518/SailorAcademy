using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class DialogueShow : MonoBehaviour
{
    [Header("Background")]
    public VideoPlayer vp;
    public RawImage backgroundImg;
    public RenderTexture render;
    [Header("Background Fade")]
    public VideoPlayer vpBack;
    public RawImage backgroundImgBack;
    public RenderTexture renderBack;

    //[Header("Character Standing")]
    //public Image imgStanding;   //스탠딩일러스트

    [Header("Texts")]
    public TMP_Text txtName;    //이름 텍스트
    public TMP_Text txtContent; //대사 텍스트

    string backgroundPath = "background/";

    [SerializeField] Image midItemImg;
    string midItemPath = "mid-item/";
    string backgroundName;


    public void LoadMidItem(string sprName) {
        
        sprName = sprName.Replace("c_", "");
       
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
        
        if (backgroundName!=""&&backgroundName != background) {//이전 것과 이번 것이 다를시
            if (backgroundImg.texture == render) {
                VideoClip clip = Resources.Load<VideoClip>(backgroundPath + backgroundName);
                vpBack.clip = clip;
                backgroundImgBack.texture = renderBack;
            }
            else backgroundImgBack.texture = backgroundImg.texture;
            backgroundImgBack.gameObject.SetActive(false);
            
        }

        Texture sprite = Resources.Load<Texture>(backgroundPath + background);
        if (sprite != null)
        {
            //backgroundImg.sprite = sprite;
            backgroundImg.texture = sprite;
        }
        else {
            VideoClip clip= Resources.Load<VideoClip>(backgroundPath + background);
            vp.clip = clip;
            //RenderTexture render = Resources.Load<RenderTexture>(backgroundPath + "VideoRenderTexture");
            backgroundImg.texture = render;
        }
        backgroundName = background;
        backgroundImgBack.gameObject.SetActive(true);
    }



}
