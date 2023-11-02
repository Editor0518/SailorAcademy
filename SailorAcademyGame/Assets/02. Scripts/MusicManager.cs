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

    public void PlayMusicIfExist(string str, int type)//type 0=�������(����O), 1=�������(����X) 2=�����(����0) 3=ȿ����(����X)
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
                    Debug.Log("������ �������� �ʽ��ϴ�!!" + path);
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
                    Debug.Log("������ �������� �ʽ��ϴ�!!" + path);
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
                    Debug.Log("������ �������� �ʽ��ϴ�!!" + path);
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
