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
        //[���ο� ����] : < b > ������ </ b > : �Ǿ��ȣ�� ĸƾ, �������� �θ��� ��. 
        if (content.Contains("[���ο� ����] : ")) content=content.Replace("[���ο� ����] : ", "");
        //< b > ������ </ b > : �Ǿ��ȣ�� ĸƾ, �������� �θ��� ��. 
        string[] str = content.Split(" : ");

        titleTxt.text = str[0];//< b > ������ </ b >
        contentTxt.text = str[1];//�Ǿ��ȣ�� ĸƾ, �������� �θ��� ��.
        whole.SetActive(true);
    }


}
