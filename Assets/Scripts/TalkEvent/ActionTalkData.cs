using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static ActionTalkData.testList;

[CreateAssetMenu]
public class ActionTalkData : ScriptableObject
{
    [field: SerializeField]
    private List<testList> test;

    //public string CharacterName;
    
    [System.Serializable]
    public struct testList
    {
        [SerializeField]
        public CharacterName character;


        [SerializeField, TextArea]
        public string Conversations;

        public enum CharacterName
        {
            player,
            enemy,
        }

    }

}

public class testList : MonoBehaviour
{
    public static testList instance { get; private set; }
    public string PlayerName;
    //private ActionTalkData action;

    TextMeshProUGUI text;
    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        
    }

    public void test(ActionTalkData.testList.CharacterName charaname)
    {
        if (CharacterName.player!=null)
        {
            Debug.Log("プレイヤーが選択されました");
        }
    }

    public void GetCharacterName(string name)
    {
        PlayerName = name;
    }


}


