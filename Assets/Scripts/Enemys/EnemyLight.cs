using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRight : MonoBehaviour
{
    public Fov_script enemy; // �G�I�u�W�F�N�g�̃X�N���v�g���Q��
    public Transform lightTransform; // ���C�g��Transform
    public float rotationSpeed = 180f; // ��]���x�i�x/�b�j

    void Update()
    {
        if (enemy != null)
        {
            RotateTowardsEnemyDirection();

            if (lightTransform != null)
            {
                AlignLightToDirection();
            }
        }
    }

    private void RotateTowardsEnemyDirection()
    {
        Vector2 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // �L���Ȉړ�����������ꍇ�̂ݏ���
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // �X���[�Y�Ƀ^�[�Q�b�g����������
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AlignLightToDirection()
    {
        Vector2 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // �L���Ȉړ�����������ꍇ�̂ݏ���
        {
            // ���C�g�̕�����G�̈ړ������Ɉ�v������
            lightTransform.up = direction;
        }
    }

    private void OnDrawGizmos()
    {
        if (lightTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(lightTransform.position, lightTransform.position + lightTransform.up * 2f);
        }
    }
}