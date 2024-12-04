using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputNumber : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> inputNum = new List<GameObject>();

    [SerializeField]
    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 50.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    int columns = 3; // ��̐�

    // Start is called before the first frame update
    void Start()
    {
        inputNum.Clear();

        foreach(Transform childin in transform)
        {
            inputNum.Add(childin.gameObject);
        }
        // ���X�g�̓��e���m�F�i�f�o�b�O�p�j
        foreach (var obj in inputNum)
        {
            Debug.Log("�q�I�u�W�F�N�g: " + obj.name);
        }
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // ���̍s�Ɉړ�
                Num += columns;
                if (Num >= inputNum.Count)
                {
                    // �Ō�̍s�𒴂�����ŏ��̍s�ɖ߂�
                    Num %= inputNum.Count;
                }
            }
        }
        // ��L�[�iW�j�őO�̍s�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // �O�̍s�Ɉړ�
                Num -= columns;
                if (Num < 0)
                {
                    // �ŏ��̍s�𒴂�����Ō�̍s�ɖ߂�
                    Num += inputNum.Count;
                }
            }
        }

        // ���L�[�iA�j�őO�̗�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // �ŏ��̗v�f�𒴂�����Ō�̗v�f�ɖ߂�
                    Num = inputNum.Count - 1;
                }
            }
        }
        // �E�L�[�iD�j�Ŏ��̗�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num += 1;
                if (Num >= inputNum.Count)
                {
                    // �Ō�̗v�f�𒴂�����ŏ��̗v�f�ɖ߂�
                    Num = 0;
                }
            }
        }

        // Backspace�Ń��Z�b�g
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Num = 0;
        }

        // �A�C�e���̐F���X�V
        UpdateItemColors();

        //Time.timeScale = 0.0f;
        //time--;

        //if (Num > 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        if (time < 0.0f)
        //        {
        //            time = MAX_TIME;
        //            Num -= 1;
        //        }
        //    }
        //}
        //else if (Num == 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        time = MAX_TIME;

        //        for(int i = 0; i < inputNum.Count; i++)
        //        {
        //            if (inputNum[i].name != null)
        //            {
        //                Num = 0;

        //            }
        //        }
        //    }
        //}

        //if (Num < inputNum.Count - 1)
        //{
        //    if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        if (time < 0.0f)
        //        {
        //            time = MAX_TIME;
        //            Num += 1;

        //            // Check if the selected item is null
        //            if (inputNum[Num] == null)
        //            {
        //                Num = 0; // Reset Num if the item at the current index is null
        //            }
        //        }
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    Num = 0;
        //}

        //else
        //{
        //    UpdateItemColors();
        //}

    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < inputNum.Count; i++)
        {
            UnityEngine.UI.Image itemImage = inputNum[i].GetComponent<UnityEngine.UI.Image>();

            if (itemImage != null)
            {
                // Num�Ԗڂ̃A�C�e���͗΁A����ȊO�͔��ɐݒ�
                itemImage.color = (i == Num) ? Color.green : Color.white;
            }
        }
    }


}
