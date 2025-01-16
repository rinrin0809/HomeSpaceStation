using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    public GameObject door;  // ���̃Q�[���I�u�W�F�N�g
    public Vector3 openPosition;  // �����J�����Ƃ��̈ʒu
    public float openSpeed = 2f;  // �����J�����x
    public bool isOpening = false;

    private SelectGimmick SG;  // SelectGimmick �N���X�ւ̎Q��

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        Debug.Log("�J�n�� " + door.transform.localPosition);
        if (door == null)
        {
            Debug.LogError("Door not assigned in the inspector!");
        }

        SG = FindObjectOfType<SelectGimmick>();  // �V�[������ SelectGimmick ���擾
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
        if (isOpening == true)
        {
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
        }
    }

    // �����J���郁�\�b�h
    public void OpenDoor()
    {
        isOpening = true;
    }
}
