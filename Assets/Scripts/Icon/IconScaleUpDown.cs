using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScaleUpDown : MonoBehaviour
{
    //�A�C�R���̃X�P�[��
    private Vector2 ScaleXY;

    public Vector2 GetScaleXY()
    {
        return ScaleXY;
    }

    //�A�C�R���X�P�[���̍ŏ��l
    private Vector2 MIN_SCALE_XY = new Vector2(0.9f, 0.9f);
    //�A�C�R���X�P�[���̍ő�l
    private Vector2 MAX_SCALE_XY = new Vector2(1.25f, 1.25f);
    //���ʂ̑傫��
    private Vector2 DEFAULT_SCALE_XY = new Vector2(1.0f, 1.0f);
    //�������]�p�̃t���O
    private bool UpDownFlg = false;
    //�ω����x
    private float ScaleSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ScaleXY = new Vector2(1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //�t���O�𔽓]������
        InversionFlg();
    }

    //�X�P�[����ω�������
    public void ScaleRaise(Button[] Buttons, int Num)
    {
        if (Num < 0 || Num >= Buttons.Length) return; // �͈͊O�`�F�b�N

        if (!UpDownFlg)
        {
            ScaleXY.x += ScaleSpeed * Time.deltaTime;
            ScaleXY.y += ScaleSpeed * Time.deltaTime;
        }
        else
        {
            ScaleXY.x -= ScaleSpeed * Time.deltaTime;
            ScaleXY.y -= ScaleSpeed * Time.deltaTime;
        }

        for (int i = 0; i < Buttons.Length; i++)
        {
            RectTransform rectTransform = Buttons[i].gameObject.GetComponent<RectTransform>();
            rectTransform.localScale = (i == Num) ? ScaleXY : DEFAULT_SCALE_XY;
        }
    }

    //�t���O�𔽓]������
    public void InversionFlg()
    {
        if(ScaleXY.x >= MAX_SCALE_XY.x || ScaleXY.y >= MAX_SCALE_XY.y)
        {
            UpDownFlg = true;
        }

        else if(ScaleXY.x <= MIN_SCALE_XY.x || ScaleXY.y <= MIN_SCALE_XY.y)
        {
            UpDownFlg = false;
        }
    }
}
