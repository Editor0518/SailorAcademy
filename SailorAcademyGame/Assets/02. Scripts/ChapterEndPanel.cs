using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterEndPanel : MonoBehaviour
{
    /// <summary>
    /// ���ѷα� ���� �г��� ���ѷα׸� �о���� ���00
    /// ĳ���� 11���� ��Ż�� �ݿ��ϴ� ���
    /// ĳ���� 10���� ģ�е� ��ȭ�� ǥ�����ִ� ���
    /// ĳ���Ͱ� �׾��� �� Xǥ�� ���ִ� ���
    /// ���ݱ����� ������ �ҷ����� ���.00
    /// ��������, �������� Ŭ�� �� â �ߵ���
    /// �������� ������ é�� ���� â�� �ߵ��� �ϴ� ���**
    /// </summary>

    public TMP_Text headerTxt;
    public TMP_Text headerTxt2;
    public AudioClip clip;
    AudioSource audiosource;
    public SceneLoadManager scene;
    public int nextChapter = 1;

    string[] scenes = { "���ѷα�", "é�� 1", "é�� 2" };

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        ShowRecords();
        headerTxt.text = scenes[int.Parse(PlayerPrefs.GetString("DftSave", (nextChapter-1).ToString()))] + " ����";
        headerTxt2.text = headerTxt.text;
        if (scene != null)scene.UpdateDefaultSave(nextChapter);
    }

    public RectTransform RecContent;//���� Content
    public GameObject RecIns;//���� �ؽ�Ʈ (�ν��Ͻ�)

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
