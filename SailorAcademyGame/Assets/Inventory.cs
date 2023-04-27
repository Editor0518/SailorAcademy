using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public struct Item {
        public string name;
        public Transform trans;
    }

    [System.Serializable]
    public struct Var {
        public string name;
        public int value;

        public Var(string name, int value) {
            this.name = name;
            this.value = value;
        }
    }

    

    public List<Item> itemList;

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
}
