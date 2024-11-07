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

    //// シーンごとにラベルを定義
    //public enum ConversationLabel
    //{
    //    Reception,
    //    EnemyAppears,
    //    // 他の会話シーンを追加可能
    //}

    //// 各会話エントリごとのラベルを定義
    //public enum ConversationEntryLabel
    //{
    //    Greeting,
    //    Warning,
    //    Farewell,
    //    // 他の会話内容のラベルを追加可能
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

    //// Dictionaryを初期化し、外部ラベルごとの会話内容を内部ラベルで登録
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

    //// 外部ラベルと内部ラベルで会話内容を取得するメソッド
    //public string GetConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    //{
    //    if (conversationDictionary.TryGetValue(sceneLabel, out var entryDictionary) &&
    //        entryDictionary.TryGetValue(entryLabel, out var conversation))
    //    {
    //        return conversation;
    //    }
    //    return "会話データが存在しません";
    //}

    //// 会話を開始
    //public void StartConversation(ConversationLabel sceneLabel, ConversationEntryLabel entryLabel)
    //{
    //    string conversation = GetConversation(sceneLabel, entryLabel);
    //    Debug.Log("表示する会話内容: " + conversation);
    //    // UIの表示処理などをここに追加
    //    text.text = conversation;
    //}
}

//namespace TalkManager
//{
//    [System.Serializable]
//    public class ConversationEntry
//    {
//        // 会話内容のラベル（各シーン内での識別用）
//        public enum ConversationEntryLabel
//        {
//            Test1,   // 受付会話1
//            Test2,   // 受付会話2
//            Enemy1,  // エネミー戦1
//            Enemy2   // エネミー戦2
//        }

//        public ConversationEntryLabel entryLabel;  // 会話のラベル
//        [TextArea] public string content;          // 実際の会話内容
//    }
//}