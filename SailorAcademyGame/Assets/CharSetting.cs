using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharSetting : MonoBehaviour
{
    [System.Serializable]
    public struct Character {
        public string code;
        [Space]
        public Image standing;
        public TMP_Text jobTxt;
        public TMP_Text nameTxt;
        [Header("Do not change this")]
        public int isAlive;
        public int mental;
        public int relation;
    }

    public Character character;

    Characters characters;

    private void Start()
    {
        characters = GameObject.FindGameObjectWithTag("SheetData").GetComponent<Characters>();

        string[] info = characters.ReturnCharacterInfo(character.code).Split(",");

        //이름,직업,관계도,멘탈,생사여부 순서로 리턴
        character.nameTxt.text = info[0];
        character.jobTxt.text = info[1];
        character.relation = int.Parse(info[2]);
        character.mental = int.Parse(info[3]);
        character.isAlive = int.Parse(info[3]);
    }
}
