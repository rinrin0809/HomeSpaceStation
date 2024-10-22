using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMove : MonoBehaviour
{
    //���̈ʒu
    [SerializeField] Vector2[] Pos;
    //���őI���ł��鐔�̍ő�l
    [SerializeField] int MaxPosNum;
    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 63.0f;
    //�ړ����̃C���^�[�o���̎���
    float Time = 0.0f;
    //���ړ����̔z��̔ԍ�
    private int SideNum;
    //���ړ����̔z��̏���ԍ�
    [SerializeField] private int MaxSideNum;
    //���ړ����̔z��̔ԍ��ݒ�
    public void SetSideNum(int Num)
    {
        SideNum = Num;
    }
    //���ړ����̔z��̔ԍ��擾
    public int GetSideNum()
    {
        return SideNum;
    }

    //�c�ړ����̔z��̔ԍ�
    private int LengthNum;
    //�c�ړ����̔z��̏���ԍ�
    [SerializeField] private int MaxLengthNum;
    //�c�ړ����̔z��̔ԍ��ݒ�
    public void SetLengthNum(int Num)
    {
        LengthNum = Num;
    }
    //�c�ړ����̔z��̔ԍ��擾
    public int GetLengthNum()
    {
        return LengthNum;
    }
    //�ړ�����
    public Vector3 Move(bool SideFlg, Vector3 Position)
    {
        //�V�����ʒu
        Vector3 NewPos;

        // ���ړ��̏ꍇ
        if (SideFlg)
        {
            Vector2 SidePos = Pos[SideNum];
            // ���ړ�����
            SideMove();

            // RectTransform�̃|�W�V�������擾
            NewPos = Position;

            // X, Y���W���X�V
            NewPos.x = SidePos.x;
            NewPos.y = SidePos.y;
        }

        else
        {
            Vector2 LengthPos = Pos[LengthNum];

            // �c�ړ�����
            LengthMove();

            // RectTransform�̃|�W�V�������擾
            NewPos = Position;

            // X, Y���W���X�V
            NewPos.x = LengthPos.x;
            NewPos.y = LengthPos.y;
        }
        // �V�����|�W�V������Ԃ�
        return NewPos;
    }

    //���ړ�
    private void SideMove()
    {
        if (SideNum > 0)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    Time = MAX_TIME;
                    SideNum--;
                    //Debug.Log("SideNum" + SideNum);
                }
            }
        }

        if (SideNum < MaxSideNum)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    SideNum++;
                    //Debug.Log("SideNum" + SideNum);
                }
            }
        }
    }

    //�c�ړ�
    private void LengthMove()
    {
        Time--;

        if (LengthNum > 0)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    Time = MAX_TIME;
                    LengthNum--;
                    //Debug.Log("LengthNum" + LengthNum);
                }
            }
        }

        if (LengthNum < MaxLengthNum)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    Time = MAX_TIME;
                    LengthNum++;
                    //Debug.Log("LengthNum" + LengthNum);
                }
            }
        }
    }
}
