using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("0:-, 1:0, 2:+")]
    public Sprite[] sprFriend;
    public Sprite[] sprMental;

    [System.Serializable]
    public struct CharBlock {
        public string code;
        public Image friend;
        public TMP_Text friendTxt;
        public Image mental;
        public TMP_Text mentalTxt;

        public CharBlock(string code, Image friend, TMP_Text friendTxt, Image mental, TMP_Text mentalTxt) {
            this.code = code;
            this.friend = friend;
            this.friendTxt = friendTxt;
            this.mental = mental;
            this.mentalTxt = mentalTxt;
        }
    }

    Characters ch;
    public List<ChapCharSetting> chars;


    string[] scenes = { "���ѷα�", "é�� 1", "é�� 2" };

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        ShowRecords();
        headerTxt.text = scenes[int.Parse(PlayerPrefs.GetString("DftSave", (nextChapter - 1).ToString()))] + " ����";
        headerTxt2.text = headerTxt.text;
        if (scene != null) scene.UpdateDefaultSave(nextChapter);
    }

    private void OnEnable()
    {
        ch = GameObject.FindGameObjectWithTag("SheetData").GetComponent<Characters>();
        CharacterFMSet();
        //ch.CharacterReset();
    }

    public RectTransform RecContent;//���� Content
    public GameObject RecIns;//���� �ؽ�Ʈ (�ν��Ͻ�)

    public void ShowRecords() {
        //PlayerPrefs.SetString("OneRecord", "1;2;3;4;5;6");
        StartCoroutine(AddRecord());

    }

    public void CharacterFMSet() {
        for (int i = 0; i < 11; i++) {
            string[] strs = PlayerPrefs.GetString("tempsave" + chars[i].info.code, "0,0").Split(",");

            SetImgFM(i, int.Parse(strs[0]), int.Parse(strs[1]));

        }
    }

    public void SetImgFM(int index, int friendNum, int mentalNum){
        if (friendNum < 0) {
            chars[index].info.friend.sprite = sprFriend[0];
        }
        else if (friendNum == 0) {
            chars[index].info.friend.sprite = sprFriend[1];
        }
        else {
            chars[index].info.friend.sprite = sprFriend[2];
        }

        if (mentalNum < 0) {
            chars[index].info.mental.sprite = sprMental[0];
        }
        else if (mentalNum == 0) {
            chars[index].info.mental.sprite = sprMental[1];
        }
        else {
            chars[index].info.mental.sprite = sprMental[2];
        }
        chars[index].info.friendTxt.text = friendNum > 0 ? "+" + friendNum : friendNum.ToString();
        chars[index].info.mentalTxt.text = mentalNum > 0 ? "+" + mentalNum : mentalNum.ToString();

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
