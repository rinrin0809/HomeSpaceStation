using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 1f;  // �ړ����x
    public float leftLimit = -10000f; // ���̈ړ�����
    public float rightLimit = 10000f; // �E�̈ړ�����
    public float topLimit = 10000f;   // ��̈ړ�����
    public float bottomLimit = -10000f; // ���̈ړ�����
    public float timeToChangeDirection = 1.0f; // ������ς���܂ł̎���
    private Vector3 moveDirection; // �ړ�����

    public bool isAtLimit = false; // ���~�b�g�ɓ��B�������ǂ����̃t���O
    private float remainingTime; // �������]�܂ł̎c�莞��

    public bool RockFlg = false; //�J�M���������Ă��鎞�̃t���O
    public bool isMoving = false; // �X�y�[�X�L�[�������ꂽ�Ƃ������������t���O

    // Start�͏���������
    void Start()
    {
        isAtLimit = true;

        // PlayerPrefs�őO��̏�Ԃ𕜌�
        if (PlayerPrefs.HasKey("RemainingTime"))
        {
            remainingTime = PlayerPrefs.GetFloat("RemainingTime");
            moveDirection = new Vector3(PlayerPrefs.GetFloat("MoveDirectionX"),
                                         PlayerPrefs.GetFloat("MoveDirectionY"),
                                         0); // �ۑ�����Ă��������𕜌�
        }
        else
        {
            remainingTime = timeToChangeDirection; // �����l
            moveDirection = Vector3.right; // �����ړ�����
        }
    }

    // �ړ�������ݒ�
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    // �ړ�����
    public void Move()
    {
        // ���݂̈ʒu���擾
        Vector3 currentPos = transform.position;

        // ���~�b�g�ɋ߂Â����ꍇ�̏���
        if (currentPos.x <= leftLimit || currentPos.x >= rightLimit || currentPos.y <= bottomLimit || currentPos.y >= topLimit)
        {
            // ���~�b�g�ɓ��B�����ꍇ�A�ʒu�����~�b�g�ɍ��킹��
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // �ʒu�����~�b�g�ɍ��킹��
            transform.position = new Vector3(newX, newY, currentPos.z);

            // ���~�b�g�ɓ��B�����t���O���Z�b�g
            isAtLimit = true;
        }

        // �����𔽓]������^�C�~���O
        if (isAtLimit)
        {
            // �����𔽓]
            moveDirection = -moveDirection;

            isAtLimit = false;
        }

        // ���~�b�g�������ŃX�y�[�X�L�[��������Ă����Ȃ�
        if (!isAtLimit && isMoving)
        {
            // �I�u�W�F�N�g���ړ�
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
