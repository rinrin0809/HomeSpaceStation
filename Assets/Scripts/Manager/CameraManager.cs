using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // �Ǐ]����Ώۂ����߂�ϐ�
    public GameObject target;
    // �J�����̏����ʒu���L�����邽�߂̕ϐ�
    Vector3 pos;             

    // Start is called before the first frame update
    void Start()
    {
        //�J�����̏����ʒu��ϐ�pos�ɓ����
        pos = Camera.main.gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
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
}
