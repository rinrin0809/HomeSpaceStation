using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTalkManager : MonoBehaviour
{
    // 会話データリスト
    [SerializeField] List<TestActionTalkData> TestactionTalkList;

    public static TestTalkManager Instance { get; private set; }

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
    public string GetTalk(int actionIndex,int talkIndex)
    {
        if (actionIndex >= 0 && actionIndex < TestactionTalkList.Count)
        {
            var actiontalk = TestactionTalkList[actionIndex];
            if(talkIndex>=0&&talkIndex< actiontalk.Conversations.Count)
            {
                return actiontalk.Conversations[talkIndex];
            }
           
        }
        return "会話データが存在しません";
    }
}
