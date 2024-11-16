using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // �Ǐ]����Ώۂ����߂�ϐ�
    public GameObject target;

    //����
    public GameObject Enemy;

    // �J�����̏����ʒu���L�����邽�߂̕ϐ�
    Vector3 pos;

    // �J�����ړ����x
    public float cameraSpeed = 0.5f;

    //�C�x���g
    public EventData Event;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����̏����ʒu��ϐ�pos�ɓ����
        pos = Camera.main.gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Event.GetNameEventActionFlg("���ʂ̏��o��"))
        {
            //���ʂ̏��o�ꎞ�̃J�������o
            LockOnEnemyCamera();
        }

        else if(!Event.GetNameEventActionFlg("���ʂ̏��o��") && !Event.GetNameEventFlg("���ʂ̏��o��"))
        {
            //�v���C���[�ɃJ������Ǐ]����
            LockOnPlayerCamera();
        }

        //for (int i = 0; i < Event.GetEvents().Length; i++)
        //{
        //    //�C�x���g�̎��͒Ǐ]���Ȃ�
        //    if(!Event.GetEvents()[i].EventFlag)
        //    {
               
        //    }
        //}
    }

    //�v���C���[�ɃJ������Ǐ]����
    public void LockOnPlayerCamera()
    {
        // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������
        Vector3 cameraPos = target.transform.position;

        // �����Ώۂ̏c�ʒu��0���傫���ꍇ
        if (target.transform.position.y > 0)
        {
            // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
            cameraPos.y = target.transform.position.y;
        }

        // �J�����̉��s���̈ʒu��-10������
        cameraPos.z = -10;
        //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������
        Camera.main.gameObject.transform.position = cameraPos;
    }

    //���ʂ̏��o�ꎞ�̃J�������o
    private void LockOnEnemyCamera()
    {
        // ���݂̃J�����ʒu
        Vector3 currentCameraPos = Camera.main.gameObject.transform.position;

        // �ڕW�ʒu�i���ʂ̏����O���j
        Vector3 targetCameraPos = Enemy.transform.position;
        targetCameraPos.z = -10; // �J�����̉��s����ݒ�

        // �J���������X�ɂ��ʂɈړ�������
        Vector3 smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);

        ////��b�̔ԍ���currentCameraPos��targetCameraPos�𔽓]�ɂ���
        //if()
        //{

        //}

        //else
        //{

        //}
        Camera.main.gameObject.transform.position = smoothedPosition;
    }
}
