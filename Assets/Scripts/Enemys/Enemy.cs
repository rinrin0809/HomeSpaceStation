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
        if (player != null)
        {
            float step = speed * Time.deltaTime;
            float threshold = .1f;

            // Only calculate new position if we are under the "threshold"
            if (Vector2.Distance(transform.position, newPosition) < threshold)
            {
                newPosition = player.position - transform.position;

                if (Mathf.Abs(newPosition.x) > Mathf.Abs(newPosition.y))
                {
                    newPosition.x = player.position.x;
                    newPosition.y = transform.position.y;
                }
                else
                {
                    newPosition.x = transform.position.x;
                    newPosition.y = player.position.y;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, newPosition, step);
        }
    }
}

