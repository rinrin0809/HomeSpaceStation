using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //移動
    public List<Transform> positions; // 巡回ポイントのリスト
    public List<Transform> randomPositions; // ランダム移動先のリスト
    public float randomPatrolChance = 0.5f;
    public float speed = 2f; // 移動速度
    public float rotationSpeed = 180f; // 回転速度（度/秒）

    //振り返り
    public Transform player; // プレイヤーのTransform
    public float fieldOfViewAngle = 90f; // 視野角
    public float detectionDistance = 5f; // 振り返り動作の発動距離
    public float behaviorResetTime = 3f; // 振り返り後に元の行動に戻るまでの時間
    public float lookAtDelay = 1f; // 振り返る前の遅延時間

    //指定ポジション
    private int currentTargetIndex = 0; // 現在のターゲットポジションのインデックス
    private bool isRandomPatrol = false; // ランダムパトロール状態かどうか
    private Transform randomTarget; // 現在のランダムターゲット

    //視野
    private bool isLookingAtPlayer = false; // プレイヤーを見ている状態かどうか
    private bool isPreparingToLookAtPlayer = false; // 振り返る準備中かどうか
    private Coroutine resetBehaviorCoroutine;

    //
    private Transform returnPosition; // 一時的な復帰先
    public Transform randomPatrolFagPosition; // RandomPatrolFag の位置を設定
    public float detectionRange = 0.5f; // RandomPatrolFag に到達とみなす距離

    private int half = 2;
    void Update()
    {
        if (isLookingAtPlayer)
        {
            LookPlayer();
        }
        else
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
            //プレイヤーのチェック
            CheackForPlayer();
        }
    }

    private void CheackForPlayer()
    {
       if (player == null || isPreparingToLookAtPlayer || isLookingAtPlayer) return;

        //プレイヤーとの距離をチェック
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer<= detectionDistance)
        {
            //視野角内にいるかをチェック
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.up, directionToPlayer);

            //視野外なら
            if (angleToPlayer > fieldOfViewAngle / half)
            {
                Debug.Log("プレイヤーが条件を満たしました。振り返り準備を開始します。");
                isPreparingToLookAtPlayer = true;
                StartCoroutine(DelayLookAtPlayer());
            }
        }
    }

    private IEnumerator DelayLookAtPlayer()
    {
        yield return new WaitForSeconds(lookAtDelay);

        // 振り返り動作の開始
        Debug.Log("振り返ります。");
        isPreparingToLookAtPlayer = false;
        isLookingAtPlayer = true;

        // 振り返り後に元の行動に戻るためのタイマー開始
        if (resetBehaviorCoroutine != null)
        {
            StopCoroutine(resetBehaviorCoroutine);
        }
        resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
    }

    private void LookPlayer()
    {
        if (player == null) return;

        //プレイヤーの方向を向く
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ResetBehaviorAfterDelay()
    {
        yield return new WaitForSeconds(behaviorResetTime);
        Debug.Log("元の行動に戻ります。");

        isLookingAtPlayer = false;
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
