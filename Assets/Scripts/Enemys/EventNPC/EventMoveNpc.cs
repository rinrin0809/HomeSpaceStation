using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveNpc : MonoBehaviour
{
    public List<Transform> targets; // �ǐՂ���^�[�Q�b�g�̃��X�g
    public float speed = 3f; // �ړ����x
    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�̃C���f�b�N�X

    void Update()
    {
        if (targets.Count == 0) return;

        // ���݂̃^�[�Q�b�g�Ɍ������Ĉړ�
        Transform target = targets[currentTargetIndex];

        // �ڕW�����ւ̃x�N�g��
        Vector3 direction = (target.position - transform.position).normalized;

        // �ړ�
        transform.position += direction * speed * Time.deltaTime;

        // �^�[�Q�b�g�ɓ��B�����玟�̃^�[�Q�b�g�Ɉړ�
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Count; // ���̃^�[�Q�b�g�ɐ؂�ւ�
        }
    }
}
