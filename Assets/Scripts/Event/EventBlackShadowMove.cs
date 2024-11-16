using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlackShadowMove : MonoBehaviour
{
    //�C�x���g�̃f�[�^
    public EventData Event;
    //�����e�̈ʒu
    private Transform GetTransform;
    //�ړ��ʒu
    private Vector3 MovePos;
    // �����e�̈ړ����x
    public float speed = 7f; 

    // Start is called before the first frame update
    void Start()
    {
        GetTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�����
        Move();
    }

    //�ړ�����
    private void Move()
    {
        if(Event.GetNameEventActionFlg("�����e��������"))
        {
            //x�����Œ�
            MovePos.x = GetTransform.position.x;
            //Y���W�����ړ�
            MovePos.y += speed * Time.deltaTime;
            //Transform�ɑ��
            GetTransform.position = MovePos;
        }
    }
}
