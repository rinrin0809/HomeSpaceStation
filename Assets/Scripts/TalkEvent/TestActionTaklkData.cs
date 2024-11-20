using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestActionTalkData : ScriptableObject
{
    public string talkEventName;
    [SerializeField, TextArea]
    public List<string> Conversations; // 複数の会話内容を格納



}

