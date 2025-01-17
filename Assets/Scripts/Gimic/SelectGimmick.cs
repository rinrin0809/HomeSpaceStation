using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SelectGimmick : MonoBehaviour
{
    public GameObject Book;

    [SerializeField]
    private List<GameObject> BookList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Bookshelf = new List<GameObject>();
    [SerializeField]
    private List<GameObject> testList = new List<GameObject>();

    private List<GameObject> testBookList = new List<GameObject>();

    private List<bool> selectedFlag = new List<bool>();
    [SerializeField]
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

    public bool Ans = false;

    float alpha = 0.3f;

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
                            //if(shelfImage.color.a==0.3f && shelfImage ==null )
                            if (shelfImage != null && shelfImage.sprite == null) // ���Image��T��
                            {
                                shelfImage.color = Color.white;
                                // sprite��ݒ�
                                targetshlf.SetSprite(shelf, exportNum.sprite);
                                inputValue += number.ToString();


                                if (selectedImage != null)
                                {
                                    selectedImage.enabled = false;
                                }
                                // �I����Ԃ��X�V
                                selectedFlag[Num] = true;

                                testList.Add(BookList[Num]);
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
            if (testList.Count > 0)
            {
                int lastIndex = testList.Count - 1; // testList�̍Ō�̃C���f�b�N�X
                GameObject book = testList[lastIndex]; // �Ō�̃I�u�W�F�N�g���擾
                Image bookImage = Bookshelf[lastIndex].GetComponent<Image>();
                
                // BookList�̒��œ������O�̃I�u�W�F�N�g��T��
                for (int j = 0; j < BookList.Count; j++)
                {
                    if (book.name == BookList[j].name) // ���O����v���邩�m�F
                    {
                        Image selectedImage = BookList[j].GetComponent<Image>();
                        if (selectedImage != null)
                        {
                            selectedImage.enabled = true; // Image��L���ɂ���
                            bookImage.sprite = null;
                            selectedFlag[j] = false;
                            bookImage.color = Color.gray;
                            Debug.Log("true�ɂȂ���: " + selectedImage.name);
                        }
                        break; // ��v����I�u�W�F�N�g�����������烋�[�v�𔲂���
                    }
                }

                testList.RemoveAt(lastIndex); // testList����Ō�̃I�u�W�F�N�g���폜
            }
        }


        //if (inputValue.Length < ResultNum && Input.GetKeyDown(KeyCode.Space))
        //{
        //    //inputValue = "";
           
        //    Debug.Log("�s����");
        //}
        if (inputValue.Length == ResultNum && Input.GetKeyDown(KeyCode.Space)) // Space�Ŕ���
        {

            if (inputValue == answer.ToString())
            {
                Ans = true;
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
                Book.gameObject.SetActive(false);
                testList.Clear();
            }
            else
            {
                Debug.Log("�s����");
                Ans = false;
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
                for (int i = 0; i < 4; i++)
                {
                    selectedFlag[i] = false; // ���ׂĖ��I����ԂɃ��Z�b�g
                    
                }
                
                testList.Clear();
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
                float alpha1 = 0.3f;
                Color colorwhite = itemImage.color;
                colorwhite = Color.white;
                colorwhite.a = Mathf.Clamp01(alpha1);
                float alpha2 = 1f;
                Color colorblack = itemImage.color;
                colorblack = Color.white;
                colorblack.a = Mathf.Clamp01(alpha2);
                itemImage.color = (i == Num) ? colorwhite : colorblack;
              

            }
        }
    }


}