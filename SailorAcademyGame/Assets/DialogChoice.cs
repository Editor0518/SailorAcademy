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
        public string sub;
        public int isLock;

        public Choice(int branch, string main, string sub, int isLock) {
            this.branch = branch;
            this.main = main;
            this.sub = sub;
            this.isLock = isLock;//-1=no lock / 0=lock / 1=open
        }
    }

    public GameObject choiceWhole;
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
    public TMP_Text subTxtA;
    public Animator lockA;
    [Space]
    public Button buttonB;
    public TMP_Text mainTxtB;
    public TMP_Text subTxtB;
    public Animator lockB;

    public void SetChoice(string question, string cAMain, string cASub, int cABranch, int cALock, string cBMain, string cBSub, int cBBranch, int cBLock) {
        this.question = question;
        cA.main = cAMain;
        cA.sub = cASub;
        cA.branch = cABranch;
        cA.isLock = cALock;
        cB.main = cBMain;
        cB.sub = cBSub;
        cB.branch = cBBranch;
        cB.isLock = cBLock;
        ShowChoice();
    }

    public void SetChoice(string question, string cAMain, string cASub, int cABranch, int cALock, string cBMain, string cBSub, int cBBranch, int cBLock, string cCMain, string cCSub, int cCBranch, int cCLock) {
        this.question = question;
        cA.main = cAMain;
        cA.sub = cASub;
        cA.branch = cABranch;
        cA.isLock = cALock;
        cB.main = cBMain;
        cB.sub = cBSub;
        cB.branch = cBBranch;
        cB.isLock = cBLock;
        cC.main = cCMain;
        cC.sub = cCSub;
        cC.branch = cCBranch;
        cC.isLock = cCLock;
        ShowChoice();
    }




    public void ShowChoice() {
        hideTxt.text = "";
        qstTxt.text = question;
        mainTxtA.text = cA.main;
        subTxtA.text = cA.sub;

        buttonA.interactable = (cA.isLock.Equals(-1) || cA.isLock.Equals(1));

        


        mainTxtB.text = cB.main;
        subTxtB.text = cB.sub;
        buttonB.interactable = (cB.isLock.Equals(-1)||cB.isLock.Equals(1));
        
        


        choiceWhole.SetActive(true);
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
        
        choiceWhole.SetActive(false);
    }

    public void WhenChoiceB() {
        
        dialogSystem.canGoNext = true;
        dialogSystem.MovebranchNext(cB.branch);
        choiceWhole.SetActive(false);
    }

}
