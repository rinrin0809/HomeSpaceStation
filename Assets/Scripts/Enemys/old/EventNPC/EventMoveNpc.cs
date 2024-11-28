using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveNpc : MonoBehaviour
{
    public List<Transform> targets; // 追跡するターゲットのリスト
    public float speed = 3f; // 移動速度
    private int currentTargetIndex = 0; // 現在のターゲットのインデックス

    void Update()
    {
        if (targets.Count == 0) return;

        // 現在のターゲットに向かって移動
        Transform target = targets[currentTargetIndex];

        // 目標方向へのベクトル
        Vector3 direction = (target.position - transform.position).normalized;

        // 移動
        transform.position += direction * speed * Time.deltaTime;

        // ターゲットに到達したら次のターゲットに移動
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Count; // 次のターゲットに切り替え
        }
    }
}
