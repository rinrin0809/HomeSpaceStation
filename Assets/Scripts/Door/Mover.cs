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
    private bool isNearLimit = false; // ���~�b�g�ɋ߂Â��Ă��邩�ǂ����̃t���O

    public bool isMoving = false; // �X�y�[�X�L�[�������ꂽ�Ƃ������������t���O

    private float customTime = 0f; // �����ŊǗ����鎞��

    // Start�͏���������
    void Start()
    {
        // �V�[���J�ڌ���I�u�W�F�N�g��ێ�
        DontDestroyOnLoad(gameObject);

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
            if (!isNearLimit)
            {
                isNearLimit = true; // ���~�b�g�ɋ߂Â����Ƃ��Ƀt���O�𗧂Ă�
                customTime = 0f; // �J�X�^���^�C�}�[�����Z�b�g
            }

            // ���~�b�g�ɓ��B�����ꍇ�A�ʒu���X���[�Y�Ƀ��~�b�g�ɍ��킹��
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // �ʒu�����~�b�g�Ɍ������ď��X�ɕύX
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(newX, newY, currentPos.z), moveSpeed * 0.033f);

            // ���~�b�g�ɓ��B�����t���O���Z�b�g
            isAtLimit = true;
        }
        else
        {
            isNearLimit = false; // ���~�b�g�𗣂ꂽ��t���O�����Z�b�g
        }

        // ���~�b�g�ɋ߂Â�����A�J�X�^���^�C�}�[�𑝉�������
        if (isAtLimit)
        {
            customTime += Time.deltaTime; // �蓮�Ŏ��Ԃ𑝉�������
        }

        // �����𔽓]������^�C�~���O
        if (customTime >= timeToChangeDirection && isAtLimit)
        {
            // �����𔽓]
            moveDirection = -moveDirection;
            // �^�C�}�[�����Z�b�g
            customTime = 0f;

            isAtLimit = false;
        }

        // ���~�b�g�������ŃX�y�[�X�L�[��������Ă����Ȃ�
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false; // �ړ����~
        }

        // �ړ��������Ăяo��
        Move();
    }

    // �V�[���J�ڑO�ɏ�Ԃ�ۑ�
    void OnApplicationQuit()
    {
        // ���݂̏�Ԃ�ۑ�
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
        PlayerPrefs.SetFloat("MoveDirectionX", moveDirection.x);
        PlayerPrefs.SetFloat("MoveDirectionY", moveDirection.y);
        PlayerPrefs.Save();
    }
}
