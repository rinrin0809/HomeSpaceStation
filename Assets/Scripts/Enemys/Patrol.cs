using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //�ړ�
    public List<Transform> positions; // ����|�C���g�̃��X�g
    public List<Transform> randomPositions; // �����_���ړ���̃��X�g
    public float randomPatrolChance = 0.5f;
    public float speed = 2f; // �ړ����x
    public float rotationSpeed = 180f; // ��]���x�i�x/�b�j

    //�U��Ԃ�
    public Transform player; // �v���C���[��Transform
    public float fieldOfViewAngle = 90f; // ����p
    public float detectionDistance = 5f; // �U��Ԃ蓮��̔�������
    public float behaviorResetTime = 3f; // �U��Ԃ��Ɍ��̍s���ɖ߂�܂ł̎���
    public float lookAtDelay = 1f; // �U��Ԃ�O�̒x������

    //�w��|�W�V����
    private int currentTargetIndex = 0; // ���݂̃^�[�Q�b�g�|�W�V�����̃C���f�b�N�X
    private bool isRandomPatrol = false; // �����_���p�g���[����Ԃ��ǂ���
    private Transform randomTarget; // ���݂̃����_���^�[�Q�b�g

    //����
    private bool isLookingAtPlayer = false; // �v���C���[�����Ă����Ԃ��ǂ���
    private bool isPreparingToLookAtPlayer = false; // �U��Ԃ鏀�������ǂ���
    private Coroutine resetBehaviorCoroutine;

    //
    private Transform returnPosition; // �ꎞ�I�ȕ��A��
    public Transform randomPatrolFagPosition; // RandomPatrolFag �̈ʒu��ݒ�
    public float detectionRange = 0.5f; // RandomPatrolFag �ɓ��B�Ƃ݂Ȃ�����

    private int half = 2;
    void Update()
    {
        if (isLookingAtPlayer)
        {
            LookPlayer();
        }
        else
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
            //�v���C���[�̃`�F�b�N
            CheackForPlayer();
        }
    }

    private void CheackForPlayer()
    {
       if (player == null || isPreparingToLookAtPlayer || isLookingAtPlayer) return;

        //�v���C���[�Ƃ̋������`�F�b�N
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer<= detectionDistance)
        {
            //����p���ɂ��邩���`�F�b�N
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.up, directionToPlayer);

            //����O�Ȃ�
            if (angleToPlayer > fieldOfViewAngle / half)
            {
                Debug.Log("�v���C���[�������𖞂����܂����B�U��Ԃ菀�����J�n���܂��B");
                isPreparingToLookAtPlayer = true;
                StartCoroutine(DelayLookAtPlayer());
            }
        }
    }

    private IEnumerator DelayLookAtPlayer()
    {
        yield return new WaitForSeconds(lookAtDelay);

        // �U��Ԃ蓮��̊J�n
        Debug.Log("�U��Ԃ�܂��B");
        isPreparingToLookAtPlayer = false;
        isLookingAtPlayer = true;

        // �U��Ԃ��Ɍ��̍s���ɖ߂邽�߂̃^�C�}�[�J�n
        if (resetBehaviorCoroutine != null)
        {
            StopCoroutine(resetBehaviorCoroutine);
        }
        resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
    }

    private void LookPlayer()
    {
        if (player == null) return;

        //�v���C���[�̕���������
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ResetBehaviorAfterDelay()
    {
        yield return new WaitForSeconds(behaviorResetTime);
        Debug.Log("���̍s���ɖ߂�܂��B");

        isLookingAtPlayer = false;
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
