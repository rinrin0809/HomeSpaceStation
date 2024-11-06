using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //// ��b�f�[�^���X�g
    //[SerializeField] List<ActionTalkData> actionTalkList;

    //public static TalkManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// ��b�f�[�^���擾���郁�\�b�h
    //public string GetTalk(int index)
    //{
    //    if (index >= 0 && index < actionTalkList.Count)
    //    {
    //        return actionTalkList[index].Conversation;
    //    }
    //    return "��b�f�[�^�����݂��܂���";
    //}


    //----------------------------------------------------------

    // public TextMeshProUGUI text;

    //[SerializeField] private List<ActionTalkData> actionTalkList;

    //public static TalkManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// ���x���iAction�j�ƃC���f�b�N�X�ŉ�b�f�[�^���擾���郁�\�b�h
    //public string GetConversation(ActionTalkData.Action actionLabel, int index)
    //{
    //    ActionTalkData actionData = actionTalkList.Find(data => data.action == actionLabel);

    //    if (actionData != null && index >= 0 && index < actionData.Conversations.Count)
    //    {
    //        return actionData.Conversations[index];
    //    }
    //    return "��b�f�[�^�����݂��܂���";
    //}

    //// ��b���J�n����
    //public void StartConversation(ActionTalkData.Action actionLabel, int index)
    //{
    //    string conversation = GetConversation(actionLabel, index);
    //    // ��b���e��\������UI�̍X�V�R�[�h�������ɒǉ�
    //    text.text = conversation;
    //    Debug.Log("�\�������b���e: " + conversation);
    //}

    //-----------------------------------------------------------------------

    [System.Serializable]
    public class ConversationEntry
    {
        public ConversationEntryLabel entryLabel;
        [TextArea] public string content;
    }

    [System.Serializable]
    public class LabeledConversationList
    {
        public ConversationLabel label;
        public List<ConversationEntry> conversations;
    }

    // �V�[�����ƂɃ��x�����`
    public enum ConversationLabel
    {
        Reception,
        EnemyAppears,
        // ���̉�b�V�[����ǉ��\
    }

    // �e��b�G���g�����Ƃ̃��x�����`
    public enum ConversationEntryLabel
    {
        Greeting,
        Warning,
        Farewell,
        // ���̉�b���e�̃��x����ǉ��\
    }

    [SerializeField] private List<LabeledConversationList> labeledConversationLists;
    private Dictionary<ConversationLabel, Dictionary<ConversationEntryLabel, string>> conversationDictionary;

    public static TalkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeConversationDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Dictionary�����������A�O�����x�����Ƃ̉�b���e��������x���œo�^
    private void InitializeConversationDictionary()
    {
        conversationDictionary = new Dictionary<ConversationLabel, Dictionary<ConversationEntryLabel, string>>();

        foreach (var list in labeledConversationLists)
        {
            var entryDictionary = new Dictionary<ConversationEntryLabel, string>();

            foreach (var entry in list.conversations)
            {
                if (!entryDictionary.ContainsKey(entry.entryLabel))
                {
                    entryDictionary.Add(entry.entryLabel, entry.content);
                }
            }
            conversationDictionary[list.label] = entryDictionary;
        }
    }

    // �O�����x���Ɠ������x���ŉ�b���e���擾���郁�\�b�h
    public string GetConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    {
        if (conversationDictionary.TryGetValue(sceneLabel, out var entryDictionary) &&
            entryDictionary.TryGetValue(entryLabel, out var conversation))
        {
            return conversation;
        }
        return "��b�f�[�^�����݂��܂���";
    }

    // ��b���J�n
    public void StartConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    {
        string conversation = GetConversation(sceneLabel, entryLabel);
        Debug.Log("�\�������b���e: " + conversation);
        // UI�̕\�������Ȃǂ������ɒǉ�
    }
}
