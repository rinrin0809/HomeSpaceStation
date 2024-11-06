using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //[SerializeField] AudioSource seAudioSource;

    // 会話データリスト
    [SerializeField] List<ActionTalkData> actionTalkList;

    public static TalkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 会話データを取得するメソッド
    public string GetTalk(int index)
    {
        if (index >= 0 && index < actionTalkList.Count)
        {
            return actionTalkList[index].Conversation;
        }
        return "会話データが存在しません";
    }
}

//[System.NonSerialized]
