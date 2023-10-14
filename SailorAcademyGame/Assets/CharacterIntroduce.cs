using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterIntroduce : MonoBehaviour
{
    public TMP_Text nameTxt;
    public TMP_Text infoTxt;
    public TMP_Text contentTxt;
    public Image img;

    DialogSystem dialog;
    ///Characters ch;

    private void Start()
    {
        dialog = GameObject.FindWithTag("Manager").GetComponent<DialogSystem>();
        //ch = dialog.ch;
    }

    public void SetIntroduce(string name, string info, string content) {
        nameTxt.text = name;
        infoTxt.text = info;
        contentTxt.text = content;
        ChangeSprite(name);
    }

    void ChangeSprite(string name)
    {
        if(dialog==null) dialog = GameObject.FindWithTag("Manager").GetComponent<DialogSystem>();
        if (name == "") { return; }

        for (int i = 0; i < dialog.ch.chracters.Count+1; i++){
            if (name.Equals(dialog.ch.chracters[i].strName))
            {
                img.sprite = dialog.ch.chracters[i].standingImg[0].sprite;

                break;
            }

        }
    }


}
