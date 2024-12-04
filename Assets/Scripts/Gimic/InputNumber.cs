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

    //移動時のインターバルの時間上限
    const float MAX_TIME = 50.0f;
    //移動時のインターバルの時間
    float time = MAX_TIME;

    int columns = 3; // 列の数

    // Start is called before the first frame update
    void Start()
    {
        inputNum.Clear();

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

        // Backspaceでリセット
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Num = 0;
        }

        // アイテムの色を更新
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
                // Num番目のアイテムは緑、それ以外は白に設定
                itemImage.color = (i == Num) ? Color.green : Color.white;
            }
        }
    }


}
