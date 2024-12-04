using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // �ړ�����|�W�V�����̃��X�g
    public float speed = 2f; // �ړ����x
    public float rotationSpeed = 180f; // ��]���x�i�x/�b�j
    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�|�W�V�����̃C���f�b�N�X

    void Update()
    {
        MoveToPosition();
    }

    void MoveToPosition()
    {
        if (positions.Count == 0) return; // �|�W�V�������Ȃ��ꍇ�͉������Ȃ�

        Transform target = positions[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        // �ړ�
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // �X���[�Y�Ƀ^�[�Q�b�g����������
        if (direction.magnitude > 0.1f) // �^�[�Q�b�g���߂����Ȃ��ꍇ�̂݉�]
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 90�x�I�t�Z�b�g
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle); // �^�[�Q�b�g�̉�]
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // ���݂̃^�[�Q�b�g�ɓ��B���������`�F�b�N
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= positions.Count)
            {
                currentTargetIndex = 0; // ���X�g�̍ŏ��ɖ߂�i����j
            }
        }
    }
}
