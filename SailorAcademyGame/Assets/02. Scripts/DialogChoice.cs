using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogChoice : MonoBehaviour
{
    public DialogSystem dialogSystem;

    public struct Choice
    {
        public int branch;
        public string main;
        public int isLock;

        public Choice(int branch, string main, int isLock) {
            this.branch = branch;
            this.main = main;
            this.isLock = isLock;//-1=no lock / 0=lock / 1=open
        }
    }

    public GameObject choiceWhole;
    public Animator choiceWholeA;
    public Animator choiceAnim;
    public RectTransform choiceHolder;
    public string question;

    public Choice cA;
    public Choice cB;
    public Choice cC;

    public TMP_Text hideTxt;
    public TMP_Text qstTxt;
    [Space]
    public Button buttonA;
    public TMP_Text mainTxtA;
    public Animator lockA;
    [Space]
    public Button buttonB;
    public TMP_Text mainTxtB;
    public Animator lockB;

    //2개짜리 선택지
    public void SetChoice(string question, string cAMain, int cABranch, int cALock, string cBMain, int cBBranch, int cBLock) {
        this.question = question;
        cA.main = cAMain;
        cA.branch = cABranch;
        cA.isLock = cALock;
        cB.main = cBMain;
        cB.branch = cBBranch;
        cB.isLock = cBLock;
        ShowChoice();
    }

    //3개짜리 선택지
    public void SetChoice(string question, string cAMain, int cABranch, int cALock, string cBMain, int cBBranch, int cBLock, string cCMain, int cCBranch, int cCLock) {
        this.question = question;
        cA.main = cAMain;
        cA.branch = cABranch;
        cA.isLock = cALock;
        cB.main = cBMain;
        cB.branch = cBBranch;
        cB.isLock = cBLock;
        cC.main = cCMain;
        cC.branch = cCBranch;
        cC.isLock = cCLock;
        ShowChoice();
    }

    public void ShowChoice() {
        hideTxt.text = "";
        qstTxt.text = question;
        mainTxtA.text = cA.main;

        buttonA.interactable = (cA.isLock.Equals(-1) || cA.isLock.Equals(1));

        mainTxtB.text = cB.main;
        buttonB.interactable = (cB.isLock.Equals(-1)||cB.isLock.Equals(1));

        Debug.Log("선택지");
        

        //choiceWhole.SetActive(true);
        choiceAnim.SetBool("choice", true);
        choiceWholeA.SetBool("choice", true);
        dialogSystem.canAutoSkip = false;

        if (cA.isLock.Equals(-1)) { }//lockA.gameObject.SetActive(false);
        else {
            
            //lockA.gameObject.SetActive(true);
            lockA.SetTrigger(cA.isLock.Equals(1)?"on":"off");
            //lockA.SetBool("isOn", );
        }

        if (cB.isLock.Equals(-1)) { } //lockB.gameObject.SetActive(false);
        else {
            //lockB.gameObject.SetActive(true);
            lockB.SetTrigger(cB.isLock.Equals(1) ? "on" : "off");
            //lockB.SetBool("isOn", cB.isLock.Equals(1));
        }
    }




    public void WhenChoiceA() {
        dialogSystem.canGoNext = true;
        dialogSystem.MovebranchNext(cA.branch);
        choiceAnim.SetBool("choice", false);
        choiceWholeA.SetBool("choice", false);
        //choiceWhole.SetActive(false);
    }

    public void WhenChoiceB() {
        
        dialogSystem.canGoNext = true;
        dialogSystem.MovebranchNext(cB.branch);
        choiceAnim.SetBool("choice", false);
        choiceWholeA.SetBool("choice", false);
        //choiceWhole.SetActive(false);
    }

}
