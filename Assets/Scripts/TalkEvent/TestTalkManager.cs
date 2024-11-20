using JetBrains.Annotations;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestTalkManager : MonoBehaviour
{
    // 会話データリスト
    [SerializeField]
    public TalkManagerList talklist;

    //[SerializeField]
    //public testtalk test;
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
    //public string GetTalk(int actionIndex, int talkIndex)
    //{
    //    if (actionIndex >= 0 && actionIndex < talklist.TalkEventList.Count)
    //    {
    //        var actiontalk = talklist.TalkEventList[actionIndex];
    //        if (talkIndex >= 0 && talkIndex < actiontalk.Conversations.Count)
    //        {
    //            //testList.instance.test(ActionTalkData.testList.CharacterName.player);
    //            Debug.Log("talkindex:" + talkIndex);
    //            return actiontalk.Conversations[talkIndex];
    //        }
    //    }

    //    return "";
    //}

    public string GetTalk(int talkIndex, int actionIndex)
    {
  
        string text;
        
        if (talkIndex >= 0 && talkIndex < talklist.testtalkList.Count())
        {
            var actiontalk = talklist.testtalkList[talkIndex];
            if (actionIndex >= 0 && actionIndex < actiontalk.testList.Count)
            {
                //name = talklist.testtalkList[actionIndex].GetName(talkIndex);
                text = talklist.testtalkList[talkIndex].GetConverstaion(actionIndex);
                return text;
            }
        }
        return "";
    }


    public string GetTalkName(int talkIndex, int actionIndex)
    {
        string name;
       
        if (talkIndex >= 0 && talkIndex < talklist.testtalkList.Count())
        {
            var actiontalk = talklist.testtalkList[talkIndex];
            if (actionIndex >= 0 && actionIndex < actiontalk.testList.Count)
            {
                name = talklist.testtalkList[talkIndex].GetName(actionIndex);
                
                return name;
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
