using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // ����|�C���g�̃��X�g
    public List<Transform> randomPositions; // �����_���ړ���̃��X�g
    public float speed = 2f; // �ړ����x
    public float rotationSpeed = 180f; // ��]���x�i�x/�b�j

    public float randomPatrolChance = 0.5f;

    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�|�W�V�����̃C���f�b�N�X
    private bool isRandomPatrol = false; // �����_���p�g���[����Ԃ��ǂ���
    private Transform randomTarget; // ���݂̃����_���^�[�Q�b�g

    private Transform returnPosition; // �ꎞ�I�ȕ��A��
    public Transform randomPatrolFagPosition; // RandomPatrolFag �̈ʒu��ݒ�
    public float detectionRange = 0.5f; // RandomPatrolFag �ɓ��B�Ƃ݂Ȃ�����
    void Update()
    {
       
        if (isRandomPatrol)
        {
            MoveToRandomPosition();
        }
        else
        {
            CheckForRandomPatrolFag();
            MoveToPosition();
        }
    }

    private void CheckForRandomPatrolFag()
    {
        if (randomPatrolFagPosition == null) return;

        // �������v�Z���ă`�F�b�N
        float distanceToFag = Vector3.Distance(transform.position, randomPatrolFagPosition.position);
        if (distanceToFag < detectionRange)
        {
            Debug.Log("RandomPatrolFag �ɓ��B���܂���");

            // �����_���p�g���[���ւ̐؂�ւ�
            if (randomPositions.Count > 0)
            {
                randomTarget = randomPositions[Random.Range(0, randomPositions.Count)];
                Debug.Log($"�����_���^�[�Q�b�g: {randomTarget.name} �Ɉڍs���܂�");

                isRandomPatrol = true;
            }
        }
    }

        private void MoveToPosition()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex];
        MoveAndRotateTowards(target);

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

    private void MoveToRandomPosition()
    {
        if (randomTarget == null) return;

        MoveAndRotateTowards(randomTarget);

        // �����_���^�[�Q�b�g�ɓ��B���������`�F�b�N
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            Debug.Log("�����_���^�[�Q�b�g�ɓ��B���܂���");
            isRandomPatrol = false; // �����_���p�g���[���I��
        }
    }

    private void MoveAndRotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        // �ړ�
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // �X���[�Y�Ƀ^�[�Q�b�g����������
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 90�x�I�t�Z�b�g
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
