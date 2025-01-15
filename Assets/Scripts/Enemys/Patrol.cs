using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // 巡回ポイントのリスト
    public List<Transform> positions;

    // ランダム巡回用のポイントリスト
    public List<Transform> randomPositions;
    public float randomPatrolChance = 0.5f; // ランダム巡回を開始する確率
    public float speed = 2f; // 移動速度
    public float rotationSpeed = 180f; // 回転速度

    // プレイヤー関連設定
    public Transform player; // プレイヤーの位置
    public float fieldOfViewAngle = 90f; // 前方視野角
    public float detectionDistance = 5f; // プレイヤー検知距離
    public float behaviorResetTime = 3f; // 行動リセット時間
    public float lookAtDelay = 1f; // プレイヤーを向くまでの遅延
    public float backFieldOfViewAngle = 90f; // 後方視野角（未使用）
    public float backDetectionDistance = 3f; // 背後検知距離


    private int currentTargetIndex = 0; // 現在の巡回ポイントインデックス
    private bool isRandomPatrol = false; // ランダム巡回中かどうか
    private Transform randomTarget; // ランダム巡回先のターゲット

    private bool isLookingAtPlayer = false; // プレイヤーを注視中かどうか
    private bool isPreparingToLookAtPlayer = false; // プレイヤーを注視準備中かどうか
    private Coroutine resetBehaviorCoroutine; // 行動リセット用コルーチン

    private bool isMovementStopped = false; // 動きが止まっているか
    private Transform returnPosition; // 元の位置（未使用）
    public Transform randomPatrolFagPosition; // ランダム巡回の開始地点
    public float detectionRange = 0.5f; // 巡回フラグ検知範囲

    public Vector3 CurrentDirection { get; private set; } // 現在の移動方向

    public Animator animator; // アニメーター参照



    void Update()
    {
        // プレイヤーを注視中の場合、その処理を優先
        if (isLookingAtPlayer)
        {
            LookPlayer();
        }
        else
        {
            CheckForBackDetection();


            // ランダム巡回中か、通常巡回かを選択
            if (isRandomPatrol)
            {
                MoveToRandomPosition();
            }
            else
            {
                CheckForRandomPatrolFag(); // ランダム巡回フラグのチェック
                MoveToPosition(); // 通常巡回
            }

            // プレイヤーが視野内にいるかをチェック
            CheckForPlayer();
        }
    }

    // プレイヤーが視野内にいるかチェック
    private void CheckForPlayer()
    {
        if (player == null || isPreparingToLookAtPlayer || isLookingAtPlayer) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionDistance) // 検知距離内か確認
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.up, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle / 2) // 視野内か確認
            {
                isPreparingToLookAtPlayer = true;
                StartCoroutine(DelayLookAtPlayer()); // プレイヤー注視準備開始
            }
        }
    }

    // プレイヤー注視を遅延させるコルーチン
    private IEnumerator DelayLookAtPlayer()
    {
        yield return new WaitForSeconds(lookAtDelay); // 遅延時間を待つ
        isPreparingToLookAtPlayer = false;
        isLookingAtPlayer = true;

        // 行動リセット用コルーチンを停止し、新しく開始
        if (resetBehaviorCoroutine != null)
        {
            StopCoroutine(resetBehaviorCoroutine);
        }
        resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
    }

    // プレイヤーを注視する処理
    private void LookPlayer()
    {
        if (player == null) return;

        // プレイヤーの方向を計算
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        SetAnimationParameters(directionToPlayer); // アニメーション更新

        // 敵をプレイヤー方向に回転
        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (resetBehaviorCoroutine == null)
        {
            resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
        }
    }

    // 行動リセット用コルーチン
    private IEnumerator ResetBehaviorAfterDelay()
    {
        yield return new WaitForSeconds(behaviorResetTime); // リセット時間を待つ
        isLookingAtPlayer = false;
    }

    // ランダム巡回フラグをチェック
    private void CheckForRandomPatrolFag()
    {
        if (randomPatrolFagPosition == null) return;

        float distanceToFag = Vector3.Distance(transform.position, randomPatrolFagPosition.position);
        if (distanceToFag < detectionRange && randomPositions.Count > 0)
        {
            randomTarget = randomPositions[Random.Range(0, randomPositions.Count)]; // ランダムターゲットを設定
            isRandomPatrol = true;
        }
    }

    // 通常巡回の移動
    private void MoveToPosition()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex];
        MoveTowards(target);

        // ターゲット地点に到達したら次の地点へ
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % positions.Count;
        }
    }

    // ランダム巡回の移動
    private void MoveToRandomPosition()
    {
        if (randomTarget == null) return;

        MoveTowards(randomTarget);

        // ターゲット地点に到達したらランダム巡回終了
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            isRandomPatrol = false;
        }
    }

    // ターゲットに向かって移動
    private void MoveTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        CurrentDirection = direction; // 現在の移動方向を更新

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        SetAnimationParameters(direction); // アニメーション更新
    }

    private bool IsPlayerInBackField()
    {
        if (player == null) return false;

        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > backDetectionDistance) return false;

        // プレイヤーの方向を計算
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // 背後方向（-transform.up）との角度を計算
        float angleToPlayer = Vector3.Angle(-transform.up, directionToPlayer);

        // 背後の視野角内にいるか確認
        return angleToPlayer <= backFieldOfViewAngle / 2;
    }

    private void CheckForBackDetection()
    {
        if (IsPlayerInBackField())
        {
          
            // 必要に応じてアニメーションや追加の挙動をここで設定
            Debug.Log("振り向き判定: プレイヤーが背後にいます！");
        }
    }

    // プレイヤーが背後にいる場合、振り向く処理
    private IEnumerator TurnAround()
    {
        isMovementStopped = true;

        // 少し止まってから回転開始
        yield return new WaitForSeconds(0.5f);

        // プレイヤーの方向を計算
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // 回転の目標値を計算
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

        // 滑らかに回転する処理
        float timeToRotate = 1f; // 回転にかける時間（秒）
        float elapsedTime = 0f;

        while (elapsedTime < timeToRotate)
        {
            // 時間をかけて回転
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return null; // フレームごとに処理を続ける
        }

        // 最終的にターゲット回転に到達
        transform.rotation = targetRotation;

        // 少し待ってから動き再開
        yield return new WaitForSeconds(0.5f);
        isMovementStopped = false;
    }

    // アニメーションパラメータを設定
    private void SetAnimationParameters(Vector3 direction)
    {
        if (animator == null) return;

        if (direction.magnitude < 0.1f) // 移動していない場合
        {
            animator.SetBool("IsMoving", false);
            return;
        }

        animator.SetBool("IsMoving", true);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    // デバッグ用の視野角を描画
    private void OnDrawGizmos()
    {
        // 既存の視野角のデバッグ表示
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);


        //// 背後検知範囲のデバッグ表示
        //Gizmos.color = Color.red;
        //Vector3 back = -transform.up;
        //Vector3 backLeftBoundary = Quaternion.Euler(0, 0, backFieldOfViewAngle / 2) * back * backDetectionDistance;
        //Vector3 backRightBoundary = Quaternion.Euler(0, 0, -backFieldOfViewAngle / 2) * back * backDetectionDistance;

        //Gizmos.DrawLine(transform.position, transform.position + backLeftBoundary);
        //Gizmos.DrawLine(transform.position, transform.position + backRightBoundary);
    }
}
