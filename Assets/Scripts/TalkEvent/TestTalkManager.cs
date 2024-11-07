using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTalkManager : MonoBehaviour
{
    // ��b�f�[�^���X�g
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

    // ��b�f�[�^���擾���郁�\�b�h
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
        return "��b�f�[�^�����݂��܂���";
    }
}

[System.Serializable]
[CreateAssetMenu]
public class TalkManagerList :ScriptableObject
{
    public List<TestActionTalkData> TalkEventList=new List<TestActionTalkData>();
    
}
