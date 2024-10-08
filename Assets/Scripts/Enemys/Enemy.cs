using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;  // プレイヤーのTransformをインスペクターから指定
    public float speed = 2.0f; // 敵の移動速度

    //void Update()
    //{
    //    // プレイヤーとの距離を計算
    //    Vector3 direction = player.position - transform.position;

    //    // 斜め移動を防ぐために、xまたはyのどちらかを優先
    //    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    //    {
    //        // x方向に移動
    //        if (direction.x > 0)
    //        {
    //            transform.position += Vector3.right * speed * Time.deltaTime; // 右移動
    //        }
    //        else
    //        {
    //            transform.position += Vector3.left * speed * Time.deltaTime; // 左移動
    //        }
    //    }
    //    else
    //    {
    //        // y方向に移動
    //        if (direction.y > 0)
    //        {
    //            transform.position += Vector3.up * speed * Time.deltaTime; // 上移動
    //        }
    //        else
    //        {
    //            transform.position += Vector3.down * speed * Time.deltaTime; // 下移動
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

