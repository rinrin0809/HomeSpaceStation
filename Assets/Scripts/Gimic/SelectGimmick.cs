using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SelectGimmick : MonoBehaviour
{
    //public GameObject Book;

    [SerializeField]
    private List<GameObject> BookList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Bookshelf = new List<GameObject>();
    [SerializeField]
    private List<GameObject> testList = new List<GameObject>();

    private List<GameObject> testBookList = new List<GameObject>();

    private List<bool> selectedFlag = new List<bool>();
    string inputValue = "";

    [SerializeField]
    int answer = 1234;
    // ���͕�����
    [SerializeField]
    private int ResultNum = 4;

    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 50.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    int NumCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // BookList�Ɠ����T�C�Y�̑I����ԃ��X�g��������
        for (int i = 0; i < BookList.Count; i++)
        {
            selectedFlag.Add(false); // �S�Ė��I�����
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
        Time.timeScale = 0.0f;
        time--;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // ���̍s�ɍs��
                if (Num >= BookList.Count)
                {
                    Num %= BookList.Count;
                }
            }
        }

        // ��Ɉړ�
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                if (Num < 0)
                {
                    Num += BookList.Count;
                }
            }
        }

        // ���Ɉړ�
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // �ŏ��̗v�f�𒴂�����Ō�̗v�f�ɖ߂�
                    Num = BookList.Count - 1;
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
                if (Num >= BookList.Count)
                {
                    // �Ō�̗v�f�𒴂�����ŏ��̗v�f�ɖ߂�
                    Num = 0;
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ExportNumber exportNum = BookList[Num].GetComponent<ExportNumber>();
            Image selectedImage = BookList[Num].GetComponent<Image>();

            if (exportNum != null)
            {
                int number = exportNum.ExpNum; // �I�u�W�F�N�g�̒l���擾

                if (inputValue.Length > -1)
                {
                    foreach (GameObject shelf in Bookshelf)
                    {
                        ExportNumber targetshlf = Bookshelf[Num].GetComponent<ExportNumber>();
                        if (targetshlf != null)
                        {
                            // �I���ς݂��ǂ������`�F�b�N
                            if (selectedFlag[Num])
                            {
                                break; // �����𒆒f
                            }

                            Image shelfImage = shelf.GetComponent<Image>();

                            if (shelfImage != null && shelfImage.sprite == null) // ���Image��T��
                            {

                                // sprite��ݒ�
                                targetshlf.SetSprite(shelf, exportNum.sprite);

                                inputValue += number.ToString();


                                if (selectedImage != null)
                                {
                                    selectedImage.enabled = false;
                                }
                                // �I����Ԃ��X�V
                                selectedFlag[Num] = true;

                                testList.Add(shelf);
                                testBookList.Add(shelf);


                                break; // 1�ݒ肵���烋�[�v�𔲂���
                            }

                        }
                    }
                }
            }


        }

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Bookshelf�̑ΏۃX���b�g���t���Ɍ���
            for (int i = testList.Count - 1; i >= 0; i--)
            {
                GameObject book = testList[i];
                Image bookImage = Bookshelf[i].GetComponent<Image>();
                Image selectedImage = BookList[Num].GetComponent<Image>();

                if (bookImage != null) // null�łȂ��X���b�g����������
                {
                    Debug.Log("name:" + bookImage.name);
                    bookImage.sprite = null;
                    // targetShelf.SetRemoved(targetShelf.image); // �X���b�g��null�ɂ���
                    testList.RemoveAt(i); // �C���f�b�N�X���w�肵�č폜

                    if (bookImage.name == testBookList[Num].name)
                    {
                        selectedImage.enabled = true;
                        Debug.Log("true�ɂȂ���");
                    }
                    selectedFlag[i] = false;
                    break; // �ŏ��Ɍ������X���b�g���폜�����烋�[�v�𔲂���
                }

            }

        }

        if (inputValue.Length < ResultNum && Input.GetKeyDown(KeyCode.Space))
        {
            inputValue = "";

            Debug.Log("�s����");
        }
        if (inputValue.Length == ResultNum && Input.GetKeyDown(KeyCode.Space)) // Space�Ŕ���
        {

            if (inputValue == answer.ToString())
            {
                Debug.Log("�����I");
                inputValue = "";
                foreach (GameObject book in BookList)
                {
                    Image bookImage = book.GetComponent<Image>();
                    if (bookImage != null && !bookImage.enabled) // ����������Ă���ꍇ�̂ݏ���
                    {
                        bookImage.enabled = true; // Image��L����
                    }
                }
                foreach (GameObject shelf in Bookshelf)
                {
                    Image shelfImage = shelf.GetComponent<Image>();
                    if (shelfImage != null)
                    {
                        shelfImage.sprite = null; // sprite���폜
                    }
                }
                // �I���t���O�����Z�b�g
                for (int i = 0; i < selectedFlag.Count; i++)
                {
                    selectedFlag[i] = false; // ���ׂĖ��I����ԂɃ��Z�b�g
                }
            }
            else
            {
                Debug.Log("�s����");
                inputValue = "";
                foreach (GameObject book in BookList)
                {
                    Image bookImage = book.GetComponent<Image>();
                    if (bookImage != null && !bookImage.enabled) // ����������Ă���ꍇ�̂ݏ���
                    {
                        bookImage.enabled = true; // Image��L����
                    }
                }
                foreach (GameObject shelf in Bookshelf)
                {
                    Image shelfImage = shelf.GetComponent<Image>();
                    if (shelfImage != null)
                    {
                        shelfImage.sprite = null; // sprite���폜
                    }
                }
                // �I���t���O�����Z�b�g
                for (int i = 0; i < selectedFlag.Count; i++)
                {
                    selectedFlag[i] = false; // ���ׂĖ��I����ԂɃ��Z�b�g
                }
            }
        }
        UpdateItemColors();
    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            Image itemImage = BookList[i].GetComponent<Image>();

            if (itemImage != null)
            {
                float alpha = 0.3f;
                Color colorwhite = itemImage.color;
                colorwhite = Color.white;
                colorwhite.a = Mathf.Clamp01(alpha);
                float alpha2 = 1f;
                Color colorblack = itemImage.color;
                colorblack = Color.white;
                colorblack.a = Mathf.Clamp01(alpha2);
                // Num�Ԗڂ̃A�C�e���͗΁A����ȊO�͔��ɐݒ�
                //itemImage.color = (i == Num) ? Color.black : Color.white;
                itemImage.color = (i == Num) ? colorwhite : colorblack;

            }
        }
    }


}
