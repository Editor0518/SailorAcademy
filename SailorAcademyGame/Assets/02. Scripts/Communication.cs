using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Communication : MonoBehaviour
{
    Canvas canvas;
    public List<CharSetting> chars;
    public AccessToDialogueSystem dialog;

    [Header("Character Choice Panel")]
    public GameObject choicePanel;
    public Image standing;
    public TMP_Text nametxt;
    public TMP_Text jobtxt;

    public Image mental;
    public Image relation;

    [Header("chat")]
    //public RectTransform scroll;
    public RectTransform content;
    public ChatBubble chatBubble;
    public ChatBubble chatThinking;

    [Header("질문지 1")]
    public string question1 = "질문1을 입력";
    public Button ask1;

    [Header("질문지 2")]
    public string question2 = "질문2를 입력";
    public Button ask2;

    public List<string> chats=new(0);

    public void AddOneChat(string str) {
        chats.Add(str);
        chars[crtCharIndex].childOfChat.Add(str);
        ClearChat();

        ReloadChat();
    }
    public void ReloadChat() {
        if(chats==null) chats = new(0);
        //chats = (chars[crtCharIndex].childOfChat!=null?chars[crtCharIndex].childOfChat:new(0));

        if (chats.Count == 0) {
            chats.Add(chars[crtCharIndex].firstChat);
        
            for(int i=0; i<chars[crtCharIndex].childOfChat.Count; i++) {
                chats.Add(chars[crtCharIndex].childOfChat[i]);
            }
        }
        ChatBubble newBubble;
        for (int i = 0; i < chats.Count; i++) {
            if (content.childCount > i&& content.GetChild(i)) newBubble = content.GetChild(i).GetComponent<ChatBubble>();
            else{

                newBubble = Instantiate(chatBubble.gameObject, content).GetComponent<ChatBubble>();
            }
            newBubble.gameObject.SetActive(true);
            newBubble.SetText(chats[i], chars[crtCharIndex].character.color);
        }
        chatThinking.transform.parent = content.parent;
        chatThinking.transform.parent = content;
        chatThinking.SetText("", chars[crtCharIndex].character.color);

        bool askInter = ask1.interactable || ask2.interactable;

        chatThinking.gameObject.SetActive(askInter);



        content.sizeDelta = new Vector2(content.sizeDelta.x, chats.Count*133 + (askInter?0:-133));
        if (chats.Count >= 5) {
            
            content.anchoredPosition = new Vector2(0, chats.Count * 133 + (askInter ? 0 : -133));
        }
    }

    public void ClearChat() {
        SaveChat();

        chats.Clear();
        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SaveChat() {
        //if(chats.Count>0) chats.Remove(chatThinking.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text);
        //chars[crtCharIndex].childOfChat = chats;
    }

    public int crtCharIndex=0;

    private void OnEnable()
    {
        crtCharIndex = FindIndexFromChars(nametxt.name);
        if (canvas == null) canvas = gameObject.transform.parent.GetComponent<Canvas>();
        
    }

    public void SetIndexEnablePanel(int index) {
        crtCharIndex = index;
        if (!choicePanel.activeInHierarchy) choicePanel.SetActive(true);
        SetChoicePanel();
    }

    public void SetChoicePanel()
    {
        
        standing.sprite = chars[crtCharIndex].character.standing.sprite;
        nametxt.text = chars[crtCharIndex].character.nameTxt.text;
        jobtxt.text = chars[crtCharIndex].character.jobTxt.text;
        mental.fillAmount = chars[crtCharIndex].character.mental/10.0f;
        relation.fillAmount = chars[crtCharIndex].character.relation/20.0f;
        //color = chars[crtCharIndex].character.color;

        ask1.interactable = chars[crtCharIndex].canAsk1;
        ask2.interactable = chars[crtCharIndex].canAsk2;
        //ClearChat();
        ReloadChat();
    }

    public void ChangeChars(bool isRight)
    {
        crtCharIndex += isRight ? 1 : -1;
        if (crtCharIndex >= chars.Count) crtCharIndex = 0;
        if (crtCharIndex < 0) crtCharIndex = chars.Count-1;
        ClearChat();

        SetChoicePanel();
    }

    public void WhenClickAsk(int index) {

        if (index.Equals(1)) { 
            chars[crtCharIndex].canAsk1 = false;
             AddOneChat(question1);
            dialog.MoveBranch(chars[crtCharIndex].branch1);
           
            Debug.Log(chars[crtCharIndex].canAsk1);
        }
        else if (index.Equals(2)) { 
            chars[crtCharIndex].canAsk2 = false;
            AddOneChat(question2);
            dialog.MoveBranch(chars[crtCharIndex].branch2);
            
            Debug.Log(chars[crtCharIndex].canAsk2);
        }
        SetChoicePanel();
        

    }



    int FindIndexFromChars(string name) {
        for (int i = 0; i < chars.Count; i++) {
            if (chars[i].character.nameTxt.text.Equals(name)) {
                return i;
            }
        }
        return -1;
    }


    public void UpdateChars() {
        for (int i = 0; i < chars.Count; i++) {
            chars[i].UpdateIfFinished();
        }
    }

}
