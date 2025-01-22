using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // 通常巡回ポイント
    public List<Transform> randomPositions; // ランダム巡回ポイント
    public float speed = 2f; // 移動速度
    public float randomPatrolChance = 0.3f; // ランダム巡回の確率
    public float fieldOfViewAngle = 90f; // 前方視野角
    public float detectionDistance = 5f; // プレイヤー検知距離
  
    public Transform player; // プレイヤーのTransform
    public Fov_script fovScript; // 視野制御用スクリプト

    private int currentTargetIndex = 0; // 現在の巡回ポイント
    private Transform randomTarget; // ランダム巡回のターゲット
    private bool isRandomPatrol = false; // 現在ランダム巡回中かどうか
    private Vector3 currentDirection; // 現在の移動方向
   
    private bool isStopped = false; // 停止中かどうか
    [SerializeField]
    private float stopDuration = 2f; // 停止時間
    private float stopTimer = 0f; // 停止タイマー

    [SerializeField]
    private Animator animator;

    private float detectionHoldTime = 0.2f; // プレイヤーを視認している状態を保持する時間
    private float detectionTimer = 0f; // 視認状態の保持タイマー

   
    bool isPlayerVisible;

    [SerializeField]
    private Transform reversePoint; // 巡回反転ポイント

    private void Update()
    {
        // 停止中の処理
        if (isStopped)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
                ResumePatrolling();
            }
            //Debug.Log($"停止中: isStopped={isStopped}, stopTimer={stopTimer}");
            return; // 停止中はそれ以上の処理を行わない
        }

        isPlayerVisible = fovScript != null && fovScript.IsPlayerInFieldOfView(player, transform, fieldOfViewAngle, detectionDistance);
        // 視認チェック
        if (isPlayerVisible)
        {
            StopAndDetectPlayer();
            return; // 視認した場合、他の処理を中断
        }
      

        // 巡回処理
        if (isRandomPatrol)
        {
            RandomPatrolMovement();
        }
        else
        {
            PatrolMovement();
        }
    }


    // 通常巡回処理
    private void PatrolMovement()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex]; // 次の巡回ポイント
        MoveTowards(target); // ターゲットに向かって移動

        // ターゲット地点に到達したら次の地点に進む
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            //currentTargetIndex = (currentTargetIndex + 1) % positions.Count;

            // もしターゲットが reversePoint に一致するなら巡回ルートを反転
            if (reversePoint != null && target.position == reversePoint.position)
            {
                positions.Reverse(); // 巡回ルートを反転
                currentTargetIndex = 1; // 反転後の次のポイントへ
                Debug.Log("巡回ルートを反転！");
            }
            else
            {
                // 反転しない場合は通常の巡回を続行
                currentTargetIndex = (currentTargetIndex + 1) % positions.Count;
            }
        }
    }

    // ランダム巡回処理
    private void RandomPatrolMovement()
    {
        if (randomTarget == null) return;

        MoveTowards(randomTarget); // ランダムターゲットに向かって移動

        // ターゲット地点に到達したらランダム巡回終了
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            isRandomPatrol = false; // 通常巡回に戻る
            randomTarget = null;
        }
    }

    // ターゲットに向かって移動
    private void MoveTowards(Transform target)
    {
        // ターゲットへの方向を計算
        Vector3 direction = (target.position - transform.position).normalized;

        //// 視野の方向を更新
        if (fovScript != null)
        {
            fovScript.CurrentDirection = direction; // レイキャスト方向を更新
            fovScript.UpdateVisionDirection(direction);
        }
        // パトロール時に視野を更新
        if (fovScript != null)
        {
            fovScript.UpdateVisionDirection(direction); // 現在の移動方向を視野に反映
        }


        // ターゲットに向かって移動
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 現在の移動方向を更新
        UpdateAnimationDirection(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == reversePoint)
        {
           
            // 衝突時に何かしらの処理を実行したい場合は、ここに記述
            Debug.Log("巡回方向を逆転しました");
        }
    }
    // アニメーションの方向を更新
    private void UpdateAnimationDirection(Vector3 direction)
    {
        if (animator != null)
        {
            // X, Y方向の値をアニメーターに設定
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);

            // 移動中フラグを有効にする
            animator.SetBool("IsMoving", direction.magnitude > 0);
        }
    }

    // ランダム巡回を開始するか判定
    private void CheckRandomPatrolStart()
    {
        if (randomPositions.Count == 0 || isRandomPatrol) return;

        // 一定確率でランダム巡回を開始
        if (Random.value < randomPatrolChance)
        {
            randomTarget = randomPositions[Random.Range(0, randomPositions.Count)];
            isRandomPatrol = true;
        }
    }

    // プレイヤーを視認した際に停止処理を開始
    private void StopAndDetectPlayer()
    {
        isStopped = true; // 停止フラグを設定
        stopTimer = stopDuration; // 停止タイマーをリセット

        // Debug.Log($"停止処理を開始しました: isStopped={isStopped}, stopTimer={stopTimer}");
      
        // アニメーションを停止状態に設定
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
        Debug.Log($"停止: {stopDuration}秒");
    }

  
    // 停止終了後に巡回を再開
    private void ResumePatrolling()
    {
        Debug.Log("巡回を再開");
    }
}
