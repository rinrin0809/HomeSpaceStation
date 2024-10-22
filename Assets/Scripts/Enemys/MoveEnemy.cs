using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Grid grid; // グリッドへの参照
    public float speed = 5f; // 敵の移動速度
    private int targetIndex = 0; // 現在のターゲットノードのインデックス
    private List<Node> path; // 現在の追跡経路

    private void Start()
    {
        // 初期パスの設定
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
    }

    private void Update()
    {
        // パスが存在するかを確認
        if (path != null && path.Count > 0)
        {
            // 現在のターゲットノードに向かって移動
            Vector2 targetPosition = path[targetIndex].Position;
            Vector2 currentPosition = transform.position;

            // ターゲットノードへの移動
            Vector2 direction = (targetPosition - currentPosition).normalized;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            // ノードに到達したら次のノードへ移動
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++; // 次のノードへ

                // 最後のノードに到達したら追跡を終了するかループを繰り返す
                if (targetIndex >= path.Count)
                {
                    targetIndex = 0; // ループして再び最初のノードへ
                }
            }
        }
        else
        {
            Debug.Log("No path to follow.");
        }
    }

    // 外部からパスを更新するメソッド
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // パスの最初から再スタート
    }
}
