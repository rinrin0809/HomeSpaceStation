//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AStarAI : MonoBehaviour
//{
//    public static Vector2[] CalculatePath(Vector2 start, Vector2 target)
//    {
//        Debug.Log($"Calculating path from {start} to {target}..."); // デバッグ用
//        List<Node> openList = new List<Node>();

//        HashSet<Node> closedList = new HashSet<Node>();

//        Node startNode = new Node(start);
//        Node targetNode = new Node(target);
//        openList.Add(startNode);

//        while (openList.Count > 0)
//        {
//            Node currentNode = openList[0];

//            // 目標に到達した場合
//            if (currentNode.Equals(targetNode))
//            {
//                return RetracePath(startNode, currentNode);
//            }

//            openList.Remove(currentNode);
//            closedList.Add(currentNode);

//            foreach (Node neighbor in GetNeighbors(currentNode))
//            {
//                if (closedList.Contains(neighbor))
//                    continue;

//                float newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
//                if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
//                {
//                    neighbor.gCost = newCostToNeighbor;
//                    neighbor.hCost = GetDistance(neighbor, targetNode);
//                    neighbor.parent = currentNode;

//                    if (!openList.Contains(neighbor))
//                        openList.Add(neighbor);
//                }
//            }
//        }

//        return null; // 経路が見つからなかった場合
//    }

//    private static float GetDistance(Node a, Node b)
//    {
//        return Vector2.Distance(a.position, b.position);
//    }

//    private static List<Node> GetNeighbors(Node node)
//    {
//        List<Node> neighbors = new List<Node>();
//        Debug.Log($"Getting neighbors for {node.position}..."); // デバッグ用

//        // 隣接ノードの追加（上、下、左、右）
//        neighbors.Add(new Node(node.position + new Vector2(1, 0))); // 右
//        neighbors.Add(new Node(node.position + new Vector2(-1, 0))); // 左
//        neighbors.Add(new Node(node.position + new Vector2(0, 1))); // 上
//        neighbors.Add(new Node(node.position + new Vector2(0, -1))); // 下

//        // ここで、通行可能なノードのみを追加する処理を実装

//        return neighbors;
//    }

//    private static Vector2[] RetracePath(Node startNode, Node endNode)
//    {
//        List<Vector2> path = new List<Vector2>();
//        Node currentNode = endNode;

//        while (currentNode != startNode)
//        {
//            path.Add(currentNode.position);
//            currentNode = currentNode.parent;
//        }

//        path.Reverse();
//        return path.ToArray();
//    }

//    private class Node
//    {
//        public Vector2 position;
//        public float gCost;
//        public float hCost;
//        public Node parent;

//        public Node(Vector2 pos)
//        {
//            position = pos;
//            gCost = float.MaxValue; // 初期コスト
//        }

//        public float fCost => gCost + hCost;

//        public override bool Equals(object obj)
//        {
//            return obj is Node node && position.Equals(node.position);
//        }

//        public override int GetHashCode()
//        {
//            return position.GetHashCode();
//        }
//    }


//    private static bool IsWalkable(Node node)
//    {
//        // 通行可能性のチェックロジックを実装
//        return true; // ここを適切な条件に変更
//    }

//}
