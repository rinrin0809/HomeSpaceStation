using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerMove : MonoBehaviour
{
    //public GameObject enemyPrefab; // �G�̃v���n�u
    public Vector3 targetPosition; // �v���C���[�̈ړ���
    public float moveSpeed = 5f;   // �v���C���[�̈ړ����x
    private bool isHit = false;    // �v���C���[���I�u�W�F�N�g�ɓ����������ǂ���



    void Update()
    {
        // �v���C���[���^�[�Q�b�g�Ɍ������Ĉړ�������
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // �v���C���[���^�[�Q�b�g�ʒu�Ɍ������Ĉړ�
        if (transform.position != targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized; // �^�[�Q�b�g����
            transform.Translate(direction * moveSpeed * Time.deltaTime);  // �ړ�
        }
    }

}
 
