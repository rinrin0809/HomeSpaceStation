using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform StartPosition;
    public Transform TargetPosition;
    //private bool pathFound = false;
    private MoveEnemy enemyMovement; // 敵の追従スクリプトを参照する変数

    private float pathUpdateInterval = 0.5f; // 経路更新の間隔
    private float timeSinceLastUpdate = 0f;
    private bool pathFound = false;

    private void Awake()
    {
        grid = GetComponent<Grid>();

        if (grid == null)
        {
           // Debug.LogError("Grid component not found on the GameObject.");
        }
    }
    private void Start()
    {
       
        enemyMovement = FindObjectOfType<MoveEnemy>(); // 敵の追従スクリプトを取得
        if (enemyMovement == null)
        {
           // Debug.LogError("EnemyFollowPath script not found in the scene.");
        }
    }
    private void Update()
    {
        
        // プレイヤーの位置を取得
        if (TargetPosition == null)
        {
           // Debug.LogError("TargetPosition is not set.");
            return;
        }

        timeSinceLastUpdate += Time.deltaTime;

        // 敵とプレイヤーの距離を計算
        float distanceToPlayer = Vector2.Distance(StartPosition.position, TargetPosition.position);

        // 一定の距離内でかつ、経路更新の間隔が経過した場合に経路を再計算
        if (distanceToPlayer < 20f && timeSinceLastUpdate >= pathUpdateInterval) // 10fは適宜調整
        {
            timeSinceLastUpdate = 0f; // タイマーをリセット
            FindPath(StartPosition.position, TargetPosition.position);

            // パスが見つかった場合は敵に通知
            if (grid.FinalPath.Count > 0)
            {
                enemyMovement.UpdatePath(grid.FinalPath);
            }
        }
    }

    private void UpdatePathToPlayer()
    {
        if (TargetPosition != null)
        {
            FindPath(transform.position, TargetPosition.position);
           // Debug.Log($"Finding path from {transform.position} to {TargetPosition.position}");
        }
    }

    private void FindPath(Vector2 a_Startpos, Vector2 a_Targetpos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_Startpos);
        Node TargetNode = grid.NodeFromWorldPosition(a_Targetpos);

       // Debug.Log($"Start Node: {StartNode.Position}, Target Node: {TargetNode.Position}"); // 追加

        if (TargetNode.Iswall)
        {
           // Debug.Log("Target node is a wall, finding nearest walkable node.");
            TargetNode = GetNearestWalkableNode(TargetNode);
        }

        // OpenListとClosedListの初期化
        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].Fcost < CurrentNode.Fcost ||
                    (OpenList[i].Fcost == CurrentNode.Fcost && OpenList[i].Hcost < CurrentNode.Hcost))
                {
                    CurrentNode = OpenList[i];
                }
            }

            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            // 目的のノードに到達した場合
            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
               return;
            }

            // 隣接ノードを取得
            foreach (Node NeighborNode in grid.GetNeighboringNode(CurrentNode))
            {
                //Debug.Log($"Checking neighbor node at: {NeighborNode.Position}, Is wall: {NeighborNode.Iswall}");
                if(NeighborNode.Iswall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }

                int MoveCost = CurrentNode.Gcost + GetManhattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.Gcost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.Gcost = MoveCost;
                    NeighborNode.Hcost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }

        //Debug.Log("Path not found, could not reach the target node.");

        //Debug.Log("Searching for path...");
    }


    private Node GetNearestWalkableNode(Node wallNode)
    {
        Node nearestNode = wallNode;

        foreach (Node neighbor in grid.GetNeighboringNode(wallNode))
        {
            if (!neighbor.Iswall)
            {
                nearestNode = neighbor;
                break; // 最初に見つかった歩けるノードを返す
            }
        }

        return nearestNode;
    }

    // 最終的なパスを確定させるメソッド
    void GetFinalPath(Node a_startNode, Node a_targetNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_targetNode;

        while (CurrentNode != a_startNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        FinalPath.Reverse();

        grid.FinalPath = FinalPath;

       // Debug.Log($"Final Path found: {FinalPath.Count} nodes");
        foreach (var node in FinalPath)
        {
          //  Debug.Log($"Node at: {node.Position}");
        }

        // 敵の経路を更新
        if (enemyMovement != null)
        {
            enemyMovement.UpdatePath(FinalPath);
        }
    }

    // マンハッタン距離を計算するメソッド
    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);
        return ix + iy;
    }
}

