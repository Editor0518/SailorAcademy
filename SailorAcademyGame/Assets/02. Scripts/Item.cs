using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Command")]
    public string itemName; //아이템명(커맨드용)
    [Header("Looking")]
    public string itemShowName; //아이템명(커맨드용)
    public string[] itemTag;  //아이템 태그 (ex. 불쏘시개, 식량)
    //None, 식량, 막대기, 불쏘시개, 단단함, 날카로움, 의료품
    public string[] userOnly; //특정 인물에게만 사용 가능한 경우
    public int mental;  //멘탈에 주는 변동
    public int friendly;  //친밀도에 주는 변동
    [Header("Explaination")]
    [TextArea]public string content;

    [System.Serializable]
    public struct Bonus {
        public string cName;
        public int mentalAdd;
        public int friendAdd;
    }

    public Bonus[] charBonus;

}
