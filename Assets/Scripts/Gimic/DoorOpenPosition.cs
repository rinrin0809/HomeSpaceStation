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
            move.isMoving = false;
            Debug.Log("�v���C���[���R���C�_����o���I");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�M�~�b�N���A�N�e�B�u�ɂ��܂��B");
            if(move != null)
            {
                move.isMoving = true;
            }
        }
    }
}
