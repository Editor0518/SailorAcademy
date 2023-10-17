using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoSystem : MonoBehaviour
{
    public GameObject whole;

    public TMP_Text titleTxt;
    public TMP_Text contentTxt;

    public Image icon;
    public Image iconBk;

    public void ShowInfo(string content) {
        if (whole.activeInHierarchy) whole.SetActive(false);
        //[새로운 정보] : < b > 범죄자 </ b > : 피어리스호의 캡틴, 오민현을 부르는 말. 
        if (content.Contains("[새로운 정보] : ")) content=content.Replace("[새로운 정보] : ", "");
        //< b > 범죄자 </ b > : 피어리스호의 캡틴, 오민현을 부르는 말. 
        string[] str = content.Split(" : ");

        titleTxt.text = str[0];//< b > 범죄자 </ b >
        contentTxt.text = str[1];//피어리스호의 캡틴, 오민현을 부르는 말.
        whole.SetActive(true);
    }


}
