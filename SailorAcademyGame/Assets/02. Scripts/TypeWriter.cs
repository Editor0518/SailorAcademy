using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    public TMP_Text contentText;
    public string[] sentences;
    private int index;

    private void Start() {
        StartCoroutine(Type());
    }

    IEnumerator Type() {
        contentText.text = "";
        WaitForSeconds wait = new WaitForSeconds(0.03f);
        foreach (char letter in sentences[index].ToCharArray()) {
            contentText.text += letter;
            yield return wait;
        }
    }

    public void NextSentence() {
        if (index < sentences.Length - 1) {
            index++;
            contentText.text = "";
            StartCoroutine(Type());
        }
        else {
            contentText.text = "";
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(contentText.text.Length>=sentences[index].Length)
            NextSentence();
        }
    }

}
