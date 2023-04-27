using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationSlider : MonoBehaviour
{
    /*���赵 �ý��� > �������� �߿��ϰ� �ۿ��ϴ°Ű�
    ���赵�� [�ſ� ������] > [������] > [����] > [��ȣ��] > [�ſ� ��ȣ��] �ܰ�� ������ �غð�
    ��ġ�� �Ϲ� �÷��� ȭ�鿡�� ������ �ʰ�, �����ι��� ��ȭ�� �ϴٰ�
    ���赵�� ������ �ִ� �������� ����, ������ �ϰ� �Ǹ� ���� ��, ��ȭ�� �̾���������
    ���赵�� �ö󰬴ٰ� �ϸ� �ö󰬴ٰ� ǥ��? �������ٰ� �ϸ� �������ٰ� 
    ��� ǥ�ð� �Ǿ ������ �ɷ� �����߾��.
    �׷��ٰ� é�Ͱ� ���������� ���� é�ͷ� �Ѿ�� ���� ���ݱ��� ����� ���赵�� ��� ��Ż�� ���
    �߰����ó�� �Ҷ�� �����ִ� ������ �ϴ� �� ��� �������Դϴ�
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
        if (relation >= 18) {       //�ſ� ��ȣ��
            imgFill.color = colFrd2;
        }
        else if (relation >= 13) {  //��ȣ��
            imgFill.color = colFrd1;
        }
        else if (relation >= 8) {   //����
            imgFill.color = colNut1;
        }
        else if (relation >= 3) {   //������
            imgFill.color = colHos1;
        }
        else {                      //�ſ� ������
            imgFill.color = colHos2;
        }
    }

}
