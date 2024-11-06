using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //// 会話データリスト
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

    //// 会話データを取得するメソッド
    //public string GetTalk(int index)
    //{
    //    if (index >= 0 && index < actionTalkList.Count)
    //    {
    //        return actionTalkList[index].Conversation;
    //    }
    //    return "会話データが存在しません";
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

    //// ラベル（Action）とインデックスで会話データを取得するメソッド
    //public string GetConversation(ActionTalkData.Action actionLabel, int index)
    //{
    //    ActionTalkData actionData = actionTalkList.Find(data => data.action == actionLabel);

    //    if (actionData != null && index >= 0 && index < actionData.Conversations.Count)
    //    {
    //        return actionData.Conversations[index];
    //    }
    //    return "会話データが存在しません";
    //}

    //// 会話を開始する
    //public void StartConversation(ActionTalkData.Action actionLabel, int index)
    //{
    //    string conversation = GetConversation(actionLabel, index);
    //    // 会話内容を表示するUIの更新コードをここに追加
    //    text.text = conversation;
    //    Debug.Log("表示する会話内容: " + conversation);
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

    // シーンごとにラベルを定義
    public enum ConversationLabel
    {
        Reception,
        EnemyAppears,
        // 他の会話シーンを追加可能
    }

    // 各会話エントリごとのラベルを定義
    public enum ConversationEntryLabel
    {
        Greeting,
        Warning,
        Farewell,
        // 他の会話内容のラベルを追加可能
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

    // Dictionaryを初期化し、外部ラベルごとの会話内容を内部ラベルで登録
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

    // 外部ラベルと内部ラベルで会話内容を取得するメソッド
    public string GetConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    {
        if (conversationDictionary.TryGetValue(sceneLabel, out var entryDictionary) &&
            entryDictionary.TryGetValue(entryLabel, out var conversation))
        {
            return conversation;
        }
        return "会話データが存在しません";
    }

    // 会話を開始
    public void StartConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    {
        string conversation = GetConversation(sceneLabel, entryLabel);
        Debug.Log("表示する会話内容: " + conversation);
        // UIの表示処理などをここに追加
    }
}
