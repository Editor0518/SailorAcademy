using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class VoiceManager : MonoBehaviour
{
    DialogSystem dialog;
    [HideInInspector]public AudioSource audiosource;

    [System.Serializable]
    public struct Vce
    {
        public string name;
        public AudioClip voice;
    }

    public List<Vce> VoiceList;

    string filepath = "sound/";//"Assets/05. Sound/";

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        dialog = GameObject.FindWithTag("Manager").GetComponent<DialogSystem>();
        StartCoroutine(IEUpdate());
    }

    public void PlayVoiceIfExist(string str) {
        audiosource.Stop();
        if (str == null || str == "") return;
        string sceneName = SceneManager.GetActiveScene().name;
        string path = filepath + sceneName + "/" + str;// +".wav";//Path.Combine("Assets/05. Sound/Prologue", str + ".wav");
        AudioClip obj = Resources.Load<AudioClip>(path); //(AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
        if (obj == null) {
            path = filepath + sceneName + "/" + str;// + ".mp3";//Path.Combine("Assets/05. Sound/Prologue", str + ".mp3");
            obj = Resources.Load<AudioClip>(path); //(AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));

            if (obj == null) { 
                Debug.Log("파일이 존재하지 않습니다!!" + path);
                audiosource.clip = null;
                return; 
            }
        }

        audiosource.clip = obj;
        audiosource.Play();
    }

    IEnumerator IEUpdate() {
        WaitForSeconds wait = new WaitForSeconds(.1f);

        WaitForSeconds wait2 = new WaitForSeconds(1f);
        while (true) {
            if (dialog.isSkip==1&&dialog.canGoNext) {
                if (!audiosource.isPlaying) {
                    dialog.ShowDialog();
                }

            }
            if (dialog.isSkip ==2 && dialog.canGoNext)
            {
                dialog.ShowDialog();
                yield return wait2;
            }

            yield return wait;
        }
    }

}
