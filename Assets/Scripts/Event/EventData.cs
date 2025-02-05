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
    private string[] EventNames =
{
    "車故障",
    "黒い影を発見",
    "黒い影が逃げる",
    "旅館を見つけた時の会話",
    "女将との会話",
    "主人公・友人・女将との会話",
    "お面の初登場",
    "主人公と主人公の彼女が中庭で分かれる"
};

    public void SetEvents(Event[] Event)
    {
        Events = Event;
    }

    public Event[] GetEvents()
    {
        return Events;
    }

    public void Initialize()
    {
        Events = new Event[Size];
        //イベントの番号設定
        SetEventNumber();
        //イベントの名前設定
        SetEventName(EventNames);
        //フラグをリセット
        AllResetFlag();
    }

    // 特定のイベントフラグを設定
    public void SetEventFlag(string Name, bool Flg)
    {
        if (Events == null) return;
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
        if (Events == null) return false;
        for (int i = 0; i < Events.Length; i++)
        {
            if(Events[i].EventName == Name)
            {
                return Events[i].EventFlag;
            }
        }
        return false;
    }

    //イベントが終了した時のフラグを設定
    public void SetEndEventFlg(string Name, bool Flg)
    {
        if (Events == null) return;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EndEventFlg = Flg;
            }
        }
    }

    //イベントが終了した時のフラグを取得
    public bool GetNameEndEventActionFlg(string Name)
    {
        if (Events == null) return false;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                return Events[i].EndEventFlg;
            }
        }
        return false;
    }

    //イベント中に何かカメラの動作とかが必要な時のフラグを設定
    public void SetEventActionEventFlg(string Name, bool Flg)
    {
        if (Events == null) return;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EventActionFlg = Flg;
            }
        }
    }

    //イベント中に何かカメラの動作とかが必要な時のフラグを取得
    public bool GetNameEventActionFlg(string Name)
    {
        if (Events == null) return false;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                return Events[i].EventActionFlg;
            }
        }
        return false;
    }

    //イベントの番号設定
    public void SetEventNumber()
    {
        if (Events == null) return;
        for (int i = 0; i < Size; i++)
        {
            Events[i].EventNumber = i;
        }
    }

    //イベントの名前設定
    public void SetEventName(string[] EventNames)
    {
        if (Events == null) return;
        for (int i = 0; i < Size; i++)
        {
            Events[i].EventName = EventNames[i];
        }
    }

    //総当たりでイベントが発生しているかチェック
    public bool IsEvent()
    {
        if (Events == null) return false;

        for(int i = 0; i < Events.Length; i++)
        {
            if(!Events[i].Equals(default(Event)))
            if (Events[i].EventFlag) return true;
        }

        return false;
    }

    public void AllResetFlag()
    {
        Debug.Log("タイトルなのでリセットします");
        ResetEventflag();
        ResetEndEventflag();
        ResetEventActionflag();
    }

    public void ResetEventflag()
    {
        if (Events == null) return;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventFlag == true)
            {
                Events[i].EventFlag = false;
            }
        }
    }

    public void ResetEndEventflag()
    {
        if (Events == null) return;
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EndEventFlg == true)
            {
                Events[i].EndEventFlg = false;
            }
        }
    }

    public void ResetEventActionflag()
    {
        if (Events == null) return;
        for (int i = 0;i<Events.Length; i++)
        {
            if (Events[i].EventActionFlg == true)
            {
                
                Events[i].EventActionFlg = false;
            }
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
