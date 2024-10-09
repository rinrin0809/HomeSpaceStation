using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;  // �v���C���[��Transform���C���X�y�N�^�[����w��
    public float speed = 2.0f; // �G�̈ړ����x

    //void Update()
    //{
    //    // �v���C���[�Ƃ̋������v�Z
    //    Vector3 direction = player.position - transform.position;

    //    // �΂߈ړ���h�����߂ɁAx�܂���y�̂ǂ��炩��D��
    //    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    //    {
    //        // x�����Ɉړ�
    //        if (direction.x > 0)
    //        {
    //            transform.position += Vector3.right * speed * Time.deltaTime; // �E�ړ�
    //        }
    //        else
    //        {
    //            transform.position += Vector3.left * speed * Time.deltaTime; // ���ړ�
    //        }
    //    }
    //    else
    //    {
    //        // y�����Ɉړ�
    //        if (direction.y > 0)
    //        {
    //            transform.position += Vector3.up * speed * Time.deltaTime; // ��ړ�
    //        }
    //        else
    //        {
    //            transform.position += Vector3.down * speed * Time.deltaTime; // ���ړ�
    //        }
    //    }
    //}

    private Vector2 newPosition;

    private void Awake()
    {
        newPosition = transform.position;
    }

    private void Update()
    {
        if (player == null) return;

        Vector2 endPosition = player.position;
        Vector2 delta = player.position - transform.position;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            // Y is the smaller delta => move only in Y
            // => keep your current X
            endPosition.x = transform.position.x;
        }
        else
        {
            // X is the smaller delta => move only in X
            // => keep your current Y
            endPosition.y = transform.position.y;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    }
}
