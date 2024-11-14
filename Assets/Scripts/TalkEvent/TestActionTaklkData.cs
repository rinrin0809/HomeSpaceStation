using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestActionTalkData : ScriptableObject
{
    // ��b���e
    public string Conversation { get; set; }
    public string listName;

    public enum Action
    {
        Reception,    // ��: ��t�ł̉�b
        EnemyAppears, // ��: �G�l�~�[���o���������̉�b
        // �K�v�ɉ����đ��̃V�[�����ǉ�
    }

    public Action action;

    //public enum CharacterName
    //{
    //    // ��
    //    Player, // ��l��
    //    Enemy, // �G
    //    Okami, // ����
    //    // �K�v�ɉ����Ēǉ�
    //}


    [SerializeField, TextArea]
    public List<string> Conversations; // �����̉�b���e���i�[

}

