using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationSlider : MonoBehaviour
{
    /*관계도 시스템 > 엔딩에서 중요하게 작용하는거고
    관계도는 [매우 적대적] > [적대적] > [보통] > [우호적] > [매우 우호적] 단계로 생각을 해봤고
    배치는 일반 플레이 화면에는 보이지 않고, 등장인물과 대화를 하다가
    관계도에 영향을 주는 선택지에 도달, 선택을 하게 되면 선택 후, 대화가 이어지기전에
    관계도가 올라갔다고 하면 올라갔다고 표시? 내려갔다고 하면 내려갔다고 
    잠깐 표시가 되어서 나오는 걸로 생각했어요.
    그러다가 챕터가 끝날때마다 다음 챕터로 넘어가기 전에 지금까지 결과로 관계도는 어떻고 멘탈은 어떤지
    중간결과처럼 쫘라락 보여주는 식으로 하는 건 어떨까 생각중입니다
    3 5 5 5 3 */
    
    public int tem_CharacterNum = 1;
    public int tem_Relation = 10;

    int relation=0;

    Slider slider;
    Image imgFill;

    private Color32 colHos2 = new(192, 50, 50, 255);
    private Color32 colHos1 = new(190, 112, 112, 255);
    private Color32 colNut1 = new(209, 209, 209, 255);
    private Color32 colFrd1 = new(155, 193, 212, 255);
    private Color32 colFrd2 = new(94, 168, 241, 255);


    private void Start() {
        slider = transform.GetChild(0).GetComponent<Slider>();
        imgFill = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
    }

    private void OnEnable() {
        if (slider == null) slider = transform.GetChild(0).GetComponent<Slider>();
        if (imgFill == null) imgFill = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();

        relation = tem_Relation;
        slider.value = relation;
        SetColor();
    }

    public void SetColor() {
        if (relation >= 18) {       //매우 우호적
            imgFill.color = colFrd2;
        }
        else if (relation >= 13) {  //우호적
            imgFill.color = colFrd1;
        }
        else if (relation >= 8) {   //보통
            imgFill.color = colNut1;
        }
        else if (relation >= 3) {   //적대적
            imgFill.color = colHos1;
        }
        else {                      //매우 적대적
            imgFill.color = colHos2;
        }
    }

}
