using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;         // 敵の移動速度
    public Transform player;             // プレイヤーのTransform
    public float gridSize = 1f;          // グリッドのサイズ
    public float raycastDistance = 1.0f; // Raycastの距離
    public LayerMask obstacleLayer;      // 障害物のレイヤーマスク

    private Vector2 targetPosition;      // 敵が次に移動する位置
    private List<Vector2> path = new List<Vector2>(); // A*経路探索の結果

    Rigidbody2D rb;
    void Start()
    {
        obstacleLayer = LayerMask.GetMask("Obstacles");
        rb = GetComponent<Rigidbody2D>();
        // プレイヤーを探す
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // 初期位置をグリッドにスナップ
        targetPosition = SnapToGrid(transform.position);
    }

    void Update()
    {
        // A*アルゴリズムを使用してプレイヤーへの最短経路を計算
        path = FindPath(transform.position, player.position);

        if (path != null && path.Count > 0)
        {
            // 経路に従って移動
            targetPosition = path[0];  // 次のターゲット位置を経路の先頭に設定
            path.RemoveAt(0);          // 経路の先頭を削除
        }

        // ターゲット位置に向かって移動
        MoveToTarget();
    }

    // ターゲット位置に向かって移動
    void MoveToTarget()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        }
    }

    // グリッドにスナップする
    Vector2 SnapToGrid(Vector2 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector2(x, y);
    }

    // A*アルゴリズムで経路を探索する関数
    List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        // 開始地点とゴール地点をグリッドにスナップ
        start = SnapToGrid(start);
        target = SnapToGrid(target);

        // 探索に必要なリストを定義
        List<Node> openList = new List<Node>();  // 未探索ノード
        HashSet<Vector2> closedList = new HashSet<Vector2>();  // 探索済みノードを位置ベースで保存

        // 開始ノードを作成してopenListに追加
        Node startNode = new Node(start, null, 0, GetDistance(start, target));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // F値が最小のノードを取得
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F || (openList[i].F == currentNode.F && openList[i].H < currentNode.H))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode.Position);  // 座標をclosedListに追加

            // ゴールに到達した場合、経路を返す
            if (currentNode.Position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            // 周囲のノードを探索
            foreach (Vector2 direction in GetNeighbours())
            {
                Vector2 neighbourPos = currentNode.Position + direction;

                // 障害物があるか、探索済みの位置かどうかチェック
                if (IsBlocked(neighbourPos) || closedList.Contains(neighbourPos))
                {
                    continue;
                }

                float newMovementCostToNeighbour = currentNode.G + GetDistance(currentNode.Position, neighbourPos);
                Node neighbourNode = new Node(neighbourPos, currentNode, newMovementCostToNeighbour, GetDistance(neighbourPos, target));

                // openListに同じ位置のノードがないか確認
                if (!openList.Exists(n => n.Position == neighbourPos) || newMovementCostToNeighbour < neighbourNode.G)
                {
                    openList.Add(neighbourNode);
                }
            }
        }

        return null; // 経路が見つからなかった場合
    }

    // 周囲の移動方向を返す
    List<Vector2> GetNeighbours()
    {
        return new List<Vector2>
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };
    }

    // 移動先に障害物があるかチェック
    bool IsBlocked(Vector2 position)
    {
        //Vector2 direction = targetPosition - (Vector2)transform.position;  // 目的地への方向を計算
        //// Raycastを使って障害物を検出
        //RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, raycastDistance, obstacleLayer);
        //// return hit.collider != null; // 障害物がある場合はtrueを返す

        //Debug.DrawRay(transform.position, direction * raycastDistance, Color.red);  // Rayの可視化

        //// 障害物が検知されたかどうか
        //if (hit.collider != null)
        //{
        //    return true;
        //}
        //return false;

        // 目的地への方向を計算
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();  // 正規化して方向ベクトルを単位ベクトルに

        // Raycastを発射し、障害物があるかどうか確認
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleLayer);

        // デバッグ用にRayを可視化
        Debug.DrawRay(transform.position, direction * raycastDistance, Color.red, 1.0f);  // 1秒間表示

        if (hit.collider != null)
        {
            Debug.Log("障害物が検知されました: " + hit.collider.name);  // 検知されたオブジェクトの名前をログに表示
            return true;  // 障害物を検知
        }

        return false;  // 障害物がない
    }

    // 経路をたどって戻る関数
    List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse(); // 経路を反転
        return path;
    }

    // 位置の差を取得
    float GetDistance(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    // ノードクラス（A*アルゴリズム用）
    class Node
    {
        public Vector2 Position;
        public Node Parent;
        public float G; // 開始点からのコスト
        public float H; // ゴールまでの推定コスト

        public float F { get { return G + H; } } // F値 = G + H

        public Node(Vector2 position, Node parent, float g, float h)
        {
            this.Position = position;
            this.Parent = parent;
            this.G = g;
            this.H = h;
        }

        // Vector2を基に比較する
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Node)) return false;
            return Position.Equals(((Node)obj).Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
