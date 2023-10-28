using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterEndPanel : MonoBehaviour
{
    /// <summary>
    /// 프롤로그 종료 패널의 프롤로그를 읽어오는 기능00
    /// 캐릭터 11명의 멘탈을 반영하는 기능
    /// 캐릭터 10명의 친밀도 변화를 표시해주는 기능
    /// 캐릭터가 죽었을 시 X표시 쳐주는 기능
    /// 지금까지의 행적을 불러오는 기능.00
    /// 항해일지, 선원관리 클릭 시 창 뜨도록
    /// 다음으로 누르면 챕터 선택 창이 뜨도록 하는 기능**
    /// </summary>

    public TMP_Text headerTxt;
    public TMP_Text headerTxt2;
    public AudioClip clip;
    AudioSource audiosource;
    public SceneLoadManager scene;
    public int nextChapter = 1;

    string[] scenes = { "프롤로그", "챕터 1", "챕터 2" };

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        ShowRecords();
        headerTxt.text = scenes[int.Parse(PlayerPrefs.GetString("DftSave", (nextChapter-1).ToString()))] + " 종료";
        headerTxt2.text = headerTxt.text;
        if (scene != null)scene.UpdateDefaultSave(nextChapter);
    }

    public RectTransform RecContent;//행적 Content
    public GameObject RecIns;//행적 텍스트 (인스턴스)

    public void ShowRecords() {
        //PlayerPrefs.SetString("OneRecord", "1;2;3;4;5;6");
        StartCoroutine(AddRecord());
        
    }

    IEnumerator AddRecord() {
        WaitForSeconds wait = new(1);
        string[] rec = PlayerPrefs.GetString("OneRecord").Split(";");
        RecContent.sizeDelta = new Vector2(0, 50);
        for (int i = -2; i < rec.Length-1; i++)
        {
            if (i >= 0)
            {
                GameObject obj = Instantiate(RecIns, RecContent);
                obj.GetComponent<TMP_Text>().text = "- " + rec[i];
                RecContent.sizeDelta += new Vector2(0, 75);
                if (i >= 3) RecContent.anchoredPosition += new Vector2(0, 75);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(210, -(55 + (80 * i)));
                audiosource.clip = clip;
                audiosource.Stop();
                audiosource.Play();
            }
            yield return wait;
        }

    }
}
