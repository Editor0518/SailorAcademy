using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class TileDecider : MonoBehaviour
{
    //string myVersion = "v1.01";
    // Start is called before the first frame update

    //public AudioClip[] Tilesaudio;
    //public AudioSource speaker;
    public int[] order_of_tiles;
    //public TextAsset CSVfile;
    //string[] notes;
    public TMP_Text display;
    //string[] row;
    int i = 0;
    //int starter = -1;
    //int levelno = 1;
    //AudioClip[] temp=new AudioClip[12];

    //public Animator levelcompanim;

    string rowsjson = "";
    //string[] lines;
    List<string> eachrow;
    public GameObject networkerror;
    public GameObject versionerror;
    public TMP_Text verText;
    //public gameConroller gamecontroller;
    //public Animator aim;

    public SheetData sd;
   
    protected string path1 = "https://sheets.googleapis.com/v4/spreadsheets/1cshuvUxPBE4xkH1pChXWnn3vdEz_8oDg0dFE68Hs1WA/values/";
    protected string path2= "?key=AIzaSyActNUgOKySleTsSLNYMM3OE0qB_KXee8o";
    public string sheetName = "prolog";


    public string Path() {
        return string.Concat(path1, sheetName, path2);
    }


    //bool didsend = false;

    /*void Start() {
        row = CSVfile.text.Split(new char[] { '\n' });
        if (!PlayerPrefs.HasKey("levelno"))
        {
            PlayerPrefs.SetInt("levelno", 1);
            levelno = PlayerPrefs.GetInt("levelno");
        }
        else
        {
            levelno = PlayerPrefs.GetInt("levelno");
        }
        aim.speed = 0.5f + (levelno*0.01f);
        takefromCSV();
        
        //verText.text = myVersion;
        //StartCoroutine(VersionControl());

    }*/

    /*void Update() {
        if (i < 10)
        {
            takefromCSV();
        }*/
    /*
    if (starter == 11)
    {
        levelno++;
        if (levelno > PlayerPrefs.GetInt("levelopened"))
        {
            PlayerPrefs.SetInt("levelopened", levelno);
        }
        takefromCSV();
        starter = -1;
        gamecontroller.levelwon(levelno-1);
        Debug.Log("levelno: " + levelno);
    }*/
    /*if (!didsend) {
        takefromCSV();
    }
}*/


    public void takefromCSV() {
        StartCoroutine(ObtainSheetData());
    }
    /*public AudioClip pickSound()
    {
        starter++;
        return Tilesaudio[starter];
    }
    void takerandomvalues()
    {
        for (i = 0; i < 12; i++)
        {
            temp[i] = Tilesaudio[i];
        }
        for (i = 0; i < 12; i++)
        {
            float rndm = Random.Range(0, 11);
            int rndmvalue = Mathf.FloorToInt(rndm);
            Tilesaudio[i] = temp[rndmvalue];
        }
    }*/

    

    /*IEnumerator VersionControl() {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/18xumC_FO15zwNaSr8RrWiGojUqnIgEOkhw63uqoir1Y/values/version?key=AIzaSyBSD1rsOonDjAHJEbKXttI_xers7iVrk-U");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.timeout > 2) {
            Debug.Log("error" + www.error);
            //networkerror.SetActive(true);
            //takerandomvalues();
            Debug.Log("Offline");
        }
        else {
            string json = www.downloadHandler.text;

            var o = JSON.Parse(json);

            foreach (var item in o["values"]) {
                    var itemo = JSON.Parse(item.ToString());
                //version = itemo[0].AsStringList;
                    foreach (var bro in eachrow) {
                        rowsjson += bro + ",";
                    }
                    rowsjson += "\n";
                    //rowsjson = "";
                    /*foreach (var bro in eachrow) {
                        rowsjson += bro + ",";
                    }
            } 
            //if(versionerror!=null)versionerror.SetActive(!version[0].Equals(myVersion));
        }
    }*/


    IEnumerator ObtainSheetData() {
        UnityWebRequest www = UnityWebRequest.Get(Path());
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.timeout > 2) {
            Debug.Log("error" + www.error);
            //networkerror.SetActive(true);
            //takerandomvalues();
            Debug.Log("Offline");
        }
        else {//네트워크 연결 성공했을 떄
            //networkerror.SetActive(false);

            sd.sheetData.Clear();
            //didsend = true;
            string json = www.downloadHandler.text;

            var o = JSON.Parse(json);
            int a = 0;

            foreach (var item in o["values"]) {
                if (a > 0) {
                    var itemo = JSON.Parse(item.ToString());
                    eachrow = itemo[0].AsStringList;
                    /*foreach (var bro in eachrow) {
                        rowsjson += bro + ",";
                    }
                    rowsjson += "\n";*/
                    rowsjson = "";
                    foreach (var bro in eachrow) {
                        rowsjson += bro + "@";
                    }
                    //Debug.LogFormat(rowsjson);
                    //주석일시 스킵하는 기능 추가
                    if (!rowsjson.Contains("//")) {
                        SheetData.Data data = FillSheetData(rowsjson);
                        sd.sheetData.Add(data);
                    }
                    

                }
                a++;
            }
            //lines = rowsjson.Split(new char[] { '\n' });


            /*for (int n=0; n<1; n++) {
                Debug.Log(levelno);
                //notes = lines[levelno].Split(new char[] { ',' });


                Data data = FillSheetData(lines[levelno].Split(new char[] { ',' }));
                sheetData.Add(data);
                Debug.Log(data);


                //for (i = 0; i < notes.Length - 1; i++) {
                    //display.text += ", " + notes[i];

                    //temp[i - 1] = Tilesaudio[i - 1];
                //}
            }*/

            /*
            for (i = 1; i < notes.Length-1; i++)
            {
                //Tilesaudio[i - 1] = temp[int.Parse(notes[i]) - 1];
                Tilesaudio[int.Parse(notes[i]) - 1] = temp[i - 1];
            }*/
        }
    }

    SheetData.Data FillSheetData(string json) {
       
        string[] value = json.ToString().Split("@");
        
        return FillSheetData(value);
    }

    SheetData.Data FillSheetData(string[] lines) {
        SheetData.Data data;
        data.Branch = "";
        data.Background = "";
        data.Name = "";
        data.Img = "";
        data.Dialog = "";
        data.Voice = "";
        data.CMD = "";
        data.Move = "";

       
        if (lines.Length > 0) data.Branch = lines[0];
        if (lines.Length > 1) data.Background = lines[1];
        if (lines.Length > 2) data.Name = lines[2];
        if (lines.Length > 3) data.Img = lines[3];
        if (lines.Length > 5) data.Dialog = lines[5];
        if (lines.Length > 6) data.Voice = lines[6];
        if (lines.Length > 7) data.CMD = lines[7];
        if (lines.Length > 8) data.Move = lines[8];
        
        return data;

    }

}
