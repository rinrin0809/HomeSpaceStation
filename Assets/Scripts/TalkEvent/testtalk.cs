using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class testtalk : ScriptableObject
{
    public string EventName;

    public List<testTalk> testList = new List<testTalk>();



    // 特定の会話を取得するメソッド
    public string GetName(int index)
    {
        if (index >= 0 && index < testList.Count)
        {
            return testList[index].charadata.CharacterName;
        }
        return "";
    }

    public string GetConverstaion(int index)
    {
        if(index>=0&& index < testList.Count)
        {
            return testList[index].Conversations;
        }
        return "";
    }


    [System.Serializable]
    public struct testTalk
    {

        [SerializeField]
        public CharacterData charadata;

        [SerializeField, TextArea]
        public string Conversations; // 複数の会話内容を格納
    }
}
