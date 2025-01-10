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

    //移動時のインターバルの時間上限
    const float MAX_TIME = 50.0f;
    //移動時のインターバルの時間
    float time = MAX_TIME;

    int columns = 3; // 列の数

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
        // 一時停止状態
        Time.timeScale = 0.0f;
        time--;

        // 下キー（S）で次の行に移動
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // 次の行に移動
                Num += columns;
                if (Num >= inputPanel.Count)
                {
                    // 最後の行を超えたら最初の行に戻る
                    Num %= inputPanel.Count;
                }
            }
        }
        // 上キー（W）で前の行に移動
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // 前の行に移動
                Num -= columns;
                if (Num < 0)
                {
                    // 最初の行を超えたら最後の行に戻る
                    Num += inputPanel.Count;
                }
            }
        }

        // 左キー（A）で前の列に移動
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // 最初の要素を超えたら最後の要素に戻る
                    Num = inputPanel.Count - 1;
                }
            }
        }
        // 右キー（D）で次の列に移動
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num += 1;
                if (Num >= inputPanel.Count)
                {
                    // 最後の要素を超えたら最初の要素に戻る
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

      
        // アイテムの色を更新
        UpdateItemColors();

    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < inputPanel.Count; i++)
        {
            UnityEngine.UI.Image itemImage = inputPanel[i].inputpanel.GetComponent<UnityEngine.UI.Image>();

            if (itemImage != null)
            {
                // Num番目のアイテムは緑、それ以外は白に設定
                itemImage.color = (i == Num) ? Color.red : Color.white;
            }
        }
    }



}

[System.Serializable]
public struct inputPanelList 
{
    public GameObject inputpanel;

    public bool IsCorrectFlag; // 正解の時にオンにするフラグ

    public void FlagIsTrue()
    {
        
        IsCorrectFlag = true;
        if(IsCorrectFlag == true)
        {
            Debug.Log("正解の可能性あり");
        }
    }

    public void FlagIsFlase()
    {
       
        IsCorrectFlag = false;
        if(IsCorrectFlag == false)
        {
            Debug.Log("不正解の可能性あり");
        }
    }
}
