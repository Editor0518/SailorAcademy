using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendEventSystem : MonoBehaviour
{
    public DialogSystem dialog;
    Animator anim;

    public bool isOn = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartEvent(int branch) {
        StartCoroutine(StartWait(branch));
    }


    IEnumerator StartWait(int branch) {
        anim.SetTrigger("headerOn");
        Debug.Log("start");
        SetSheet(true, branch);
        yield return new WaitForSeconds(4.5f);
        isOn = true;
        
        
        yield return null;
    }

    public void FinishEvent(int branch) {
        Debug.Log("finish");
        SetSheet(false, branch);
        
        isOn = false;
    }

    void SetSheet(bool isEvent, int branch) {
        dialog.sd.sheetData.Clear();
        dialog.td.isEvent = isEvent;
        dialog.branch = branch;
        dialog.crtbranch = branch;
        dialog.page = 0;
        dialog.LoadSheet();
        dialog.branch = branch;
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
