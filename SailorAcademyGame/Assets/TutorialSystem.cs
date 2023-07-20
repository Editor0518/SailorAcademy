using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialSystem : MonoBehaviour
{
    public Transform canvas;

    [System.Serializable]
    public struct Guide
    {
        public string name;
        public GameObject prefab;

        public Guide(string name, GameObject prefab) {
            this.name = name;
            this.prefab = prefab;
        }
    }

    public Guide[] guides;


    public void ShowInfo(string name) {
        for (int i = 0; i < guides.Length; i++) {
            if (guides[i].name.Equals(name)) {
                Instantiate(guides[i].prefab, canvas);
                
                break;
            }
        }
    }

}
