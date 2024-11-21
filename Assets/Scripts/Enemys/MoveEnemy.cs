using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveEnemy : MonoBehaviour
{
    public Grid grid; // グリッドへの参照
    public float speed = 5f; // 敵の移動速度
    private int targetIndex = 0; // 現在のターゲットノードのインデックス
    private List<Node> path; // 現在の追跡経路
    public Transform playerTransform; // プレイヤーのTransform
    public float chaseRange = 10f; // 追跡を開始する範囲
    public float stopChaseDelay = 2f; // 追跡停止までの遅延時間（秒）

    private static bool _isChasing = false; // 追跡状態を保持するフラグ

    public EventData Event;

    // 追跡フラグのゲッター
    public static bool IsChasing
    {
        get => _isChasing;
        private set => _isChasing = value;
    }

    private float timeSincePlayerExitRange = 0f; // プレイヤーが範囲外に出てからの経過時間

    private void Start()
    {
        InitializePath();

        // シーン切り替え時に敵を消すためのイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // シーンがロードされるたびに呼ばれるイベントの解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //private void OnDisable()
    //{
    //    Destroy(gameObject); // 敵が無効化される際に削除する
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンが読み込まれた後に呼ばれる
        //DestroyEnemy();
    }

    private void Update()
    {
        //if (IsChasing)
        //{
        //    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        //    // 追跡停止条件
        //    if (distanceToPlayer > chaseRange)
        //    {
        //        timeSincePlayerExitRange += Time.deltaTime;

        //        if (timeSincePlayerExitRange >= stopChaseDelay)
        //        {
        //           // StopChasing(); // 追跡停止
        //        }
        //    }
        //    else
        //    {
        //        timeSincePlayerExitRange = 0f; // 範囲内ならタイマーをリセット
        //    }
        if(Event.GetNameEventFlg("黒い影が逃げる"))
        {
            FollowPath();
        }
       // }
    }

    private void InitializePath()
    {
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
    }

    private void FollowPath()
    {
        if (path == null || path.Count == 0) return;

        Vector2 targetPosition = path[targetIndex].Position;
        Vector2 currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;
            if (targetIndex >= path.Count) targetIndex = 0; // ループ
        }
    }

    public void StartChasing()
    {
        IsChasing = true;
        timeSincePlayerExitRange = 0f; // 追跡開始時にリセット
        Debug.Log("Enemy started chasing the player!");
    }

    public void StopChasing()
    {
        IsChasing = false;
        timeSincePlayerExitRange = 0f; // 追跡停止時にタイマーをリセット


        Debug.Log("Enemy stopped chasing the player.");
    }

    private void DestroyEnemy()
    {
        // 敵が存在する場合に削除c
        if (gameObject != null)
        {
            // 必要に応じて非アクティブ化
            Destroy(gameObject); // 敵を削除
            Debug.Log("Enemy destroyed after scene change.");
        }
    }

    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // パスの最初から再スタート
    }
}