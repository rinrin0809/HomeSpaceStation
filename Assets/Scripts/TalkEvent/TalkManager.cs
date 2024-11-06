using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //[SerializeField] AudioSource seAudioSource;

    // ��b�f�[�^���X�g
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

    // ��b�f�[�^���擾���郁�\�b�h
    public string GetTalk(int index)
    {
        if (index >= 0 && index < actionTalkList.Count)
        {
            return actionTalkList[index].Conversation;
        }
        return "��b�f�[�^�����݂��܂���";
    }
}

//[System.NonSerialized]
