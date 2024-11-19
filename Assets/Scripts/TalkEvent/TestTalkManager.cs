using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTalkManager : MonoBehaviour
{
    // 会話データリスト
    [SerializeField]
    public TalkManagerList talklist;
    //public ActionEvent actionEvent;

    public static TestTalkManager Instance { get; private set; }

    //testList test;

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
    public string GetTalk(int actionIndex, int talkIndex)
    {
        if (actionIndex >= 0 && actionIndex < talklist.TalkEventList.Count)
        {
            var actiontalk = talklist.TalkEventList[actionIndex];
            if (talkIndex >= 0 && talkIndex < actiontalk.Conversations.Count)
            {
                //testList.instance.test(ActionTalkData.testList.CharacterName.player);
                return actiontalk.Conversations[talkIndex];
            }
        }

        return "";
    }

    //public void UpdateFisishflag()
    //{
    //    if (actionEvent.finishtalk)
    //    {
    //        actionEvent.finishtalk = false;

    //    }
    //}
}
