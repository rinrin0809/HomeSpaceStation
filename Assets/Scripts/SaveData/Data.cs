using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��̃I�u�W�F�N�g��ێ�����
    }
}
