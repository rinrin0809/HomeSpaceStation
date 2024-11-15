using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventData : ScriptableObject
{
    // イベントのデータ配列
    [SerializeField]
    private Event[] Events;

    //配列数
    [SerializeField]
    public int Size = 8;

    //イベントの名前
    private string[] EventNames;

    public void Initialize()
    {
        Events = new Event[Size];
        SetEventNumber();
    }

    // 特定のイベントフラグを設定
    public void SetEventFlag(string Name, bool Flg)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EventFlag = Flg;
            }
        }
    }

    // 特定の名前のイベントフラグを取得
    public bool GetNameEventFlg(string Name)
    {
        for(int i = 0; i < Events.Length; i++)
        {
            if(Events[i].EventName == Name)
            {
                return Events[i].EventFlag;
            }
        }
        return false;
    }

    //イベントの番号設定
    public void SetEventNumber()
    {
        for (int i = 0; i < Size; i++)
        {
            Events[i].EventNumber = i;
        }
    }
}

// イベントの構造体
[System.Serializable]
public struct Event
{
    //イベントの名前（インスペクターに表示するだけ）
    [SerializeField]
    public string EventName;

    [SerializeField]
    //配列の番号
    public int EventNumber;

    //イベントのフラグ
    [SerializeField]
    public bool EventFlag;

    [SerializeField]
    //イベントが終了した時のフラグ
    public bool EndEventFlg;

    [SerializeField]
    //イベント中に何かカメラの動作とかが必要な時のフラグ
    public bool EventActionFlg;
}
