using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    public GameObject door;  // ���̃Q�[���I�u�W�F�N�g
    public Vector3 openPosition;  // �����J�����Ƃ��̈ʒu
    public float openSpeed = 2f;  // �����J�����x
    private bool isOpening = false;

    private SelectGimmick SG;  // Mover �N���X�ւ̎Q��

    // Start is called before the first frame update
    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door not assigned in the inspector!");
        }

        // Mover �R���|�[�l���g���擾
        SG = FindObjectOfType<SelectGimmick>();  // �V�[������ Mover ���擾
        if (SG == null)
        {
            Debug.LogError("Mover not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mover �� isMoving �t���O�� true �Ȃ�����J����
        if (SG != null && SG.Ans)
        {
            OpenDoor();
        }

        // �����J���鏈��
        if (isOpening)
        {
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * Time.deltaTime);
        }
    }

    // �����J���郁�\�b�h
    public void OpenDoor()
    {
        isOpening = true;
    }
}
