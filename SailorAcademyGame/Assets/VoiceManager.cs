using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if (int.TryParse(str, out int index)) {
            audiosource.clip = VoiceList[index].voice;
        }
        else {
            bool didFind = false;
            for (int i = 0; i < VoiceList.Count; i++) {
                if (str.Contains(VoiceList[i].name)) {
                    audiosource.clip = VoiceList[i].voice;
                    didFind = true;
                    break;
                }
            }
            if (!didFind) return;
        }
        audiosource.Play();
    }

}
