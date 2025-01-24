using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPosition : MonoBehaviour
{
    public GameObject OpenDoor;
    private bool isPlayerInRangeOpenDoor = false;
    public Mover move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���R���C�_�ɓ������Ƃ�
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = true;
            Debug.Log("�v���C���[���R���C�_�ɓ������I");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �v���C���[���R���C�_����o���Ƃ�
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = false;
            Debug.Log("�v���C���[���R���C�_����o���I");
        }
    }

    // Update is called once per frame
    #region �A�łł���
    /*void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�M�~�b�N���A�N�e�B�u�ɂ��܂��B");
            if(move != null)
            {
                move.isMoving = true;
            }
        }
    }*/
    #endregion

    #region ���Ԑ����Ő���
    public bool hasPressedSpace = false; // Space�L�[�������ꂽ���ǂ����̃t���O
    private float timeBetweenInputs = 5.0f; // ���͂��󂯕t����܂ł̑ҋ@���ԁi�b�j
    public float time = 0.0f;
    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space) && !hasPressedSpace)
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�M�~�b�N���A�N�e�B�u�ɂ��܂��B");

            move.isMoving = true;

            time = 2.0f;

            // Space�L�[�������ꂽ��A�t���O�𗧂Ă�
            hasPressedSpace = true;

            // ��莞�Ԍ�Ƀt���O�����Z�b�g����R���[�`�����J�n
            StartCoroutine(WaitForInputCooldown());
        }

        if (hasPressedSpace && time < 0)
        {
            move.isMoving = false;
        }
        time -= Time.deltaTime;
    }

    // ��莞�ԑҋ@���Ă���ēx���͂��󂯕t����
    private IEnumerator WaitForInputCooldown()
    {
        yield return new WaitForSeconds(timeBetweenInputs); // �w�肵���b���ҋ@

        // ���Ԃ��o�߂�����t���O�����Z�b�g���čēx���͂��󂯕t����
        hasPressedSpace = false;
    }
    #endregion

    #region �t���[���Ǘ�
    /*private bool hasPressedSpace = false; // Space�L�[�������ꂽ���ǂ����̃t���O

    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space) && !hasPressedSpace)
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�M�~�b�N���A�N�e�B�u�ɂ��܂��B");

            if (move != null)
            {
                move.isMoving = true;
            }

            // Space�L�[�������ꂽ��A�t���O�𗧂Ă�
            hasPressedSpace = true;

            // �t���O�����Z�b�g���邽�߂�1�t���[����ɍĂуt���O��߂�
            StartCoroutine(ResetSpacePressFlag());
        }
    }

    // 1�t���[����Ƀt���O�����Z�b�g����R���[�`��
    private IEnumerator ResetSpacePressFlag()
    {
        yield return null; // 1�t���[���ҋ@
        hasPressedSpace = false; // 1�t���[����Ƀt���O��߂��ĘA�ł�h��
    }*/
    #endregion
}
