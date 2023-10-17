using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public Color col;

    [System.Serializable]
    public struct StandingImg
    {
        public string describe;
        public Sprite sprite;
    }

    [System.Serializable]
    public struct Character {
        public string strName;
        public string code;
        public string job;
        public float relation;
        public float mental;
        public string color;
        public int isAlive; //0=살아있음, -1=죽음, 1=실종.
        public StandingImg[] standingImg;

        public Character(string strName, string code, string job, float relation, float mental, string color, int isAlive, StandingImg[] standingImg){
            this.strName = strName;
            this.code = code;
            this.job = job;
            this.relation = relation;
            this.mental = mental;
            this.color = color;
            this.isAlive = isAlive;
            this.standingImg = standingImg;
        }

    }

    //public Character[] chracters = new Character[] { new Character("마르스", 20, 10, null), };
    public List<Character> chracters;
    public List<Character> extras;

    public string ReturnCharacterInfo(string code) {
        string info="";
        for (int i = 0; i < chracters.Count; i++) {
            if (chracters[i].code.Equals(code)) {
                info += chracters[i].strName + ",";
                info += chracters[i].job + ",";
                info += chracters[i].relation + ",";
                info += chracters[i].mental + ",";
                info += chracters[i].isAlive;
                break;
            }
        }
        return info;//이름,직업,관계도,멘탈,생사여부 순서로 리턴
    }

    Character CreateCharacterType(string strName, string code, string job, float relation, float mental, string color, int isAlive, StandingImg[] standingImg) {
        Character character;
        character.strName = strName;
        character.code = code;
        character.job = job;
        character.relation=relation;
        character.mental = mental;
        character.isAlive = isAlive;


        if (color==null||color=="") character.color = "#ffffff";
        else character.color = color;
        character.standingImg=standingImg;
        return character;
    }









}
