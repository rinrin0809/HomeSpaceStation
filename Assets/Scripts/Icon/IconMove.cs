using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconMove : MonoBehaviour
{
    //矢印の位置
    [SerializeField] GameObject[] Obj;
    //矢印で選択できる数の最大値
    [SerializeField] int MaxPosNum;
    //移動時のインターバルの時間上限
    const float MAX_TIME = 7.0f;
    //移動時のインターバルの時間
    float Time = 0.0f;
    //X座標の補正値
    private float CorX = 0.0f;
    //横移動時の配列の番号
    private int SideNum;
    //横移動時の配列の上限番号
    [SerializeField] private int MaxSideNum;

    //横移動時の配列の番号設定
    public void SetSideNum(int Num)
    {
        SideNum = Num;
    }
    //横移動時の配列の番号取得
    public int GetSideNum()
    {
        return SideNum;
    }

    //縦移動時の配列の番号
    private int LengthNum;
    //縦移動時の配列の上限番号
    [SerializeField] private int MaxLengthNum;
    //縦移動時の配列の番号設定
    public void SetLengthNum(int Num)
    {
        LengthNum = Num;
    }
    //縦移動時の配列の番号取得
    public int GetLengthNum()
    {
        return LengthNum;
    }
    //移動処理
    public Vector3 Move(bool SideFlg, Vector3 Position)
    {
        //新しい位置
        Vector3 NewPos;

        // 横移動の場合
        if (SideFlg)
        {
            Vector3 SidePos = Obj[SideNum].GetComponent<Transform>().position;
            // 横移動処理
            SideMove();

            //横移動した時の選択されている番号
            if(LoadManager.Instance != null)LoadManager.Instance.SetSideNum(SideNum);

            // RectTransformのポジションを取得
            NewPos = Position;

            // X, Y座標を更新
            NewPos.x = SidePos.x - CorX;
            NewPos.y = SidePos.y;
        }

        else
        {
            Vector3 LengthPos = Obj[LengthNum].GetComponent<Transform>().position;

            // 縦移動処理
            LengthMove();

            //縦移動した時の選択されている番号
            if (LoadManager.Instance != null) LoadManager.Instance.SetLengthNum(LengthNum);

            // RectTransformのポジションを取得
            NewPos = Position;

            // X, Y座標を更新
            NewPos.x = LengthPos.x - CorX;
            NewPos.y = LengthPos.y;
        }
        // 新しいポジションを返す
        return NewPos;
    }

    //横移動
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

    //縦移動
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

        // LengthNum が 0 の場合に最大値を設定
        if (LengthNum == 0)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    Time = MAX_TIME;
                    LengthNum = MaxLengthNum;
                    // Debug.Log("LengthNum set to MaxLengthNum: " + LengthNum);
                }
            }
        }

        // LengthNum が MaxLengthNum の場合に 0 を設定
        if (LengthNum == MaxLengthNum)
        {
            if (Time < 0.0f)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    Time = MAX_TIME;
                    LengthNum = 0;
                    // Debug.Log("LengthNum set to 0");
                }
            }
        }
    }

    public void ResetNum()
    {
        LengthNum = 0;
        SideNum = 0;
    }
}
