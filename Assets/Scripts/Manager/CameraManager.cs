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

    //�C�x���g�O�̃J�����ʒu
    Vector3 originalCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����̏����ʒu��ϐ�pos�ɓ����
        pos = Camera.main.gameObject.transform.position;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Event != null)
        {
            //���ʂ̏��o�ꎞ�̃J�������o
            if (Event.GetNameEventActionFlg("���ʂ̏��o��"))
            {
                LockOnEnemyCamera(true);
            }

            //���ʂ̏��o�ꎞ�̃J�������o�i�^�[�Q�b�g���v���C���[�ɖ߂����j
            else if (!Event.GetNameEventActionFlg("���ʂ̏��o��") && Event.GetNameEventFlg("���ʂ̏��o��"))
            {
                LockOnEnemyCamera(false);
            }

            else if (!Event.GetNameEventActionFlg("���ʂ̏��o��") && !Event.GetNameEventFlg("���ʂ̏��o��"))
            {
                //�v���C���[�ɃJ������Ǐ]����
                LockOnPlayerCamera();
            }
        }
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
        //�Ǐ]���Ă��鎞�Ɉʒu��ۑ�����
        originalCameraPosition = cameraPos;

        //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������
        Camera.main.gameObject.transform.position = cameraPos;
    }

    //���ʂ̏��o�ꎞ�̃J�������o
    private void LockOnEnemyCamera(bool EventActionFlg)
    {
        // ���݂̃J�����ʒu
        Vector3 currentCameraPos = Camera.main.gameObject.transform.position;
        // �ڕW�ʒu�i���ʂ̏����O���j
        Vector3 targetCameraPos = new Vector3(0f, 0f, 0f);
        // �J���������X�ɂ��ʂɈړ�������
        Vector3 smoothedPosition = new Vector3(0f, 0f, 0f);

        //�C�x���g�ŉ�������i����̏ꍇ�̓J�����j�̓���������鎞
        if (EventActionFlg)
        {
            // �ڕW�ʒu�i���ʂ̏����O���j
            targetCameraPos = Enemy.transform.position;
            targetCameraPos.z = -10; // �J�����̉��s����ݒ�
            // �J���������X�ɂ��ʂɈړ�������
            smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);

            Camera.main.gameObject.transform.position = smoothedPosition;
        }

        else
        {
            // �ڕW�ʒu�i���݂̃J�����ʒu�ɖ߂��j
            targetCameraPos = originalCameraPosition; // ���̃J�����ʒu��ێ������ϐ�
            // �J���������X�Ɍ��̈ʒu�ɖ߂�
            smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);
            // �J�����ʒu���X�V
            Camera.main.gameObject.transform.position = smoothedPosition;
        }
    }

    private Vector3 LockOnCamera(GameObject Start, GameObject Target)
    {
        // ���݂̃J�����ʒu
        Vector3 currentCameraPos = Start.transform.position;
        // �ڕW�ʒu�i���ʂ̏����O���j
        Vector3 targetCameraPos = Target.transform.position;
        // �J�����̉��s����ݒ�
        targetCameraPos.z = -10;
        // �J���������X�ɂ��ʂɈړ�������
        Vector3 smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);

        return smoothedPosition;
    }
}