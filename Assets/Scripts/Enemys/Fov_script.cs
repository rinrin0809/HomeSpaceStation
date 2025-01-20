using System.Collections;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public Transform visionObject; // 視野オブジェクト
    public LayerMask obstacleLayer; // 障害物のレイヤーマスク
    public float fieldOfViewAngle = 70f; // 視野角
    public float detectionDistance = 5f; // 検知距離

    private Transform player; // プレイヤーのTransform
    internal Vector2 CurrentDirection;

    public float sightCounter = 0;
    public float sightThreshold = 3;

    [SerializeField]
    private GameOverFade game;

    private void Start()
    {
        // プレイヤーを取得（タグで検索）
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        // プレイヤーが視認できるかチェック
        bool canSeePlayer = IsPlayerInFieldOfView(player, transform, fieldOfViewAngle, detectionDistance);

        if (canSeePlayer)
        {
            // 視認中ならカウントを加算
            sightCounter += Time.deltaTime;
            Debug.Log($"視認中: カウント = {sightCounter}/{sightThreshold}");

            if (sightCounter >= sightThreshold)
            {
                game.gameObject.SetActive(true); // ゲームオーバーを発動
                Debug.Log("ゲームオーバーを発動！");
            }
        }
        else
        {
            // 視認範囲外ならカウントをリセット
            sightCounter = 0f;
            Debug.Log("視認範囲外: カウントリセット");
        }

        // デバッグ出力
        // Debug.Log($"プレイヤー視認状態: {canSeePlayer}");
    }

    public bool IsPlayerInFieldOfView(Transform player, Transform enemy, float fieldOfViewAngle, float detectionDistance)
    {
        // プレイヤーまでの方向を計算
        Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        // 距離が検知範囲外なら false
        if (distanceToPlayer > detectionDistance)
        {
            return false;
        }

        // 敵の視野方向（移動方向）を取得
        Vector3 forward = CurrentDirection; // Fov_script の CurrentDirection を使用

        // 視野角を判定
        float angleToPlayer = Vector3.Angle(forward, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2)
        {
            return false;
        }

        // デバッグ用レイキャスト（視野方向）
        Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // 視野方向
        Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.red); // プレイヤー方向

        // レイキャストで障害物をチェック
        RaycastHit2D hit = Physics2D.Raycast(enemy.position, directionToPlayer, distanceToPlayer);
        if (hit.collider != null && hit.collider.transform != player)
        {
           // Debug.Log($"障害物で視認不可: {hit.collider.name}");
            Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.blue); // プレイヤー方向
            return false;
        }

        //Debug.Log("プレイヤーを視認しました！");
        //if (game != null)
        //{
        //    game.gameObject.SetActive(true);
        //}
       
        return true;
    }

    

    public void UpdateVisionDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.1f) // 有効な方向のみ更新
        {
            CurrentDirection = direction.normalized; // 移動方向を更新
        }
        //if (direction.magnitude > 0.1f) // 有効な方向がある場合のみ更新
        //{
        //    // 現在の方向を保存
        //    CurrentDirection = direction;

        //    // 視野オブジェクトを回転
        //    //if (visionObject != null)
        //    //{
        //    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //    //    visionObject.rotation = Quaternion.Euler(0, 0, angle);
        //    //}

        //    // 敵本体の回転を更新
        //    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //    transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        //    // デバッグ用ログ
        //    Debug.Log($"視野と回転更新: 移動方向={direction}, 回転角度={targetAngle}");
        //}
    }
}
