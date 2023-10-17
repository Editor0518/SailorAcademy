using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Communication : MonoBehaviour
{
    public List<CharSetting> chars;
    public AccessToDialogueSystem dialog;

    [Header("Choice Panel")]
    public GameObject choicePanel;
    public Image standing;
    public TMP_Text nametxt;
    public TMP_Text jobtxt;

    public Image mental;
    public Image relation;

    [Header("chat")]
    public Transform content;
    
}
