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
    public float timeToChangeDirection = 3.0f; // ������ς���܂ł̎���
    private Vector3 moveDirection; // �ړ�����

    private bool isAtLimit = false; // ���~�b�g�ɓ��B�������ǂ����̃t���O
    private float remainingTime; // �������]�܂ł̎c�莞��
    private bool isNearLimit = false; // ���~�b�g�ɋ߂Â��Ă��邩�ǂ����̃t���O

    public bool isMoving = false; // �X�y�[�X�L�[�������ꂽ�Ƃ������������t���O

    // Start�͏���������
    void Start()
    {
        remainingTime = timeToChangeDirection; // ������
        moveDirection = Vector3.right; // �����ړ������i�f�t�H���g�j
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
            if (!isNearLimit)
            {
                isNearLimit = true; // ���~�b�g�ɋ߂Â����Ƃ��Ƀt���O�𗧂Ă�
                remainingTime = timeToChangeDirection; // �c�莞�Ԃ����Z�b�g
            }

            // ���~�b�g�ɓ��B�����ꍇ�A�ʒu���X���[�Y�Ƀ��~�b�g�ɍ��킹��
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // �ʒu�����~�b�g�Ɍ������ď��X�ɕύX
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(newX, newY, currentPos.z), moveSpeed * Time.deltaTime);

            // ���~�b�g�ɓ��B�����t���O���Z�b�g
            isAtLimit = true;
        }
        else
        {
            isNearLimit = false; // ���~�b�g�𗣂ꂽ��t���O�����Z�b�g
        }

        // ���~�b�g�ɋ߂Â�����A���Ԃ�����������
        if (isAtLimit)
        {
            remainingTime -= Time.deltaTime;
        }

        // �����𔽓]������^�C�~���O
        if (remainingTime <= 0.0f && isAtLimit)
        {
            // �����𔽓]
            moveDirection = -moveDirection;
            // ���Ԃ����Z�b�g
            remainingTime = timeToChangeDirection;

            isAtLimit = false;
        }

        // ���~�b�g�������ŃX�y�[�X�L�[����������Ă����Ȃ�
        if (!isAtLimit && isMoving)
        {
            // �I�u�W�F�N�g���ړ�
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    // Update���\�b�h�ŃX�y�[�X�{�^�����͂�����
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true; // �ړ����J�n
        }

        // �X�y�[�X�{�^���������ꂽ�ꍇ
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false; // �ړ����~
        }

        // �ړ��������Ăяo��
        Move();
    }
}