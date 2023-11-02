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
    //public Image imgStanding;   //���ĵ��Ϸ���Ʈ

    [Header("Texts")]
    public TMP_Text txtName;    //�̸� �ؽ�Ʈ
    public TMP_Text txtContent; //��� �ؽ�Ʈ

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
        
        if (backgroundName!=""&&backgroundName != background) {//���� �Ͱ� �̹� ���� �ٸ���
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
