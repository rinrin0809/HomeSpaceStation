using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // ���̃N���X�̃C���X�^���X��ێ����Ă������߂̐ÓI�ȕϐ�
    private static Data instance;

    void Awake()
    {
        // ���ɃC���X�^���X�����݂���ꍇ�A�d�������I�u�W�F�N�g��j������
        if (instance != null)
        {
            Destroy(gameObject); // �d�����Ă���ꍇ�͍폜
        }
        else
        {
            instance = this; // �C���X�^���X���ݒ肳��Ă��Ȃ��ꍇ�A���݂̃I�u�W�F�N�g���C���X�^���X�Ƃ��Đݒ�
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��̃I�u�W�F�N�g��ێ�����
        }
    }
}
