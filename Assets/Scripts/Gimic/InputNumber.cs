using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputNumber : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> inputNum = new List<GameObject>();

    
    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 50.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    int columns = 3; // ��̐�

    private ExportNumber expNum;


    [SerializeField]
    GameObject numberBox;
    string inputValue = "";
    string inputValueWord = "";
    [SerializeField]
    TextMeshProUGUI numberText;
    [SerializeField]
    TextMeshProUGUI resultText;
    [SerializeField] int anserwer = 1234;
    [SerializeField] string anserwerword = "abcd";

    [SerializeField]bool ChangeMode = false;

    void Start()
    {
        inputNum.Clear();

        numberText.text = "";
        resultText.text = "";

        foreach(Transform childin in transform)
        {
            inputNum.Add(childin.gameObject);
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
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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
                    Num += inputNum.Count;
                }
            }
        }

        // ���L�[�iA�j�őO�̗�Ɉړ�
        else if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
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
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
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
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            string selectedObjectName = inputNum[Num].name;
            ExportNumber exportNum = inputNum[Num].GetComponent<ExportNumber>();
          
            if (exportNum != null)
            {
                int number = exportNum.ExpNum; // �I�u�W�F�N�g�̒l���擾
                string word = exportNum.Exword;

                if (inputValue.Length < 4  && number > 0)
                {
                    if (ChangeMode == false)
                    {
                        inputValue += number.ToString();
                    }
                    else if (ChangeMode == true)
                    {
                        inputValue += word;
                    }
                     numberText.text = inputValue; // ���͂��ꂽ������\��
                }
             
                else
                {
                    Debug.Log("���ł�4�����͂���Ă��܂��B");
                }
            }
        }

        // Backspace�Ń��Z�b�g
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (inputValue.Length > 0)
            {
                inputValue = inputValue.Substring(0, inputValue.Length - 1); // �Ō�̕������폜
                numberText.text = inputValue;                               // �\�����X�V
            }
        }

        if (inputValue.Length == 4 && Input.GetKeyDown(KeyCode.Space)) // Space�Ŕ���
        {
            Debug.Log("anserwer (as string): " + anserwerword.ToString());
            //if (int.Parse(inputValue) == anserwer)
            if (ChangeMode == false)
            {
                if (inputValue == anserwer.ToString())
                {
                    resultText.text = "�����I";
                    Debug.Log("�����I");
                }
                else
                {
                    resultText.text = "�s����";
                    Debug.Log("�s����");
                }
            }
            else if (ChangeMode == true)
            {
                if (inputValue == anserwerword.ToString())
                {
                    resultText.text = "�����I";
                    Debug.Log("�����I");
                }
                else
                {
                    resultText.text = "�s����";
                    Debug.Log("�s����");
                }
            }
            // ���͒l�����Z�b�g
            inputValue = "";
           
        }
        // �A�C�e���̐F���X�V
        UpdateItemColors();

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
