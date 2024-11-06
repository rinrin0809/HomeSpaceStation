using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationList : ScriptableObject
{
    // 会話リストのラベル（受付、エネミー戦など）
    public TalkManager.ConversationLabel label;

    // 各会話エントリーのリスト
    public List<TalkManager.ConversationEntry> conversations;
}