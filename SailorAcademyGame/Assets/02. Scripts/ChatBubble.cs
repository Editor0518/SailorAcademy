using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public RectTransform rect;
    public Color bubbleColor=Color.black;

    public Image imgLeft;
    public Image imgRight;
    public Image imgMiddle;

    public Sprite sprTail;
    public Sprite sprRound;

    public TMP_Text text;

    public bool isHold = false;

    private void OnEnable()
    {
        if (isHold) {
           
            SetPosition(false);
            StartCoroutine("Holding"); 
        }
    }


    public void SetText(string msg, string color) {
        bool isLeft;

        if (msg.Contains(">")) {//[ text  ]>
            msg = msg.Replace(">", "");
            isLeft = false;
        }
        else {//<[  text  ]
            msg = msg.Replace("<", "");
            isLeft = true;
        }
        ColorUtility.TryParseHtmlString(color, out bubbleColor);
        imgLeft.color = bubbleColor;
        imgRight.color = bubbleColor;
        imgMiddle.color = bubbleColor;

        text.text = msg;
        //처음여백50 + 공백뺀문자*35 + 공백당*10 + 끝에10(처음여백에 합쳐짐)
        int sizeX = 60 + msg.Replace(" ", "").Length * 35 + msg.Split(" ").Length * 10;
        rect.sizeDelta = new Vector2(sizeX, rect.sizeDelta.y);
        SetPosition(isLeft);
    }

    void SetPosition(bool isLeft) {

        if (isLeft)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.up;
            rect.anchoredPosition = new Vector2(rect.rect.width / 2, 0);

            imgLeft.sprite = sprTail;
            imgRight.sprite = sprRound;

            imgLeft.rectTransform.localScale = Vector3.one;
            imgRight.rectTransform.localScale = Vector3.one;
        }
        else
        {
            if(isHold) rect.sizeDelta = new Vector2(150, rect.sizeDelta.y);

            rect.anchorMin = Vector2.right;
            rect.anchorMax = Vector2.one;
            rect.anchoredPosition = new Vector2(-rect.rect.width / 2, 0);
            
            imgLeft.sprite = sprRound;
            imgRight.sprite = sprTail;

            imgLeft.rectTransform.localScale = new Vector3(-1,1,1);//x-=1
            imgRight.rectTransform.localScale = new Vector3(-1, 1, 1);//x-=1
        }

    }

    IEnumerator Holding() {
        WaitForSeconds wait = new(0.3f);
        WaitForSeconds waitFast = new(0.25f);
        
        string msg = "";
        while (gameObject.activeInHierarchy)
        {
            msg += "· ";
            if (msg == "· · · · ") { 
                msg = ""; 
            }

            text.text = msg;

            yield return msg==""?waitFast:wait;
        }
    }

}
