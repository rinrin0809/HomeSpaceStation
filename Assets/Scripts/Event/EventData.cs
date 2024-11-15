using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventData : ScriptableObject
{
    // �C�x���g�̃f�[�^�z��
    [SerializeField]
    private Event[] Events;

    //�z��
    [SerializeField]
    public int Size = 8;

    //�C�x���g�̖��O
    private string[] EventNames;

    public void Initialize()
    {
        Events = new Event[Size];
        SetEventNumber();
    }

    // ����̃C�x���g�t���O��ݒ�
    public void SetEventFlag(string Name, bool Flg)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EventFlag = Flg;
            }
        }
    }

    // ����̖��O�̃C�x���g�t���O���擾
    public bool GetNameEventFlg(string Name)
    {
        for(int i = 0; i < Events.Length; i++)
        {
            if(Events[i].EventName == Name)
            {
                return Events[i].EventFlag;
            }
        }
        return false;
    }

    //�C�x���g�̔ԍ��ݒ�
    public void SetEventNumber()
    {
        for (int i = 0; i < Size; i++)
        {
            Events[i].EventNumber = i;
        }
    }
}

// �C�x���g�̍\����
[System.Serializable]
public struct Event
{
    //�C�x���g�̖��O�i�C���X�y�N�^�[�ɕ\�����邾���j
    [SerializeField]
    public string EventName;

    [SerializeField]
    //�z��̔ԍ�
    public int EventNumber;

    //�C�x���g�̃t���O
    [SerializeField]
    public bool EventFlag;

    [SerializeField]
    //�C�x���g���I���������̃t���O
    public bool EndEventFlg;

    [SerializeField]
    //�C�x���g���ɉ����J�����̓���Ƃ����K�v�Ȏ��̃t���O
    public bool EventActionFlg;
}
