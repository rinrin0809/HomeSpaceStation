using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
    public Quaternion playerRotation;  // �ǉ�: �v���C���[�̌���
    public bool isInitialPositionSet = false; // �����|�W�V�����ݒ�ς݃t���O

}
