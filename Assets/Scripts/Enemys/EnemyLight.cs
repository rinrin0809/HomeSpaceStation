using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRight : MonoBehaviour
{
    public Patrol enemy; // �G�I�u�W�F�N�g�̃X�N���v�g���Q��
    public float rotationSpeed = 180f; // ��]���x�i�x/�b�j

    void Update()
    {
        if (enemy != null)
        {
            RotateTowardsEnemyDirection();
        }
    }

    private void RotateTowardsEnemyDirection()
    {
        // �G�̌��݂̈ړ��������擾
        Vector3 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // �L���Ȉړ�����������ꍇ�̂ݏ���
        {
            // ��]��̊p�x���v�Z
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // �X���[�Y�Ƀ^�[�Q�b�g����������
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}