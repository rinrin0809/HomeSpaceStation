/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // �E�Ɉړ�
        Left   // ���Ɉړ�
    }

    public DirectionType direction = DirectionType.Right; // �e�I�u�W�F�N�g���Ƃ̕����ݒ�
    public float moveSpeed = 3f; // �ړ����x
    public float leftLimit = -5f; // ���̈ړ�����
    public float rightLimit = 5f; // �E�̈ړ�����
    public float time = 3.0f; // ������ς���܂ł̎���
    public List<GameObject> targetObjects; // �ړ����������I�u�W�F�N�g�̃��X�g

    private Vector3 moveDirection; // �ړ�����

    // Start is called before the first frame update
    void Start()
    {
        // targetObjects�ɐݒ肳�ꂽ�I�u�W�F�N�g���Ƃɕ���������
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject == null) continue;

            // DirectionType�Ɋ�Â��Ĉړ�������ݒ�
            Door targetDoor = targetObject.GetComponent<Door>(); // Door �X�N���v�g���擾
            if (targetDoor != null)
            {
                // �e�I�u�W�F�N�g���Ƃɕ�����ݒ�
                if (targetDoor.direction == DirectionType.Right)
                {
                    targetDoor.moveDirection = new Vector3(1f, 0f, 0f); // �E�Ɉړ�
                }
                else
                {
                    targetDoor.moveDirection = new Vector3(-1f, 0f, 0f); // ���Ɉړ�
                }
            }
            else
            {
                Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g��Door�X�N���v�g���A�^�b�`����Ă��܂���: " + targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject == null) continue;

            Door targetDoor = targetObject.GetComponent<Door>(); // Door �X�N���v�g���擾
            if (targetDoor == null) continue;

            // ���݂̈ʒu���擾
            float currentX = targetObject.transform.position.x;

            // �ړ��̌��E���`�F�b�N���āA�I�u�W�F�N�g�����~�b�g�𒴂��Ȃ��悤�ɐ���
            if (currentX <= leftLimit || currentX >= rightLimit)
            {
                // ���~�b�g�ɓ��B�����ꍇ�A�ʒu�����~�b�g�ɍ��킹��
                targetObject.transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), targetObject.transform.position.y, targetObject.transform.position.z);

                // time��0��菬�����Ȃ�Ȃ��悤�ɏ���
                if (time <= 0.0f)
                {
                    // �����𔽓]
                    targetDoor.moveDirection = -targetDoor.moveDirection;
                    time = 3.0f;  // time�����Z�b�g���āA�ēx���]�܂őҋ@
                }

                time -= Time.deltaTime; // time������������
            }

            // �I�u�W�F�N�g���ړ�
            targetObject.transform.Translate(targetDoor.moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // �E�Ɉړ�
        Left   // ���Ɉړ�
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject; // �ΏۃI�u�W�F�N�g
        public DirectionType direction; // �ړ�����
    }

    public List<DoorObject> doorObjects; // �ړ����������I�u�W�F�N�g�Ƃ��̕�����ݒ肷�郊�X�g

    public float moveSpeed = 3f; // �ړ����x
    public float leftLimit = -5f; // ���̈ړ�����
    public float rightLimit = 5f; // �E�̈ړ�����
    public float time = 3.0f; // ������ς���܂ł̎���

    private void Start()
    {
        // doorObjects �̐ݒ�Ɋ�Â��Ċe�I�u�W�F�N�g�̈ړ�����������
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // DirectionType �Ɋ�Â��Ĉړ�������ݒ�
            Door targetDoor = doorObject.targetObject.GetComponent<Door>(); // Door �X�N���v�g���擾
            if (targetDoor != null) // Door �X�N���v�g���A�^�b�`����Ă��邩�`�F�b�N
            {
                if (doorObject.direction == DirectionType.Right)
                {
                    targetDoor.SetMoveDirection(new Vector3(1f, 0f, 0f)); // �E�Ɉړ�
                }
                else
                {
                    targetDoor.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // ���Ɉړ�
                }
            }
            else
            {
                Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g��Door�X�N���v�g���A�^�b�`����Ă��܂���: " + doorObject.targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // �����ɉ������ړ�����
            Door targetDoor = doorObject.targetObject.GetComponent<Door>();
            if (targetDoor != null)
            {
                targetDoor.Move();
            }
        }
    }

    // �ړ�������ݒ肷�郁�\�b�h
    public void SetMoveDirection(Vector3 direction)
    {
        this.moveDirection = direction;
    }

    // �I�u�W�F�N�g���ړ����郁�\�b�h
    private Vector3 moveDirection; // �ړ�����

    public void Move()
    {
        // ���݂̈ʒu���擾
        float currentX = transform.position.x;

        // �ړ��̌��E���`�F�b�N���āA�I�u�W�F�N�g�����~�b�g�𒴂��Ȃ��悤�ɐ���
        if (currentX <= leftLimit || currentX >= rightLimit)
        {
            // ���~�b�g�ɓ��B�����ꍇ�A�ʒu�����~�b�g�ɍ��킹��
            transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), transform.position.y, transform.position.z);

            // time��0��菬�����Ȃ�Ȃ��悤�ɏ���
            if (time <= 0.0f)
            {
                // �����𔽓]
                moveDirection = -moveDirection;
                time = 3.0f;  // time�����Z�b�g���āA�ēx���]�܂őҋ@
            }

            time -= Time.deltaTime; // time������������
        }

        // �I�u�W�F�N�g���ړ�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // �E�Ɉړ�
        Left   // ���Ɉړ�
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject; // �ΏۃI�u�W�F�N�g
        public DirectionType direction; // �ړ�����
    }

    public List<DoorObject> doorObjects; // �ړ����������I�u�W�F�N�g�Ƃ��̕�����ݒ肷�郊�X�g

    private void Start()
    {
        // doorObjects �̐ݒ�Ɋ�Â��Ċe�I�u�W�F�N�g�̈ړ�����������
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // Mover �X�N���v�g���擾���Ĉړ�������ݒ�
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            if (mover != null)
            {
                if (doorObject.direction == DirectionType.Right)
                {
                    mover.SetMoveDirection(new Vector3(1f, 0f, 0f)); // �E�Ɉړ�
                }
                else
                {
                    mover.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // ���Ɉړ�
                }
            }
            else
            {
                Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g��Mover�X�N���v�g���A�^�b�`����Ă��܂���: " + doorObject.targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // Mover �X�N���v�g���擾���Ĉړ������s
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            if (mover != null)
            {
                mover.Move();
            }
        }
    }
}
