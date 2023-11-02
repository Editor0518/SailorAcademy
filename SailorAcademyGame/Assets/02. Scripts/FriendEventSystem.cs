using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendEventSystem : MonoBehaviour
{
    public DialogSystem dialog;
    Animator anim;
    bool isStart = true;
    public bool isOn = false;
    public int branch = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartEvent(int branch) {
        this.branch = branch;
        isStart = true;
        StartCoroutine(StartWait());
    }


    IEnumerator StartWait() {
        if(isStart) anim.SetTrigger("headerOn");
        Debug.Log("start");
        SetSheet(isStart);
        yield return new WaitForSeconds(isStart?4.5f:2);
        if(isStart) isOn = true;
        dialog.canGoNext = true;
        dialog.StartDialogue(branch);
        yield return null;
    }

    public void FinishEvent(int branch) {
        Debug.Log("finish");
        this.branch = branch;
        isStart = false;
        isOn = false;
        StartCoroutine(StartWait());
        
    }

    void SetSheet(bool isEvent) {
        dialog.sd.sheetData.Clear();
        dialog.td.isEvent = isEvent;
        dialog.branch = this.branch;
        dialog.crtbranch = this.branch;
        dialog.page = 0;
        dialog.LoadSheet();
        dialog.branch = this.branch;
    }


    /*
    IEnumerator WaitUntilLoad()
    {
        yield return new WaitUntil(() => sd.sheetData.Count > 0);

        for (int i = dialog.page; i < sd.sheetData.Count; i++)
        {
            if (sd.sheetData[i].Branch.Length > 0)
            {
                dialog.page = i;

                break;
            }
        }
        //dialog.prevBranch = dialog.branch;
        dialog.branch = int.Parse(sd.sheetData[dialog.page].Branch);//dialogDB.prolog[0].branch;
        dialog.ShowDialog();
        StartCoroutine(dialog.tc.TextAnimation());
        StartCoroutine(dialog.tc.TextTyping());
    }
    */
}
