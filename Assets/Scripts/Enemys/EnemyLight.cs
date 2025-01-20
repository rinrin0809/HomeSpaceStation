using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRight : MonoBehaviour
{
    public Fov_script enemy; // 敵オブジェクトのスクリプトを参照
    public Transform lightTransform; // ライトのTransform
    public float rotationSpeed = 180f; // 回転速度（度/秒）

    void Update()
    {
        if (enemy != null)
        {
            RotateTowardsEnemyDirection();

            if (lightTransform != null)
            {
                AlignLightToDirection();
            }
        }
    }

    private void RotateTowardsEnemyDirection()
    {
        Vector2 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // 有効な移動方向がある場合のみ処理
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // スムーズにターゲット方向を向く
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AlignLightToDirection()
    {
        Vector2 direction = enemy.CurrentDirection;

        if (direction.magnitude > 0.1f) // 有効な移動方向がある場合のみ処理
        {
            // ライトの方向を敵の移動方向に一致させる
            lightTransform.up = direction;
        }
    }

    private void OnDrawGizmos()
    {
        if (lightTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(lightTransform.position, lightTransform.position + lightTransform.up * 2f);
        }
    }
}