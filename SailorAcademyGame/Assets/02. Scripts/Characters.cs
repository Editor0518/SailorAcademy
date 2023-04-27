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
        public float relation;
        public float mental;
        public string color;
        public StandingImg[] standingImg;

        public Character(string strName, float relation, float mental, string color, StandingImg[] standingImg){
            this.strName = strName;
            this.relation = relation;
            this.mental = mental;
            this.color = color;
            this.standingImg = standingImg;
        }

    }

    //public Character[] chracters = new Character[] { new Character("¸¶¸£½º", 20, 10, null), };
    public List<Character> chracters;
    public List<Character> extras;



    Character CreateCharacterType(string strName, float relation, float mental, string color, StandingImg[] standingImg) {
        Character character;
        character.strName = strName;
        character.relation=relation;
        character.mental = mental;
        
        if(color==null||color=="") character.color = "#ffffff";
        else character.color = color;
        character.standingImg=standingImg;
        return character;
    }









}
