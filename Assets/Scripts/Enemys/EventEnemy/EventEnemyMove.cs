using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyMove : MonoBehaviour
{
    public Transform player;  // プレイヤーのTransform
    public float moveSpeed = 3f;  // 敵の移動速度
    private bool isChasing = false;  // 追跡フラグ

    void Update()
    {
       
            // プレイヤーの位置に向かって敵が移動
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        
    }

    // 追跡を開始するメソッド
    public void StartChasing()
    {
        isChasing = true;
    }
}
