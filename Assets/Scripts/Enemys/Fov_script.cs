using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fieldOfViewAngle = 90f; // �O������p
    public float detectionDistance = 10f; // �v���C���[���m����
    public Transform player; // �v���C���[��Transform

    private bool isPlayerInView = false; // �v���C���[��������ɂ��邩�ǂ���
    private Rigidbody enemyRigidbody; // �G��Rigidbody
    public float stopDuration = 2f; // ��~���鎞��
    private float stopTimer = 0f; // ��~���Ԃ��v������^�C�}�[

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>(); // Rigidbody���擾
    }

    void Update()
    {
        bool wasInView = isPlayerInView;
        isPlayerInView = CheckAndSetPlayerInView(transform);

        if (isPlayerInView && !wasInView)
        {
            Debug.Log("�v���C���[�����F���܂����I��~���܂��B");
            StopEnemy();
        }

        // ��~���Ԃ̊Ǘ�
        if (stopTimer > 0f)
        {
            stopTimer -= Time.deltaTime;
            Debug.Log("��~��: �c�莞�� " + stopTimer);
            if (stopTimer <= 0f)
            {
                stopTimer = 0f;  // �O�̂��߃[���Ƀ��Z�b�g
                ResumeEnemy();
            }
        }
        else
        {
            Debug.Log("��~���Ă��܂���");
        }
    }

    private void StopEnemy()
    {
        // �G�̓������~����
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector3.zero; // ���x���[����
            enemyRigidbody.angularVelocity = Vector3.zero; // ��]���[����
            enemyRigidbody.isKinematic = true;  // �����������~
            Debug.Log("�G�̓�������~���܂���");
        }
        else
        {
            Debug.LogError("Rigidbody��null�ł��I");
        }

        stopTimer = stopDuration; // ��~�^�C�}�[�����Z�b�g
    }

    private void ResumeEnemy()
    {
        // �G�̓�����ĊJ
        enemyRigidbody.isKinematic = false;  // �����������ĊJ
        Debug.Log("��~���Ԃ��I�����܂����B������ĊJ���܂��B");
    }

    // �v���C���[��������ɂ��邩�m�F���A�t���O���X�V
    public bool CheckAndSetPlayerInView(Transform enemy)
    {
        bool isInFrontView = IsPlayerInFieldOfView(enemy, fieldOfViewAngle, detectionDistance);
        return isInFrontView; // ����͑O������݂̂𔻒�
    }

    // �O������̃`�F�b�N
    public bool IsPlayerInFieldOfView(Transform enemy, float angle, float distance)
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);
        //Debug.Log("����: " + distanceToPlayer); // �f�o�b�O�p���O

        if (distanceToPlayer > distance) return false;

        Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        float angleToPlayer = Vector3.Angle(enemy.forward, directionToPlayer);
        //Debug.Log("�p�x: " + angleToPlayer); // �f�o�b�O�p���O

        return angleToPlayer <= angle / 2;
    }
}
