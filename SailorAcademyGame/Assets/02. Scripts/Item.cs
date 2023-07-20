using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Command")]
    public string itemName; //�����۸�(Ŀ�ǵ��)
    [Header("Looking")]
    public string itemShowName; //�����۸�(Ŀ�ǵ��)
    public string[] itemTag;  //������ �±� (ex. �ҽ�ð�, �ķ�)
    //None, �ķ�, �����, �ҽ�ð�, �ܴ���, ��ī�ο�, �Ƿ�ǰ
    public string[] userOnly; //Ư�� �ι����Ը� ��� ������ ���
    public int mental;  //��Ż�� �ִ� ����
    public int friendly;  //ģ�е��� �ִ� ����
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
