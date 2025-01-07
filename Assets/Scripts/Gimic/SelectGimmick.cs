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

    string inputValue = "";

    [SerializeField]
    int answer = 1234;

    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 50.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    int NumCount = 0;

    // Start is called before the first frame update
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
            if (exportNum != null)
            {

                if (inputValue.Length > -1)
                {


                    foreach (GameObject shelf in Bookshelf)
                    {
                        ExportNumber targetshlf = Bookshelf[Num].GetComponent<ExportNumber>();
                        if (targetshlf != null)
                        {
                            Image shelfImage = shelf.GetComponent<Image>();

                            if (shelfImage != null && shelfImage.sprite == null) // ���Image��T��
                            {
                                Debug.Log("3 - ���Image����");

                                // sprite��ݒ�
                                targetshlf.SetSprite(shelf, exportNum.sprite);
                                Debug.Log("sprite��ݒ肵�܂���: " + shelf.name);

                                // BookList�̌��݂̑I���A�C�e����Image����ɂ���
                                Image selectedImage = BookList[Num].GetComponent<Image>();
                                if (selectedImage != null)
                                {
                                    float alpha = 0;
                                    Color color = selectedImage.color;
                                    color.a = Mathf.Clamp01(alpha);
                                    selectedImage.color = color;

                                    Debug.Log("BookList�̑I���I�u�W�F�N�g��Image����ɂ��܂���: " + BookList[Num].name);
                                }


                                break; // 1�ݒ肵���烋�[�v�𔲂���
                            }

                        }
                    }
                }
            }


        }

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Backspace��������܂���");
            // Bookshelf�̈�ԑ傫���C���f�b�N�X�����ł͂Ȃ��X���b�g��T��
            for (int i = Bookshelf.Count - 1; i >= 0; i--) // ��납��`�F�b�N
            {
                Image shelfImage = Bookshelf[i].GetComponent<Image>();

                if (shelfImage != null && shelfImage.sprite != null) // ��łȂ��X���b�g�𔭌�
                {
                    Debug.Log($"Bookshelf�̃X���b�g {i} �ɉ摜������A�폜���܂�");

                    // �摜���폜����
                    shelfImage.sprite = null;
                    Debug.Log($"Bookshelf�̃X���b�g {i} ����ɂ��܂���");

                    // BookList�̑Ή�����X���b�g�ɖ߂�
                    if (i < BookList.Count)
                    {
                        Image selectedImage = BookList[i].GetComponent<Image>();
                        if (selectedImage != null && selectedImage.sprite == null) // ������̏ꍇ�̂ݖ߂�
                        {
                            Debug.Log($"BookList�̃X���b�g {i} �ɉ摜��߂��܂�");

                            selectedImage.sprite = null; // �K�v�Ȃ�sprite��ݒ�
                            Color color = selectedImage.color;
                            color.a = 1.0f; // �s�����ɖ߂�
                            selectedImage.color = color;
                        }
                    }

                    break; // �ŏ��Ɍ������X���b�g�ŏ����I��
                }
                // Bookshelf�̑ΏۃX���b�g���擾
                //ExportNumber targetShelf = Bookshelf[Num].GetComponent<ExportNumber>();
                //if (targetShelf != null)
                //{
                //    Image shelfImage = Bookshelf[Num].GetComponent<Image>();

                //    if (shelfImage != null && shelfImage.sprite != null) // Bookshelf����łȂ��ꍇ
                //    {
                //        Debug.Log("Bookshelf�̃X���b�g�ɉ摜������A���ɖ߂��܂�");

                //        // BookList�̑I�𒆂̃X���b�g�ɖ߂�
                //        Image selectedImage = BookList[Num].GetComponent<Image>();
                //        if (selectedImage != null && selectedImage.sprite == null) // ������̏ꍇ�̂ݖ߂�
                //        {
                //            float alpha = 100;
                //            Color color = selectedImage.color;
                //            color.a = Mathf.Clamp01(alpha);
                //            selectedImage.color = color;
                //        }

                //        // Bookshelf�̃X���b�g����ɂ���
                //        shelfImage.sprite = null;
                //        Debug.Log("Bookshelf�̃X���b�g����ɂ��܂���: " + Bookshelf[Num].name);
                //    }
                //    else
                //    {
                //        Debug.Log("Bookshelf�̃X���b�g�����ɋ�ł��B�������܂���B");
                //    }
                //}
            }

            UpdateItemColors();
        }
    }
    private void UpdateItemColors()
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            Image itemImage = BookList[i].GetComponent<Image>();

            if (itemImage != null)
            {
                // Num�Ԗڂ̃A�C�e���͗΁A����ȊO�͔��ɐݒ�
                itemImage.color = (i == Num) ? Color.red : Color.white;
            }
        }
    }


}
