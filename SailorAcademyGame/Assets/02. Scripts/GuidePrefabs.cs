using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guide
{
    public string name;
    public GameObject prefab;

    public Guide(string name, GameObject prefab) {
        this.name = name;
        this.prefab = prefab;
    }
}


[System.Serializable]
public class GuidePrefabs : MonoBehaviour
{
    //public Dictionary<string, Guide> Guides;
    public List<Guide> guides;


    public GameObject FindGuide(string name) {
        GameObject prefab=null;

        for (int i = 0; i < guides.Count; i++) {
            if (guides[i].name.Equals(name)) { 
                prefab = guides[i].prefab;
                break;
            }
        }
        return prefab;
    }


}
