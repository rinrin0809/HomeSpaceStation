using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // 移動するポジションのリスト
    public float speed = 2f; // 移動速度
    public float rotationSpeed = 180f; // 回転速度（度/秒）
    private int currentTargetIndex = 0; // 現在のターゲットポジションのインデックス

    void Update()
    {
        MoveToPosition();
    }

    void MoveToPosition()
    {
        if (positions.Count == 0) return; // ポジションがない場合は何もしない

        Transform target = positions[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        // 移動
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // スムーズにターゲット方向を向く
        if (direction.magnitude > 0.1f) // ターゲットが近すぎない場合のみ回転
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 90度オフセット
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle); // ターゲットの回転
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 現在のターゲットに到達したかをチェック
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= positions.Count)
            {
                currentTargetIndex = 0; // リストの最初に戻る（巡回）
            }
        }
    }
}
