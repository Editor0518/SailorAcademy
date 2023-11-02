using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MusicManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audiosource;
    public AudioSource amb;
    public AudioSource audioSE;
    string filepath = "sound/";

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlayMusicIfExist(string str, int type)//type 0=배경음악(루프O), 1=배경음악(루프X) 2=엠비언스(루프0) 3=효과음(루프X)
    {
        audiosource.Stop();
        if (str == null || str == "") return;
        //string sceneName = SceneManager.GetActiveScene().name;
        string file="BGM";

        switch (type) {
            case 0: file = "BGM";break;
            case 1: file = "BGM";break;
            case 2: file = "BGM";break;
            case 3: file = "SE";break;
        }
        if (str != "null")
        {
            string path = filepath + file + "/" + str;// + ".wav";//Path.Combine("Assets/05. Sound/Prologue", str + ".wav");
            AudioClip obj = Resources.Load<AudioClip>(path);//(AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
            if (obj == null)
            {
                path = filepath + file + "/" + str;// + ".mp3";//Path.Combine("Assets/05. Sound/Prologue", str + ".mp3");
                obj = Resources.Load<AudioClip>(path);//(AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));


            }

            if (type <= 1)
            {
                if (obj == null)
                {
                    Debug.Log("파일이 존재하지 않습니다!!" + path);
                    audiosource.clip = null;
                    return;
                }
                audiosource.loop = type == 0 ? true : false;
                audiosource.clip = obj;
                audiosource.Play();
            }
            else if (type == 2)
            {
                if (obj == null)
                {
                    Debug.Log("파일이 존재하지 않습니다!!" + path);
                    amb.clip = null;
                    return;
                }
                amb.loop = type == 0 ? true : false;
                amb.clip = obj;
                amb.Play();

            }
            else
            {
                if (obj == null)
                {
                    Debug.Log("파일이 존재하지 않습니다!!" + path);
                    audioSE.clip = null;
                    return;
                }
                audioSE.loop = false;
                audioSE.clip = obj;
                audioSE.Play();
            }
        }
        else {
            if (type <= 1) audiosource.Stop();
            else if (type == 2) amb.Stop();
            else audioSE.Stop();
            
            
        }
    }


}
