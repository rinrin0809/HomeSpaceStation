using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // 巡回ポイントのリスト
    public List<Transform> randomPositions; // ランダム移動先のリスト
    public float speed = 2f; // 移動速度
    public float rotationSpeed = 180f; // 回転速度（度/秒）

    public float randomPatrolChance = 0.5f;

    private int currentTargetIndex = 0; // 現在のターゲットポジションのインデックス
    private bool isRandomPatrol = false; // ランダムパトロール状態かどうか
    private Transform randomTarget; // 現在のランダムターゲット

    private Transform returnPosition; // 一時的な復帰先
    public Transform randomPatrolFagPosition; // RandomPatrolFag の位置を設定
    public float detectionRange = 0.5f; // RandomPatrolFag に到達とみなす距離
    void Update()
    {
       
        if (isRandomPatrol)
        {
            MoveToRandomPosition();
        }
        else
        {
            CheckForRandomPatrolFag();
            MoveToPosition();
        }
    }

    private void CheckForRandomPatrolFag()
    {
        if (randomPatrolFagPosition == null) return;

        // 距離を計算してチェック
        float distanceToFag = Vector3.Distance(transform.position, randomPatrolFagPosition.position);
        if (distanceToFag < detectionRange)
        {
            Debug.Log("RandomPatrolFag に到達しました");

            // ランダムパトロールへの切り替え
            if (randomPositions.Count > 0)
            {
                randomTarget = randomPositions[Random.Range(0, randomPositions.Count)];
                Debug.Log($"ランダムターゲット: {randomTarget.name} に移行します");

                isRandomPatrol = true;
            }
        }
    }

        private void MoveToPosition()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex];
        MoveAndRotateTowards(target);

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

    private void MoveToRandomPosition()
    {
        if (randomTarget == null) return;

        MoveAndRotateTowards(randomTarget);

        // ランダムターゲットに到達したかをチェック
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            Debug.Log("ランダムターゲットに到達しました");
            isRandomPatrol = false; // ランダムパトロール終了
        }
    }

    private void MoveAndRotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        // 移動
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // スムーズにターゲット方向を向く
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 90度オフセット
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
