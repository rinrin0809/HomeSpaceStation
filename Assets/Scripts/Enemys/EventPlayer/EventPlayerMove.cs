using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerMove : MonoBehaviour
{
    //public GameObject enemyPrefab; // 敵のプレハブ
    public Vector3 targetPosition; // プレイヤーの移動先
    public float moveSpeed = 5f;   // プレイヤーの移動速度
    private bool isHit = false;    // プレイヤーがオブジェクトに当たったかどうか



    void Update()
    {
        // プレイヤーをターゲットに向かって移動させる
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // プレイヤーがターゲット位置に向かって移動
        if (transform.position != targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized; // ターゲット方向
            transform.Translate(direction * moveSpeed * Time.deltaTime);  // 移動
        }
    }

}
 
