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
    private string[] EventNames =
{
    "�Ԍ̏�",
    "�����e�𔭌�",
    "�����e��������",
    "���ق����������̉�b",
    "�����Ƃ̉�b",
    "��l���E�F�l�E�����Ƃ̉�b",
    "���ʂ̏��o��",
    "��l���Ǝ�l���̔ޏ�������ŕ������"
};

    public Event[] GetEvents()
    {
        return Events;
    }

    public void Initialize()
    {
        Events = new Event[Size];
        //�C�x���g�̔ԍ��ݒ�
        SetEventNumber();
        //�C�x���g�̖��O�ݒ�
        SetEventName(EventNames);
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

    //�C�x���g���I���������̃t���O��ݒ�
    public void SetEndEventFlg(string Name, bool Flg)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EndEventFlg = Flg;
            }
        }
    }

    //�C�x���g���I���������̃t���O���擾
    public bool GetNameEndEventActionFlg(string Name)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                return Events[i].EndEventFlg;
            }
        }
        return false;
    }

    //�C�x���g���ɉ����J�����̓���Ƃ����K�v�Ȏ��̃t���O��ݒ�
    public void SetEventActionEventFlg(string Name, bool Flg)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                Events[i].EventActionFlg = Flg;
            }
        }
    }

    //�C�x���g���ɉ����J�����̓���Ƃ����K�v�Ȏ��̃t���O���擾
    public bool GetNameEventActionFlg(string Name)
    {
        for (int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventName == Name)
            {
                return Events[i].EventActionFlg;
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

    //�C�x���g�̖��O�ݒ�
    public void SetEventName(string[] EventNames)
    {
        for (int i = 0; i < Size; i++)
        {
            Events[i].EventName = EventNames[i];
        }
    }

    //��������ŃC�x���g���������Ă��邩�`�F�b�N
    public bool IsEvent()
    {
        for(int i = 0; i < Events.Length; i++)
        {
            if (Events[i].EventFlag) return true;
        }

        return false;
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
