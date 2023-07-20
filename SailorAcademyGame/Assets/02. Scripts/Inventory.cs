using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    /*[System.Serializable]
    public struct Item {
        public string name;
        public Transform trans;
    }*/

    [System.Serializable]
    public struct Var {
        public string name;
        public int value;

        public Var(string name, int value) {
            this.name = name;
            this.value = value;
        }
    }

    //public List<Item> itemList;
    public Transform content;
    public GameObject tile;

    public List<Var> variableList;



    public void ChangeValueInVar(int index, int value) {
        if (index >= variableList.Count) { return; }
        Var newlist = new();
        newlist.name = variableList[index].name;
        newlist.value = value;

        variableList[index]=newlist;
        //variableList[in]
    }

    public int isValueEqual(int index, int value) {
        if (index >= variableList.Count) { return -1; }
        if (variableList[index].value.Equals(value)) return 1;
        else return 0;

    }

    public void AddItemInInventory(string itemName) {
        string path = Path.Combine("Assets/04. Prefab/Items", itemName+".prefab");
        GameObject obj = (GameObject) AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        if (obj == null) { Debug.Log("파일이 존재하지 않습니다!!"+path); return; }
        for (int i = 0; i < content.childCount; i++) {
            if (content.GetChild(i).childCount == 0) {
                Instantiate(obj, content.GetChild(i));
                Debug.Log("인벤토리에 추가되었습니다.->" + i);
                break;
            }

            if(i== content.childCount - 1) { 
                Debug.Log("저장 공간이 부족합니다!"); 
            }
        }
    }

    public void RemoveItemInInventory(string itemName) {
        
        for (int i = content.childCount-1; i >= 0; i--) {
            if (content.GetChild(i).childCount > 0) {
                if (content.GetChild(i).GetChild(0).GetComponent<Item>().itemName== itemName) {
                    GameObject obj = content.GetChild(i).gameObject;
                    Destroy(obj);
                    Instantiate(tile, content);
                    Debug.Log("인벤토리에서 제거되었습니다.->"+i);
                    break;
                }
                
            }
        }
    }

}
