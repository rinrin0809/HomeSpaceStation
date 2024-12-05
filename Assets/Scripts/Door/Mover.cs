using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 3f;  // �ړ����x
    public float leftLimit = -5f; // ���̈ړ�����
    public float rightLimit = 5f; // �E�̈ړ�����
    public float time = 3.0f;     // ������ς���܂ł̎���
    private Vector3 moveDirection; // �ړ�����

    private bool isAtLimit = false; // ���~�b�g�ɓ��B�������ǂ����̃t���O

    // �ړ�������ݒ�
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    // �ړ�����
    public void Move()
    {
        // ���݂̈ʒu���擾
        float currentX = transform.position.x;

        // �ړ��̌��E���`�F�b�N���āA�I�u�W�F�N�g�����~�b�g�𒴂��Ȃ��悤�ɐ���
        if (currentX <= leftLimit || currentX >= rightLimit)
        {
            // ���~�b�g�ɓ��B�����ꍇ�A�ʒu�����~�b�g�ɍ��킹��
            transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), transform.position.y, transform.position.z);

            // ���~�b�g�ɓ��B�����t���O���Z�b�g
            isAtLimit = true;
        }
        else
        {
            // ���~�b�g�𒴂��Ȃ��ꍇ�́A�t���O�����Z�b�g
            isAtLimit = false;
        }

        // ���~�b�g�ɓ��B�����ꍇ�A�����𔽓]������
        if (isAtLimit && time <= 0.0f)
        {
            moveDirection = -moveDirection; // �����𔽓]
            time = 3.0f;  // time�����Z�b�g���āA�ēx���]�܂őҋ@
        }

        // time���������鏈��
        if (isAtLimit)
        {
            time -= Time.deltaTime; // ���~�b�g�ɓ��B���Ă���ԁAtime������
        }

        // �I�u�W�F�N�g���ړ�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
