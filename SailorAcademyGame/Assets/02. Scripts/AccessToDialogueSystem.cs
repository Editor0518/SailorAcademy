using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessToDialogueSystem : MonoBehaviour
{
    [HideInInspector]public DialogSystem ds;

    [Header("มฆวั")]
    public bool hasLimitToRequire = false;
    public Image blockImg;

    public int requireNum = 0;
    public int currentNum = 0;

    public GameObject WarnObjWhenNotRequireNum;

    public string msg;
    public TMP_Text msgTxt;

    


    public void RequireNumPlus(int add) {
        currentNum += add;
    }

    // Start is called before the first frame update
    void Start() {
        ds = GameObject.FindWithTag("Manager").GetComponent<DialogSystem>();
    }

    private void Update() {
        if (requireNum > 0 && msg != "" && msgTxt!=null) {
            msgTxt.text = msg + " " + currentNum + "/" + requireNum;
        }
        if (hasLimitToRequire&&blockImg != null) {
            if (!blockImg.enabled && currentNum>=requireNum) blockImg.enabled = true;
        }
    }

    public void MoveBranchWhenRequire(int branch) {
        if (currentNum < requireNum) {
            if (WarnObjWhenNotRequireNum != null) { 
                WarnObjWhenNotRequireNum.SetActive(false);
                WarnObjWhenNotRequireNum.SetActive(true);
            }
            return;
        }
        MoveBranch(0);
        MoveBranch(branch);
        gameObject.SetActive(false);
        DestroyThis();

    }



    public void MoveBranch(int branch) {
        ds.dialogWholeObj.SetActive(true);
        if(ds.com_canvas.enabled) ds.com_canvas.enabled = false;
        ds.Movebranch(branch);
    }

    public void DestroyThis() {
        Destroy(gameObject);
    }


}
