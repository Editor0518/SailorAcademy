using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetData : MonoBehaviour
{
    
    public List<Data> sheetData;

    [System.Serializable]
    public struct Data
    {
        public string Branch;
        public string Background;
        public string Name;
        public string Img;
        public string Dialog;
        public string Voice;
        public string CMD;
        public string Move;

        public Data(string Branch, string Background, string Name, string Img, string Dialog, string Voice, string CMD, string Move) {
            this.Branch = Branch;
            this.Background = Background;
            this.Name = Name;
            this.Img = Img;
            this.Dialog = Dialog;
            this.Voice = Voice;
            this.CMD = CMD;
            this.Move = Move;
        }
    }

}
