using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DialogueStartDelay : MonoBehaviour
{
    public VideoPlayer vp;
    public Animator anim;
    public float time;

    public DialogSystem dialog;

    public bool isVideo = true;


    private void Start()
    {
        if (isVideo) StartCoroutine(WaitAndFadeOutVideo());
        else StartCoroutine(WaitAndFadeOut());
    }
    IEnumerator WaitAndFadeOut()
    {
        
        yield return new WaitForSeconds(time);
        dialog.StartDialogue();
        Debug.Log("tlekrt");
        //dialog.ShowDialog();
        this.gameObject.SetActive(false);
        yield return null;

    }

    IEnumerator WaitAndFadeOutVideo() {
        yield return new WaitForSeconds((float)vp.clip.length+0.5f);
        anim.SetTrigger("fadeout");

        yield return new WaitForSeconds(time);
        dialog.StartDialogue();
        this.gameObject.SetActive(false);
        yield return null;

    }
}
