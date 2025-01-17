using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fieldOfViewAngle = 90f; // 前方視野角
    public float detectionDistance = 10f; // プレイヤー検知距離
    public Transform player; // プレイヤーのTransform

    private bool isPlayerInView = false; // プレイヤーが視野内にいるかどうか
    private Rigidbody enemyRigidbody; // 敵のRigidbody
    public float stopDuration = 2f; // 停止する時間
    private float stopTimer = 0f; // 停止時間を計測するタイマー

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>(); // Rigidbodyを取得
    }

    void Update()
    {
        bool wasInView = isPlayerInView;
        isPlayerInView = CheckAndSetPlayerInView(transform);

        if (isPlayerInView && !wasInView)
        {
            Debug.Log("プレイヤーを視認しました！停止します。");
            StopEnemy();
        }

        // 停止時間の管理
        if (stopTimer > 0f)
        {
            stopTimer -= Time.deltaTime;
            Debug.Log("停止中: 残り時間 " + stopTimer);
            if (stopTimer <= 0f)
            {
                stopTimer = 0f;  // 念のためゼロにリセット
                ResumeEnemy();
            }
        }
        else
        {
            Debug.Log("停止していません");
        }
    }

    private void StopEnemy()
    {
        // 敵の動きを停止する
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector3.zero; // 速度をゼロに
            enemyRigidbody.angularVelocity = Vector3.zero; // 回転をゼロに
            enemyRigidbody.isKinematic = true;  // 物理挙動を停止
            Debug.Log("敵の動きが停止しました");
        }
        else
        {
            Debug.LogError("Rigidbodyがnullです！");
        }

        stopTimer = stopDuration; // 停止タイマーをリセット
    }

    private void ResumeEnemy()
    {
        // 敵の動作を再開
        enemyRigidbody.isKinematic = false;  // 物理挙動を再開
        Debug.Log("停止時間が終了しました。動作を再開します。");
    }

    // プレイヤーが視野内にいるか確認し、フラグを更新
    public bool CheckAndSetPlayerInView(Transform enemy)
    {
        bool isInFrontView = IsPlayerInFieldOfView(enemy, fieldOfViewAngle, detectionDistance);
        return isInFrontView; // 今回は前方視野のみを判定
    }

    // 前方視野のチェック
    public bool IsPlayerInFieldOfView(Transform enemy, float angle, float distance)
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);
        //Debug.Log("距離: " + distanceToPlayer); // デバッグ用ログ

        if (distanceToPlayer > distance) return false;

        Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        float angleToPlayer = Vector3.Angle(enemy.forward, directionToPlayer);
        //Debug.Log("角度: " + angleToPlayer); // デバッグ用ログ

        return angleToPlayer <= angle / 2;
    }
}
