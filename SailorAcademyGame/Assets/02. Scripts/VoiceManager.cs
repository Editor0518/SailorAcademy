using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class VoiceManager : MonoBehaviour
{
    AudioSource audiosource;

    [System.Serializable]
    public struct Vce
    {
        public string name;
        public AudioClip voice;
    }

    public List<Vce> VoiceList;



    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlayVoiceIfExist(string str) {
        audiosource.Stop();
        if (str == null || str == "") return;
        string sceneName = SceneManager.GetActiveScene().name;
        string path = "Assets/05. Sound/"+ sceneName+"/" + str +".wav";//Path.Combine("Assets/05. Sound/Prologue", str + ".wav");
        AudioClip obj = (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
        if (obj == null) {
            path = "Assets/05. Sound/" + sceneName + "/" + str + ".mp3";//Path.Combine("Assets/05. Sound/Prologue", str + ".mp3");
            obj = (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));

            if (obj == null) { Debug.Log("파일이 존재하지 않습니다!!" + path); return; }
        }

        audiosource.clip = obj;
        audiosource.Play();
    }

}
