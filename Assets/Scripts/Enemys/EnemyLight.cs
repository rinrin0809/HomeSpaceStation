using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRight : MonoBehaviour
{
    public Patrol enemy; // 敵オブジェクトのスクリプトを参照
    public float rotationSpeed = 180f; // 回転速度（度/秒）

    void Update()
    {
        if (enemy != null)
        {
            RotateTowardsEnemyDirection();
        }
    }

    private void RotateTowardsEnemyDirection()
    {
        // 敵の現在の移動方向を取得
        Vector3 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // 有効な移動方向がある場合のみ処理
        {
            // 回転先の角度を計算
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // スムーズにターゲット方向を向く
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}