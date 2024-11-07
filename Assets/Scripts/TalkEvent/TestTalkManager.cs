using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTalkManager : MonoBehaviour
{
    // 会話データリスト
    [SerializeField]
    public TalkManagerList talklist;
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
        if (actionIndex >= 0 && actionIndex < talklist.TalkEventList.Count)
        {
            var actiontalk = talklist.TalkEventList[actionIndex];
            if(talkIndex>=0&&talkIndex< actiontalk.Conversations.Count)
            {
                return actiontalk.Conversations[talkIndex];
            }
           
        }
        return "会話データが存在しません";
    }
}

[System.Serializable]
[CreateAssetMenu]
public class TalkManagerList :ScriptableObject
{
    public List<TestActionTalkData> TalkEventList=new List<TestActionTalkData>();
    
}
