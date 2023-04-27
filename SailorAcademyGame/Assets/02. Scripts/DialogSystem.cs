using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public SheetData sd;
    public Characters ch;

    [SerializeField] DialogChoice dialogChoice;
    [SerializeField] DialogDB dialogDB;

    [SerializeField] TextController tc;
    [SerializeField] TileDecider td;
    [SerializeField] int branch;
    [SerializeField] int crtbranch;
    [SerializeField] Image imgStanding;
    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtDialog;


    [SerializeField] VoiceManager vm;
    [SerializeField] Inventory iv;
    AudioSource audiosource;
    [SerializeField] AudioClip dialClip;
    
    [SerializeField] Animator mentalAnim;
    [SerializeField] AudioClip[] men_updown;
    [SerializeField] Animator relationAnim;
    [SerializeField] AudioClip[] rel_updown;


    public int page = 0;
    public bool canGoNext = true;
    
   
    private void Awake() {
        /*int index = 0;
        for (int i = 0; i < dialogDB.prolog.Count; ++i) {

            if (dialogDB.prolog[i].branch.Equals(branch)) {

            }
        }*/
        audiosource = GetComponent<AudioSource>();
        td.takefromCSV();
        StartCoroutine(WaitUntilLoad());
    }

    IEnumerator WaitUntilLoad() {
        yield return new WaitUntil(()=>sd.sheetData.Count > 0);

        for (int i = page; i < sd.sheetData.Count; i++) {
            if (sd.sheetData[i].Branch.Length > 0) {
                page = i;

                break;
            }
        }
        branch = int.Parse(sd.sheetData[page].Branch);//dialogDB.prolog[0].branch;
        ShowDialog();
        StartCoroutine(tc.TextAnimation());
        StartCoroutine(tc.TextTyping());
    }

    void PageSettingOnBranch() {
        for (int i = page; i < sd.sheetData.Count; i++) {
            if (sd.sheetData[i].Branch.Length > 0) {
                if (sd.sheetData[i].Branch.Equals(branch.ToString())) {
                    page = i;

                    break;
                }
            }
        }
    }

    public void ShowDialog() {
        /*for (; (sd.sheetData[page].Branch != branch.ToString()); page++) {
            if (sd.sheetData[page].Branch.Equals(branch.ToString())) break;
        }*/
        //if (sd.sheetData[page].Branch.Equals("")) { }
        //else if (!int.Parse(sd.sheetData[page].Branch).Equals(branch)) { page++;  }
        if (tc.isTyping) {
            
            return;
        }
        
        ExecuteCmd(sd.sheetData[page].Name);
        
        SetDialog(sd.sheetData[page].Name);
        PlayVoiceFromList();
        ReadHCMD(sd.sheetData[page].CMD);
        ChangeBranch(sd.sheetData[page].Move);
        
        //page = index;

        //dialogDB.character[int.Parse(dialogDB.prolog[index].name)].name;
        //txtDialog.text= ChangeNameColor(ch.[index].dialog);
        page++;
    }

    void ReadHCMD(string cmd) {
        if (cmd == "" || cmd == null) return;
        if (!cmd.Contains("=")) { return; }

        if (cmd.Contains("=")) {
            string[] s = cmd.Split("=");
            
            if (int.TryParse(s[0], out int result)) {
                iv.ChangeValueInVar(result, int.Parse(s[1]));
            }
            else {
                //s[0]의 값을 string으로 받아와서 맞는 변수를 찾아서 설치하기   
            }
        }
        else if (cmd.Contains("==")) {
            string[] s = cmd.Split("==");

            if (int.TryParse(s[0], out int result)) {
                iv.isValueEqual(result, int.Parse(s[1]));
            }
            else {
                //s[0]의 값을 string으로 받아와서 맞는 변수를 찾아서 설치하기   
            }
        }
    }

    public int crtVar = -1;

    int IntReadHCMD(string cmd) {
        if (cmd == "" || cmd == null) { crtVar = -1; return -1; }
        if (!cmd.Contains("==")) { crtVar = -1; return -1; }

        if (cmd.Contains("==")) {
            string[] s = cmd.Split("==");

            if (int.TryParse(s[0], out int result)) {
                crtVar= result;
                return iv.isValueEqual(result, int.Parse(s[1]));
            }
            else {
                //s[0]의 값을 string으로 받아와서 맞는 변수를 찾아서 설치하기   
                crtVar = -1;
                return -1;
            }
        }
        crtVar = -1;
        return -1;
    }

    void PlayVoiceFromList() {
        vm.PlayVoiceIfExist(sd.sheetData[page].Voice);
    }

    void SetDialog(string n) {
        txtName.text = n;
        txtName.color = ChangeNameColorOfText(n);
        //txtDialog.text = sd.sheetData[page].Dialog;
        tc.TextChanged(sd.sheetData[page].Dialog);
        

        //txtDialog.gameObject.SetActive(false);
        txtDialog.color = ChangeNameColorOfText(n);
        //txtDialog.gameObject.SetActive(true);
        audiosource.PlayOneShot(dialClip);
        ChangeSprite(n);
    }

    Color ChangeNameColorOfText(string name) {
        Color color=new Color(255,255,255,100);

        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].strName)) {
                if (ColorUtility.TryParseHtmlString(ch.chracters[i].color, out color)) {
                    return color;
                }
                else return color;
            }
        }
        return color;
    }

    void ChangeSprite(string name) {
        Color none = Color.clear;
        Color all = Color.white;
        bool isFinished = false;

        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].strName)) {
                imgStanding.color = all;
                imgStanding.sprite=ch.chracters[i].standingImg[0].sprite;
                
                isFinished = true;
                break;
            }
        }
        if(!isFinished) imgStanding.color = none;
        
    }

    public void NextDialog() {
        if(canGoNext) ShowDialog();
    }

    struct cNameColor {
        public string cName;
        public string cColor;

        public cNameColor(string cName, string cColor) {
            this.cName = cName;
            this.cColor = cColor;
        }

    }

    void ChangeBranch(string num) {
        if (num == "" || num == null) return;
        if (int.TryParse(num, out int result)) {
            Movebranch(result);
        }
    }

    cNameColor[] cNameCols;


    void MakeCNameColor() {
        cNameCols = new cNameColor[27];
        for (int i = 0; i < cNameCols.Length; i++) {
            if (!dialogDB.character[i].color.Equals("")) cNameCols[i].cName = dialogDB.character[i].name;
            if(!dialogDB.character[i].color.Equals("")) cNameCols[i].cColor = dialogDB.character[i].color;
            else cNameCols[i].cColor = "#ffffff";
        }

    }

    string ChangeNameColor(string content) {
        if (cNameCols == null) { MakeCNameColor(); }
        for (int i = 0; i < cNameCols.Length; i++) {
            if (content.Contains("["+cNameCols[i].cName+"]")) {
                content = content.Replace("[" + cNameCols[i].cName + "]", "<color=" + cNameCols[i].cColor + "><b>" + cNameCols[i].cName + "</b></color>");
            }
        }
        return content;
    }

    void ExecuteCmd(string cmd) {
        if (cmd.Equals("선택지")) {
            canGoNext = false;
            int choices=int.Parse(sd.sheetData[page].Img);

            string dialogA = sd.sheetData[page + 1].Dialog;
            string subA = sd.sheetData[page + 1].CMD;
            int branchA = int.Parse(sd.sheetData[page + 1].Img);
            int lockA = IntReadHCMD(sd.sheetData[page + 1].Voice);


            string dialogB = sd.sheetData[page + 2].Dialog;
            string subB = sd.sheetData[page + 2].CMD;
            int branchB = int.Parse(sd.sheetData[page + 2].Img);
            int lockB = IntReadHCMD(sd.sheetData[page + 2].Voice);

            if (choices.Equals(3)) {
                string dialogC = sd.sheetData[page + 3].Dialog;
                string subC = sd.sheetData[page + 3].CMD;
                int branchC = int.Parse(sd.sheetData[page + 3].Img);
                int lockC = IntReadHCMD(sd.sheetData[page + 3].Voice);

                dialogChoice.SetChoice(sd.sheetData[page].Dialog, dialogA, subA, branchA, lockA, dialogB, subB, branchB, lockB, dialogC, subC, branchC, lockC);

            }
            else {
                dialogChoice.SetChoice(sd.sheetData[page].Dialog, dialogA, subA, branchA, lockA, dialogB, subB, branchB, lockB);

            }
            //page += choices;

        }
        else if (cmd.Equals("친밀도")){
            
            string relation = sd.sheetData[page].Dialog;
            string[] split = relation.Split(":");
            string[] cas = split[0].Split(",");
            int n = 0;
            if (split[1].Contains("상승")) {
                n = 1;
                relationAnim.SetTrigger("up");
                audiosource.PlayOneShot(rel_updown[0]);
            }

            if (split[1].Contains("하락")) {
                n = -1;
                relationAnim.SetTrigger("down");
                audiosource.PlayOneShot(rel_updown[1]);
            }
            
            page++;
            
            for (int i = 0; i < cas.Length; i++) {
                ChangeRelation(cas[i], n);
                
            }
            
        }
        else if (cmd.Equals("멘탈")) {

            string relation = sd.sheetData[page].Dialog;
            string[] split = relation.Split(":");
            string[] cas = split[0].Split(",");
            int n = 0;
            if (split[1].Contains("상승")) {
                n = 1;
                mentalAnim.SetTrigger("up");
                audiosource.PlayOneShot(men_updown[0]);
            }

            if (split[1].Contains("하락")) {
                n = -1;
                mentalAnim.SetTrigger("down");
                audiosource.PlayOneShot(men_updown[1]);
            }

            page++;

            for (int i = 0; i < cas.Length; i++) {
                ChangeMental(cas[i], n);

            }

        }
        else if (cmd.Equals("시스템")) {
            
        }
        
    }

    void ChangeRelation(string name, int n) {
        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].strName)) {
                Characters.Character cha =ch.chracters[i];
                cha.relation += n;
                ch.chracters[i] = cha;
            }
        }

    }

    void ChangeMental(string name, int n) {
        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].strName)) {
                Characters.Character cha = ch.chracters[i];
                cha.relation += n;
                ch.chracters[i] = cha;
            }
        }

    }

    void ExitDialogue() {
        canGoNext = false;
        gameObject.SetActive(false);
    }

    /*
    void ChoiceMode(int index, string cmd) {
        string[] branch = cmd.Split("_");
        int cAi = int.Parse(branch[1]);
        int cBi = int.Parse(branch[2]);

        string[] cAs= dialogDB.prolog[index].cA.Split("<br>");
        string[] cBs= dialogDB.prolog[index].cB.Split("<br>");

        cAs[1] = MergeChoiceArray(cAs);
        cBs[1] = MergeChoiceArray(cBs);

        dialogChoice.gameObject.SetActive(true);
        dialogChoice.SetChoice(ChangeNameColor(dialogDB.prolog[index].dialog), cAs[0], cAs[1], cAi, cBs[0], cBs[1], cBi);
        canGoNext = false;
    }
   

    string MergeChoiceArray(string[] c) {
        if (c.Length > 2) {
            for (int i = 2; i < c.Length; i++) {
                c[1] += c[i];
            }
        }
        return c[1]==null?"" : c[1];
    }
 */

    public void LoadBackground(string background) {
        string[] cmd=  background.Split(".");

        if (cmd[1] != null) {
            
        }

    }


    void BackgroundAnimation() {
    
    }


    public void Movebranch(int branch) {
        this.branch = branch;
        PageSettingOnBranch();

        NextDialog();
    }

    public void MovebranchNext(int branch) {
        this.branch = branch;
        PageSettingOnBranch();

        NextDialog();
    }

    private void Update() {
        if (canGoNext) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                ShowDialog();
            }
        }

    }

}
