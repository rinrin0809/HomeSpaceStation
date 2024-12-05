using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputNumber : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> inputNum = new List<GameObject>();

    
    private int Num = 0;

    //移動時のインターバルの時間上限
    const float MAX_TIME = 50.0f;
    //移動時のインターバルの時間
    float time = MAX_TIME;

    int columns = 3; // 列の数

    private ExportNumber expNum;

    private int selectNumber;

    [SerializeField]
    GameObject numberBox;
    string inputValue = "";
    [SerializeField]
    TextMeshProUGUI numberText;
    [SerializeField]
    TextMeshProUGUI resultText;
    [SerializeField] int answer = 1234;
    // Start is called before the first frame update
    void Start()
    {
        inputNum.Clear();

        numberText.text = "";
        resultText.text = "";
       
        foreach(Transform childin in transform)
        {
            inputNum.Add(childin.gameObject);
        }
        // リストの内容を確認（デバッグ用）
        foreach (var obj in inputNum)
        {
            Debug.Log("子オブジェクト: " + obj.name);
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
        // 一時停止状態
        Time.timeScale = 0.0f;
        time--;

        // 下キー（S）で次の行に移動
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // 次の行に移動
                Num += columns;
                if (Num >= inputNum.Count)
                {
                    // 最後の行を超えたら最初の行に戻る
                    Num %= inputNum.Count;
                }
            }
        }
        // 上キー（W）で前の行に移動
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // 前の行に移動
                Num -= columns;
                if (Num < 0)
                {
                    // 最初の行を超えたら最後の行に戻る
                    Num += inputNum.Count;
                }
            }
        }

        // 左キー（A）で前の列に移動
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // 最初の要素を超えたら最後の要素に戻る
                    Num = inputNum.Count - 1;
                }
            }
        }
        // 右キー（D）で次の列に移動
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num += 1;
                if (Num >= inputNum.Count)
                {
                    // 最後の要素を超えたら最初の要素に戻る
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
                
                int number = exportNum.ExpNum; // オブジェクトの値を取得
             
                if (inputValue.Length < 4)
                {
                    inputValue += number.ToString();
                    numberText.text = inputValue; // 入力された数字を表示
                }
                else
                {
                    Debug.Log("すでに4桁入力されています。");
                }
            }

           

        }

        // Backspaceでリセット
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (inputValue.Length > 0)
            {
                inputValue = inputValue.Substring(0, inputValue.Length - 1); // 最後の文字を削除
                numberText.text = inputValue;                               // 表示を更新
            }
        }

        if (inputValue.Length == 4 && Input.GetKeyDown(KeyCode.Space)) // Spaceで判定
        {
            if (int.Parse(inputValue) == answer)
            {
                resultText.text = "正解！";
                Debug.Log("正解！");
            }
            else
            {
                resultText.text = "不正解";
                Debug.Log("不正解");
            }

            // 入力値をリセット
            inputValue = "";
            numberText.text = inputValue;
        }


        // アイテムの色を更新
        UpdateItemColors();

       


        //numberBox.gameObject.SetActive(false);
    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < inputNum.Count; i++)
        {
            UnityEngine.UI.Image itemImage = inputNum[i].GetComponent<UnityEngine.UI.Image>();

            if (itemImage != null)
            {
                // Num番目のアイテムは緑、それ以外は白に設定
                itemImage.color = (i == Num) ? Color.green : Color.white;
            }
        }
    }


}
