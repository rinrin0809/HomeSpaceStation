using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right,  // �E�Ɉړ�
        Left,   // ���Ɉړ�
        Up,     // ��Ɉړ�
        Down    // ���Ɉړ�
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject;  // �ΏۃI�u�W�F�N�g
        public DirectionType direction;  // �ړ�����
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
            #region Mover
            if (mover != null)
            {
                switch (doorObject.direction)
                {
                    case DirectionType.Right:
                        mover.SetMoveDirection(new Vector3(1f, 0f, 0f)); // �E�Ɉړ�
                        break;
                    case DirectionType.Left:
                        mover.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // ���Ɉړ�
                        break;
                    case DirectionType.Up:
                        mover.SetMoveDirection(new Vector3(0f, 1f, 0f)); // ��Ɉړ�
                        break;
                    case DirectionType.Down:
                        mover.SetMoveDirection(new Vector3(0f, -1f, 0f)); // ���Ɉړ�
                        break;
                }
            }
            else
            {
                Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g��Mover�X�N���v�g���A�^�b�`����Ă��܂���: " + doorObject.targetObject.name);
            }
            #endregion
        }
    }

    // Update�͖��t���[���Ăяo�����
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            // Mover �X�N���v�g���擾���Ĉړ������s
            if (mover != null)
            {
                mover.Move();
            }
        }
    }
}