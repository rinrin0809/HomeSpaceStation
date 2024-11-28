using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyMove : MonoBehaviour
{
    public Transform player;  // �v���C���[��Transform
    public float moveSpeed = 3f;  // �G�̈ړ����x
    private bool isChasing = false;  // �ǐՃt���O

    void Update()
    {
       
            // �v���C���[�̈ʒu�Ɍ������ēG���ړ�
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        
    }

    // �ǐՂ��J�n���郁�\�b�h
    public void StartChasing()
    {
        isChasing = true;
    }
}
