using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Spine.Unity;

public class DialogSystem : MonoBehaviour
{
    #region 변수
    [HideInInspector]public SheetData sd;
    [HideInInspector] public Characters ch;

    [SerializeField] DialogChoice dialogChoice;
    [SerializeField] DialogueShow dialogShow;
    [SerializeField] InfoSystem infoSystem;
    [SerializeField] DialogDB dialogDB;

    FirstGearGames.SmoothCameraShaker.CameraShaker cs;
    GuidePrefabs gp;

    [SerializeField] TextController tc;
    TileDecider td;
    [SerializeField] int branch;
    [SerializeField] int crtbranch;

    [SerializeField] Transform inspectTrans;
    [SerializeField] GameObject backFilter;
    [SerializeField] Image[] imgStanding;

    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtDialog;
    [SerializeField] GameObject dialogWholeObj;

    public GameObject chapterEnd;
    RecordOn ro;
    VoiceManager vm;
    Inventory iv;
    [SerializeField] CharacterIntroduce ci;
    AudioSource audiosource;
    [SerializeField] AudioClip dialClip;


    [Header("Animation")]
    //[SerializeField] AnimationReferenceAsset animRef;
    [SerializeField] SkeletonAnimation skelAnim;//SkeletonAnimation
    public Spine.Skeleton skel;
    public Spine.AnimationState animState;
    //[SerializeField] Animator mentalAnim;
    [SerializeField] AudioClip[] men_updown;
    //[SerializeField] Animator relationAnim;
    [SerializeField] AudioClip[] rel_updown;



    [SerializeField] Transform guideTrans;

    public int page = 0;
    public bool canGoNext = true;

    bool showDialogue = true;

    public bool isSkip=false;
    public bool canAutoSkip = false;

    cNameColor[] cNameCols;


    #endregion

    public void ChangeIsSkip(Toggle tog) {
        isSkip = tog.isOn;
        //if(isSkip&& skipItself==null) skipItself=StartCoroutine(SkipItSelf());
        
    }

    Coroutine skipItself;

    private void Start() {
        gp = transform.GetChild(0).GetComponent<GuidePrefabs>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstGearGames.SmoothCameraShaker.CameraShaker>();
        ro = GameObject.FindGameObjectWithTag("RecordOn").GetComponent<RecordOn>();

        
        vm = GameObject.FindGameObjectWithTag("Voice").GetComponent<VoiceManager>();

        skelAnim.Skeleton.SetToSetupPose();
        skelAnim.Skeleton.SetBonesToSetupPose();
        skelAnim.Skeleton.SetSlotsToSetupPose();
    }

    private void Awake() {
        /*int index = 0;
        for (int i = 0; i < dialogDB.prolog.Count; ++i) {

            if (dialogDB.prolog[i].branch.Equals(branch)) {

            }
        }*/
        audiosource = GetComponent<AudioSource>();
        GameObject sheetData = GameObject.FindGameObjectWithTag("SheetData");
        sd = sheetData.GetComponent<SheetData>();
        ch = sheetData.GetComponent<Characters>();
        td = sheetData.GetComponent<TileDecider>();
        iv = sheetData.GetComponent<Inventory>();
        //구글 스프레드시트 값 가져옴
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

    //스프레드 시트에 표기된 Branch에 따라 시작 페이지를 설정. 페이지 이동 할 때 사용.
    void PageSettingOnBranch() {
        for (int i = 0; i < sd.sheetData.Count; i++) {
            if (sd.sheetData[i].Branch.Contains("//")) { }
            else if (sd.sheetData[i].Branch.Length > 0) {
                if (sd.sheetData[i].Branch.Equals(branch.ToString())) {
                    page = i;

                    break;
                }
            }
        }
    }

    //다이얼로그 읽고 화면에 보여주기.
    public void ShowDialog() {
        /*for (; (sd.sheetData[page].Branch != branch.ToString()); page++) {
            if (sd.sheetData[page].Branch.Equals(branch.ToString())) break;
        }*/
        //if (sd.sheetData[page].Branch.Equals("")) { }
        //else if (!int.Parse(sd.sheetData[page].Branch).Equals(branch)) { page++;  }
        if (tc.isTyping) {  
            return;
        }
        if (page >= sd.sheetData.Count) { Debug.Log("넘침"); ExitDialogue(); gameObject.SetActive(false); return; }
        
        if (infoSystem.whole.activeInHierarchy) infoSystem.whole.SetActive(false);


        if (backFilter.activeInHierarchy) backFilter.SetActive(false);
        showDialogue = true;
        ExecuteCmd(sd.sheetData[page].Name);

        if (showDialogue) {
            SetDialog(sd.sheetData[page].Name);
            PlayVoiceFromList();
        }

        ReadHCMD(sd.sheetData[page].CMD);
        ChangeBranch(sd.sheetData[page].Move);
        
        //page = index;

        //dialogDB.character[int.Parse(dialogDB.prolog[index].name)].name;
        //txtDialog.text= ChangeNameColor(ch.[index].dialog);
        page++;
    }

    //cmd를 읽고 해석
    void ReadHCMD(string cmd) {
        if (cmd == "" || cmd == null) return;
        Debug.Log("cmd: "+cmd);
        if (cmd.Contains("s!")) {//s!1
            string length = cmd.Replace("s!", "");

        }
        if (!iv.gameObject.activeInHierarchy) iv = GameObject.FindWithTag("DontDestroy").GetComponent<Inventory>();

        if (cmd.Contains("gv_")) {
            string name = cmd.Replace("gv_", "");
            iv.AddItemInInventory(name);
        }
        if (cmd.Contains("del_")) {
            string name = cmd.Replace("del_", "");
            iv.RemoveItemInInventory(name);
        }

        if (cmd.Contains("행적")) {
            string[] str = cmd.Split("_");
            ro.TextOn(str[1]);
        }

        if (cmd.Contains("가이드")) {
            canGoNext = false;

            string[] str = cmd.Split("_");
            GameObject guide = Resources.Load<GameObject>(guideFilePath + str[1]);
            GuideInfo info = guide.GetComponent<GuideInfo>();

            guideWhole.SetActive(true);
            guideTitle.text = info.title;
            guideContent.text = info.content;

            //txtDialog.enabled = false;
            //txtName.enabled = false;
        }


        if (!cmd.Contains("=")) { return; }
        if (cmd.Contains("iv.")) {
                    cmd = cmd.Replace("iv.", "");
            
                    if (cmd.Contains("==")) {
                        string[] s = cmd.Split("==");

                        if (int.TryParse(s[0], out int result)) {
                            iv.isValueEqual(result, int.Parse(s[1]));
                        }
                        else {
                            //s[0]의 값을 string으로 받아와서 맞는 변수를 찾아서 설치하기   
                        }
                    }
                    else if (cmd.Contains("="))
                    {
                        string[] s = cmd.Split("=");

                        if (int.TryParse(s[0], out int result))
                        {
                            iv.ChangeValueInVar(result, int.Parse(s[1]));
                        }
                        else
                        {
                            //s[0]의 값을 string으로 받아와서 맞는 변수를 찾아서 설치하기   
                        }
                    }
                }

        else if (cmd.Contains("vh.")) {
            cmd = cmd.Replace("vh.", "");
            if (cmd.Contains("=="))
            {
                branch = VariableHolder.CompareVariableReturnBranch(cmd);
                Debug.Log("==호출됨");
            }
            else if (cmd.Contains("=")) {
                VariableHolder.SetVariable(cmd);

                Debug.Log("=호출됨");
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

    //다이얼로그 적용들
    void SetDialog(string n) {
        txtName.text = n;
        txtName.color = ChangeNameColorOfText(n);
        //txtDialog.text = sd.sheetData[page].Dialog;
        tc.TextChanged(sd.sheetData[page].Dialog);
        

        //txtDialog.gameObject.SetActive(false);
        txtDialog.color = ChangeNameColorOfText(n);
        //txtDialog.gameObject.SetActive(true);
        if(!audiosource.isPlaying)audiosource.PlayOneShot(dialClip);

        dialogShow.LoadBackground(sd.sheetData[page].Background);

        string[] strs = ArrayOfSheetImg(sd.sheetData[page].Img);

        for (int i = 0; i < strs.Length; i++) {
            ChangeSprite(i, strs[i]);
            
            
        }

        string spot = sd.sheetData[page].Spot;
        if (spot != "") {
            int index = Array.IndexOf(strs, spot);
            if (index < 0)
            {
                index = 0;

            }
            imgStanding[index].transform.parent.gameObject.SetActive(true);
            ChangeSpriteSpotlight(index, spot);

            

        }
        
    }

    string[] ArrayOfSheetImg(string img) {
        string[] strs = img.Replace(" ", "").Split(",");

        for (int i = 0; i < imgStanding.Length; i++) {
            imgStanding[i].transform.parent.gameObject.SetActive(i < strs.Length);
        }

        return strs;
    }

    Color ChangeNameColorOfText(string name) {
        Color color=new Color(255,255,255,100);
        /*
        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].strName)) {
                if (ColorUtility.TryParseHtmlString(ch.chracters[i].color, out color)) {
                    return color;
                }
                else return color;
            }
        }*/
        return color;
    }

    void ChangeSprite(int index, string name) {
        Color none = Color.clear;
        Color all = Color.grey;
        bool isFinished = false;

        if (name == "") { imgStanding[index].color = none; return; }

        for (int i = 0; i < ch.chracters.Count; i++) {
            if (name.Contains(ch.chracters[i].code)) {
                imgStanding[index].color = all;
                imgStanding[index].sprite=ch.chracters[i].standingImg[0].sprite;
                
                isFinished = true;
                break;
            }
            
        }
        if(!isFinished) imgStanding[index].color = none;
        
    }

    void ChangeSpriteSpotlight(int index, string name) {
        if (!name.Contains("c_"))
        {
            dialogShow.HideMidItem();
            ChangeSprite(index, name);
            imgStanding[index].color = Color.white;
        }
        else
        {
            dialogShow.LoadMidItem(name);
            imgStanding[index].color = Color.clear;
        }

        
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
            MovebranchNext(result);
        }
    }



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

    //Spine.Skin friendUp, friendDown, mentalUp, mentalDown;


    void ExecuteCmd(string cmd) {
        /*friendUp = skelAnim.SkeletonData.FindSkin("Friend/up/Friend0");
        friendDown = skelAnim.SkeletonData.FindSkin("Friend/down/Friend-0");
        mentalUp = skelAnim.SkeletonData.FindSkin("Mental/up/Mental0");
        mentalDown = skelAnim.SkeletonData.FindSkin("Mental/down/Mental-0");

        friendUp = new Spine.Skin("friend Up");
        friendDown = new Spine.Skin("friend Down");
        mentalUp = new Spine.Skin("mental Up");
        mentalDown = new Spine.Skin("mental Down");

        friendUp.AddSkin(skelAnim.SkeletonData.FindSkin("Friend/up/Friend0"));
        friendDown.AddSkin(skelAnim.SkeletonData.FindSkin("Friend/down/Friend-0"));
        mentalUp.AddSkin(skelAnim.SkeletonData.FindSkin("Mental/up/Mental0"));
        mentalDown.AddSkin(skelAnim.SkeletonData.FindSkin("Mental/down/Mental-0"));*/
        canAutoSkip = false;
        skelAnim.gameObject.SetActive(false);
        if (cmd.Equals("선택지"))
        {
            canGoNext = false;
            int choices = int.Parse(sd.sheetData[page].Img);

            string dialogA = sd.sheetData[page + 1].Dialog;
            int branchA = int.Parse(sd.sheetData[page + 1].Img);
            int lockA = IntReadHCMD(sd.sheetData[page + 1].Voice);


            string dialogB = sd.sheetData[page + 2].Dialog;
            int branchB = int.Parse(sd.sheetData[page + 2].Img);
            int lockB = IntReadHCMD(sd.sheetData[page + 2].Voice);

            if (choices.Equals(3))
            {
                string dialogC = sd.sheetData[page + 3].Dialog;
                int branchC = int.Parse(sd.sheetData[page + 3].Img);
                int lockC = IntReadHCMD(sd.sheetData[page + 3].Voice);

                dialogChoice.SetChoice(sd.sheetData[page].Dialog, dialogA, branchA, lockA, dialogB, branchB, lockB, dialogC, branchC, lockC);

            }
            else
            {
                dialogChoice.SetChoice(sd.sheetData[page].Dialog, dialogA, branchA, lockA, dialogB, branchB, lockB);

            }
            ChangeSpriteSpotlight(0, sd.sheetData[page].Spot);
            backFilter.SetActive(true);

            //page += choices;

        }
        else if (cmd.Equals("친밀도"))
        {
            skelAnim.gameObject.SetActive(true);
            canAutoSkip = true;
            Spine.Skeleton skeleton = new(skelAnim.Skeleton.Data);
            string relation = sd.sheetData[page].Dialog;
            string[] split = relation.Split(":");
            string[] cas = split[0].Split(",");
            int n = 0;
            if (split[1].Contains("상승"))
            {
                n = 1;
                //relationAnim.SetTrigger("up");
                skeleton.SetSkin("Friend");
                //skelAnim.Skeleton.SetSkin("Friend0");
                //skelAnim.Skeleton.SetSkin(friendUp);
                skelAnim.initialSkinName = "Friend";
                //skelAnim.AnimationState.
                //skelAnim.startingAnimation = "Up";
                skelAnim.AnimationState.SetAnimation(0, "Up1", true);
                //skelAnim.AnimationState.SetAnimation(0, "up", true);
                audiosource.PlayOneShot(rel_updown[0]);
            }

            if (split[1].Contains("하락"))
            {
                n = -1;
                //relationAnim.SetTrigger("down");
                skeleton.SetSkin("Friend");
                //skelAnim.Skeleton.SetSkin("Friend-0");
                //skelAnim.Skeleton.SetSkin(friendDown);
                skelAnim.initialSkinName = "Friend";
                //skelAnim.startingAnimation = "Down";
                skelAnim.AnimationState.SetAnimation(0, "Down1", true);
                //skelAnim.AnimationState.SetAnimation(0, "down", true);
                audiosource.PlayOneShot(rel_updown[1]);
            }

            skeleton.SetSlotsToSetupPose();
            skelAnim.Skeleton.SetSlotsToSetupPose();
            skelAnim.Update(0);
            skelAnim.AnimationState.Apply(skeleton);
            //skelAnim.AnimationState.Apply(skelAnim.Skeleton);

            page++;

            for (int i = 0; i < cas.Length; i++)
            {
                ChangeRelation(cas[i], n);

            }

        }
        else if (cmd.Equals("멘탈"))
        {
            skelAnim.gameObject.SetActive(true);
            canAutoSkip = true;
            string mental = sd.sheetData[page].Dialog;
            string[] split = mental.Split(":");
            string[] cas = split[0].Split(",");
            int n = 0;
            if (split[1].Contains("상승"))
            {
                n = 1;
                skelAnim.Skeleton.SetSkin("Mental");
                skelAnim.initialSkinName = "Mental";
                //skelAnim.startingAnimation = "Up";
                // skelAnim.Skeleton.SetSkin(mentalUp);
                skelAnim.AnimationState.SetAnimation(0, "Up1", true);
                //skelAnim.AnimationState.SetAnimation(0, "up", true);
                //mentalAnim.SetTrigger("up");
                audiosource.PlayOneShot(men_updown[0]);
            }
            //Friend/down/Friend-0
            //Mental / up / Mental0

            if (split[1].Contains("하락"))
            {
                n = -1;
                skelAnim.Skeleton.SetSkin("Mental");
                skelAnim.initialSkinName = "Mental";
                //skelAnim.startingAnimation = "Down";
                //skelAnim.Skeleton.SetSkin(mentalDown);
                skelAnim.AnimationState.SetAnimation(0, "Down1", true);
                //skelAnim.AnimationState.SetAnimation(0, "down", true);
                //mentalAnim.SetTrigger("down");
                audiosource.PlayOneShot(men_updown[1]);
            }
            skelAnim.Skeleton.SetSlotsToSetupPose();
            skelAnim.Update(0);

            skelAnim.AnimationState.Apply(skelAnim.Skeleton);
            page++;

            for (int i = 0; i < cas.Length; i++)
            {
                ChangeMental(cas[i], n);

            }

        }
        else if (cmd.Equals("정보"))
        {
            infoSystem.ShowInfo(sd.sheetData[page].Dialog);
            showDialogue = false;
        }
        else if (cmd.Equals("시스템"))
        {
            canAutoSkip = false;
            string CMD = sd.sheetData[page].CMD;

            if (CMD.Contains("조사_"))
            {

                string[] str = CMD.Split("_");

                if (inspectObj == null)
                {
                    inspectObj = Instantiate(Resources.Load<GameObject>(instanceFilePath + str[1]), inspectTrans);

                }
                else if (inspectObj.activeInHierarchy == false) { inspectObj.SetActive(true); }

                ExitDialogue();
            }
            else if (CMD.Contains("Ins_"))
            {
                string[] str = CMD.Split("_");

                Instantiate(Resources.Load<GameObject>(instanceFilePath + str[1]));

                ExitDialogue();
            }
            else if (CMD.Contains("소개_"))
            {
                string[] str = CMD.Split("_");
                string info = sd.sheetData[page].Dialog;
                page++;
                string content = sd.sheetData[page].Dialog;
                ci.gameObject.SetActive(true);
                ci.SetIntroduce(str[1], info, content);
                Debug.Log(str[1] + info + content);
            }
        }
        else if (cmd.Equals("챕터끝"))
        {
            chapterEnd.SetActive(true);
            ExitDialogue();
        }
        else
        {
            canAutoSkip = true;
            if (ci.gameObject.activeInHierarchy) ci.gameObject.SetActive(false);
        }

    }

    GameObject inspectObj;
    string instanceFilePath = "instance/";


    public void PagePlusOne() {
        txtDialog.enabled = true;
        txtName.enabled = true;
        canGoNext = true;
        //ShowDialog();
    }

    [Header("Guide")]
    public GameObject guideWhole;
    public TMP_Text guideTitle;
    public TMP_Text guideContent;

    string guideFilePath = "guide/";


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
        dialogWholeObj.SetActive(false);
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



    void BackgroundAnimation() {
    
    }


    public void Movebranch(int branch) {
        this.branch = branch;
        if (!dialogWholeObj.activeInHierarchy) dialogWholeObj.SetActive(true);
        PageSettingOnBranch();
        canGoNext = true;
        NextDialog();
    }

    public void MovebranchNext(int branch) {
        this.branch = branch;
        PageSettingOnBranch();

        NextDialog();
    }

    private void Update() {
        if (canGoNext && (!isSkip || !canAutoSkip)) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                ShowDialog();
            }
        }

    }
    /*
    //자동스킵 기능
    IEnumerator SkipItSelf() {
        WaitForSeconds wait = new(1.5f);
        while (isSkip) {
            if (canGoNext && tc.isFinished) {

                ShowDialog();
                yield return wait;
            }
            //new WaitUntil(() => tc.DoTextChanged && tc.hasTextChanged);
        }
    }*/

    

}
