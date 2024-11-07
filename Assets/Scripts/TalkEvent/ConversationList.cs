using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationList : ScriptableObject
{
    // ��b���X�g�̃��x���i��t�A�G�l�~�[��Ȃǁj
    public TalkManager.ConversationLabel label;

    // �e��b�G���g���[�̃��X�g
    public List<TalkManager.ConversationEntry> conversations;
}