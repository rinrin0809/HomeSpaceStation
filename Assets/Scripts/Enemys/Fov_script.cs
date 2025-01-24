using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fov_script : MonoBehaviour
{
    public Transform visionObject; // 視野オブジェクト
    public LayerMask obstacleLayer; // 障害物のレイヤーマスク
    public float fieldOfViewAngle = 70f; // 視野角
    public float detectionDistance = 5f; // 検知距離

    public GameObject player; // プレイヤーのTransform
    internal Vector2 CurrentDirection;

    public float sightCounter = 0;
    public float sightThreshold = 3;

    [SerializeField]
    private GameOverFade game;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = FindObjectOfType<Player>()?.gameObject;
        }

        if (player == null)
        {
            Debug.LogError("プレイヤーが見つからない！");
        }
        else
        {
            Debug.Log($"見つけたプレイヤー: {player.name} (ID: {player.GetInstanceID()})");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // プレイヤーが視認できるかチェック
        bool canSeePlayer = IsPlayerInFieldOfView(player.transform, transform, fieldOfViewAngle, detectionDistance);

        if (canSeePlayer)
        {
            // 視認中ならカウントを加算
            sightCounter += Time.deltaTime;
            //Debug.Log($"視認中: カウント = {sightCounter}/{sightThreshold}");

            if (sightCounter >= sightThreshold)
            {
                game.gameObject.SetActive(true); // ゲームオーバーを発動
                //Debug.Log("ゲームオーバーを発動！");
            }
        }
        else
        {
            // 視認範囲外ならカウントをリセット
            sightCounter = 0f;
            //Debug.Log("視認範囲外: カウントリセット");
        }

        // デバッグ出力
        // Debug.Log($"プレイヤー視認状態: {canSeePlayer}");
    }

    public bool IsPlayerInFieldOfView(Transform player, Transform enemy, float fieldOfViewAngle, float detectionDistance)
    {
        // プレイヤーまでの方向を計算
        // Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        // float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        // //float angle = Vector3.Angle(enemy.forward, directionToPlayer);
        //// Debug.Log($"視野角チェック: {angle}°");

        // // 距離が検知範囲外なら false
        // if (distanceToPlayer > detectionDistance)
        // {
        //     return false;
        // }

        // // 敵の視野方向（移動方向）を取得
        // Vector3 forward = CurrentDirection; // Fov_script の CurrentDirection を使用
        //// Debug.Log($"現在の視線方向: {CurrentDirection}");
        // // 視野角を判定
        // float angleToPlayer = Vector3.Angle(forward, directionToPlayer);
        // if (angleToPlayer > fieldOfViewAngle / 2)
        // {
        //     return false;
        // }

        // // デバッグ用レイキャスト（視野方向）
        // Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // 視野方向
        // Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.red); // プレイヤー方向

        // // レイキャストで障害物をチェック
        // Vector3 rayStart = enemy.position + Vector3.up * 0.5f; // ちょっと上から飛ばす
        // RaycastHit2D hit = Physics2D.Raycast(rayStart, directionToPlayer, distanceToPlayer);
        // Debug.Log($"directionToPlayer: {directionToPlayer}");

        // // デバッグログを追加
        // if (hit.collider != null)
        // {
        //     Debug.Log($"レイキャストヒット: {hit.collider.name}"); // 何に当たってるか
        // }
        // else
        // {
        //     Debug.Log("レイキャスト: 何にも当たらなかった"); // 何にも当たってないなら、別の原因かも？
        // }

        // if (hit.collider != null && hit.collider.transform != player)
        // {
        //    // Debug.Log($"障害物で視認不可: {hit.collider.name}");
        //     Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.blue); // プレイヤー方向
        //     return false;
        // }
        // //if (game != null)
        // //{
        // //    game.gameObject.SetActive(true);
        // //}

        // return true;

        Vector3 toPlayer = (player.position - enemy.position).normalized; // プレイヤーへの方向ベクトル

        // ここで forward を定義
        Vector3 forward = new Vector3(CurrentDirection.x, CurrentDirection.y, 0).normalized; // Z軸を固定して2Dに対応

        float angle = Vector3.Angle(forward, toPlayer); // forwardとtoPlayerの角度を測る

        // しきい値以下のX成分はゼロにする（ほぼ真上or真下なら完全な縦ベクトルに）
        if (Mathf.Abs(forward.x) < 0.1f) forward.x = 0f;

        // 同じく、ほぼ横向きならY成分をゼロにする
        if (Mathf.Abs(forward.y) < 0.1f) forward.y = 0f;

        // デバッグ用
        Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // 敵の正面
        Debug.DrawRay(enemy.position, toPlayer * detectionDistance, Color.red); // プレイヤー方向

        // 一定の角度内 & 一定距離内なら視認
        if (angle < fieldOfViewAngle * 0.5f && Vector3.Distance(enemy.position, player.position) <= detectionDistance)
        {
            return true;
        }

        return false;
    }

    

    public void UpdateVisionDirection(Vector3 direction)
    {

       
        if (direction.magnitude > 0.1f) // 有効な方向のみ更新
        {
            CurrentDirection = direction.normalized; // 移動方向を更新
        }

       // Debug.Log($"CurrentDirection: {CurrentDirection}");
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
