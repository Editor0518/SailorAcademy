using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backgrounds : MonoBehaviour
{
    [System.Serializable]
    public struct Back {
        public string name;
        public Sprite img;
    }

    public List<Back> backgrounds;
}
