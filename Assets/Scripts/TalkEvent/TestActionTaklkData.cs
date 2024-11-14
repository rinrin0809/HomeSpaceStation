using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestActionTalkData : ScriptableObject
{
    // 会話内容
    public string Conversation { get; set; }
    public string listName;

    public enum Action
    {
        Reception,    // 例: 受付での会話
        EnemyAppears, // 例: エネミーが出現した時の会話
        // 必要に応じて他のシーンも追加
    }

    public Action action;

    //public enum CharacterName
    //{
    //    // 仮
    //    Player, // 主人公
    //    Enemy, // 敵
    //    Okami, // 女将
    //    // 必要に応じて追加
    //}


    [SerializeField, TextArea]
    public List<string> Conversations; // 複数の会話内容を格納

}

