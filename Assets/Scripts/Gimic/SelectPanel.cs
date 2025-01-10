using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{

    [SerializeField]
    //private List<GameObject> inputPanel = new List<GameObject>();
    private List<inputPanelList> inputPanel;

    [SerializeField]
    private bool flag = false;

    Transform panelPos;

    Vector3 ChangePanelPos;

    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 50.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    int columns = 3; // ��̐�

    private ExportNumber expNum;

    [SerializeField]
    private float panelrotate =0;

    private float answerPos = 90;

    [SerializeField]
    private List<bool> AnswerFlag;




    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        MoveNum();
    }

    private void MoveNum()
    {
        // �ꎞ��~���
        Time.timeScale = 0.0f;
        time--;

        // ���L�[�iS�j�Ŏ��̍s�Ɉړ�
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // ���̍s�Ɉړ�
                Num += columns;
                if (Num >= inputPanel.Count)
                {
                    // �Ō�̍s�𒴂�����ŏ��̍s�ɖ߂�
                    Num %= inputPanel.Count;
                }
            }
        }
        // ��L�[�iW�j�őO�̍s�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // �O�̍s�Ɉړ�
                Num -= columns;
                if (Num < 0)
                {
                    // �ŏ��̍s�𒴂�����Ō�̍s�ɖ߂�
                    Num += inputPanel.Count;
                }
            }
        }

        // ���L�[�iA�j�őO�̗�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // �ŏ��̗v�f�𒴂�����Ō�̗v�f�ɖ߂�
                    Num = inputPanel.Count - 1;
                }
            }
        }
        // �E�L�[�iD�j�Ŏ��̗�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num += 1;
                if (Num >= inputPanel.Count)
                {
                    // �Ō�̗v�f�𒴂�����ŏ��̗v�f�ɖ߂�
                    Num = 0;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            string selectedObjectName = inputPanel[Num].inputpanel.name;
            ExportNumber exportNum = inputPanel[Num].inputpanel.GetComponent<ExportNumber>();
            

            exportNum.ChangePos(panelrotate);
            //Debug.Log("pos:" + exportNum.transform.rotation.z);
            //if (exportNum.angleZ == 360) 
            //{
               
            //}   
            if(exportNum.angleZ == exportNum.CorrectAnswer)
            {
                inputPanel[Num].FlagIsTrue();
                flag = true;
                AnswerFlag[Num] = true;
            }
            else if (exportNum.angleZ == exportNum.a)
            {
                inputPanel[Num].FlagIsTrue();
                flag = true;
                AnswerFlag[Num] = true;
                Debug.Log("a");
            }
            else
            {
                inputPanel[Num].FlagIsFlase();
                flag = false;
                AnswerFlag[Num] = false;
            }

        }

      
        // �A�C�e���̐F���X�V
        UpdateItemColors();

    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < inputPanel.Count; i++)
        {
            UnityEngine.UI.Image itemImage = inputPanel[i].inputpanel.GetComponent<UnityEngine.UI.Image>();

            if (itemImage != null)
            {
                // Num�Ԗڂ̃A�C�e���͗΁A����ȊO�͔��ɐݒ�
                itemImage.color = (i == Num) ? Color.red : Color.white;
            }
        }
    }



}

[System.Serializable]
public struct inputPanelList 
{
    public GameObject inputpanel;

    public bool IsCorrectFlag; // �����̎��ɃI���ɂ���t���O

    public void FlagIsTrue()
    {
        
        IsCorrectFlag = true;
        if(IsCorrectFlag == true)
        {
            Debug.Log("�����̉\������");
        }
    }

    public void FlagIsFlase()
    {
       
        IsCorrectFlag = false;
        if(IsCorrectFlag == false)
        {
            Debug.Log("�s�����̉\������");
        }
    }
}
