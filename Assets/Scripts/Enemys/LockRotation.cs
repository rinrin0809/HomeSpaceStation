using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // �����̃��[�J����]���L�^
        initialRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // �e�I�u�W�F�N�g�̉�]�𖳎����ď�����]��ێ�
        transform.localRotation = initialRotation;
    }
}
