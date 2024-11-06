using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionTalkData : ScriptableObject
{
    // ‰ï˜b“à—e
    [field: SerializeField]
    [field: TextArea]
    public string Conversation { get; set; }
}

[CreateAssetMenu]
public class ActionTalkListData : ScriptableObject
{
    [SerializeField]
    public List<ActionTalkData> list;
}