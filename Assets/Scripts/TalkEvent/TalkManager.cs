using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[CreateAssetMenu]
//public class TalkManager : ScriptableObject
public class TalkManager : MonoBehaviour
{
    //----------------------------------------------------------

    //public TextMeshProUGUI text;

    //[System.Serializable]
    //public class ConversationEntry
    //{
    //    public ConversationEntryLabel entryLabel;
    //    [TextArea] public string content;
    //}

    //[System.Serializable]
    //public class LabeledConversationList
    //{
    //    public ConversationLabel label;
    //    public List<ConversationEntry> conversations;
    //}

    //// �V�[�����ƂɃ��x�����`
    //public enum ConversationLabel
    //{
    //    Reception,
    //    EnemyAppears,
    //    // ���̉�b�V�[����ǉ��\
    //}

    //// �e��b�G���g�����Ƃ̃��x�����`
    //public enum ConversationEntryLabel
    //{
    //    Greeting,
    //    Warning,
    //    Farewell,
    //    // ���̉�b���e�̃��x����ǉ��\
    //}

    //[SerializeField] private List<LabeledConversationList> labeledConversationLists;
    //private Dictionary<ConversationLabel, Dictionary<ConversationEntryLabel, string>> conversationDictionary;

    //public static TalkManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //        InitializeConversationDictionary();
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// Dictionary�����������A�O�����x�����Ƃ̉�b���e��������x���œo�^
    //private void InitializeConversationDictionary()
    //{
    //    conversationDictionary = new Dictionary<ConversationLabel, Dictionary<ConversationEntryLabel, string>>();

    //    foreach (var list in labeledConversationLists)
    //    {
    //        var entryDictionary = new Dictionary<ConversationEntryLabel, string>();

    //        foreach (var entry in list.conversations)
    //        {
    //            if (!entryDictionary.ContainsKey(entry.entryLabel))
    //            {
    //                entryDictionary.Add(entry.entryLabel, entry.content);
    //            }
    //        }
    //        conversationDictionary[list.label] = entryDictionary;
    //    }
    //}

    //// �O�����x���Ɠ������x���ŉ�b���e���擾���郁�\�b�h
    //public string GetConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    //{
    //    if (conversationDictionary.TryGetValue(sceneLabel, out var entryDictionary) &&
    //        entryDictionary.TryGetValue(entryLabel, out var conversation))
    //    {
    //        return conversation;
    //    }
    //    return "��b�f�[�^�����݂��܂���";
    //}

    //// ��b���J�n
    //public void StartConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    //{
    //    string conversation = GetConversation(sceneLabel, entryLabel);
    //    Debug.Log("�\�������b���e: " + conversation);
    //    // UI�̕\�������Ȃǂ������ɒǉ�
    //    text.text = conversation;
    //}
}

//namespace TalkManager
//{
//    [System.Serializable]
//    public class ConversationEntry
//    {
//        // ��b���e�̃��x���i�e�V�[�����ł̎��ʗp�j
//        public enum ConversationEntryLabel
//        {
//            Test1,   // ��t��b1
//            Test2,   // ��t��b2
//            Enemy1,  // �G�l�~�[��1
//            Enemy2   // �G�l�~�[��2
//        }

//        public ConversationEntryLabel entryLabel;  // ��b�̃��x��
//        [TextArea] public string content;          // ���ۂ̉�b���e
//    }
//}