using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask WallMask;
    public Vector2 Gridworldsize; // 必要に応じて調整
    public float Noderadios;
    public float Distance;

    Node[,] grid;
    public List<Node> FinalPath;

    float Nodediameter;
    int gridsizeX, gridsizeY;

    private int Half = 2;
    private void Start()
    {
        Nodediameter = Noderadios * 2;
        gridsizeX = Mathf.RoundToInt(Gridworldsize.x / Nodediameter);
        gridsizeY = Mathf.RoundToInt(Gridworldsize.y / Nodediameter);
        CreateGrid();
    }

    public Node NodeFromWorldPosition(Vector2 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + Gridworldsize.x /Half) / Gridworldsize.x);
        float ypoint = ((a_WorldPosition.y + Gridworldsize.y / Half) / Gridworldsize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridsizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridsizeY - 1) * ypoint);

        // インデックスが範囲内であることを確認
        x = Mathf.Clamp(x, 0, gridsizeX - 1);
        y = Mathf.Clamp(y, 0, gridsizeY - 1);

        return grid[x, y];
    }

    public List<Node> GetNeighboringNode(Node a_node)
    {
        List<Node> NeighborgNodes = new List<Node>();
        int xCheck;
        int yCheck;

        // 右
        xCheck = a_node.gridX + 1;
        yCheck = a_node.gridY;
        if (xCheck >= 0 && xCheck < gridsizeX && !grid[xCheck, yCheck].Iswall)
        {
            NeighborgNodes.Add(grid[xCheck, yCheck]);
        }

        // 左
        xCheck = a_node.gridX - 1;
        yCheck = a_node.gridY;
        if (xCheck >= 0 && xCheck < gridsizeX && !grid[xCheck, yCheck].Iswall)
        {
            NeighborgNodes.Add(grid[xCheck, yCheck]);
        }

        // 上
        xCheck = a_node.gridX;
        yCheck = a_node.gridY + 1;
        if (yCheck >= 0 && yCheck < gridsizeY && !grid[xCheck, yCheck].Iswall)
        {
            NeighborgNodes.Add(grid[xCheck, yCheck]);
        }

        // 下
        xCheck = a_node.gridX;
        yCheck = a_node.gridY - 1;
        if (yCheck >= 0 && yCheck < gridsizeY && !grid[xCheck, yCheck].Iswall)
        {
            NeighborgNodes.Add(grid[xCheck, yCheck]);
        }

        return NeighborgNodes;
    }

    private void CreateGrid()
    {
        grid = new Node[gridsizeX, gridsizeY];
        Vector2 BottomLeft = (Vector2)transform.position - Vector2.right * Gridworldsize.x / Half - Vector2.up * Gridworldsize.y / Half;

        for (int y = 0; y < gridsizeY; y++)
        {
            for (int x = 0; x < gridsizeX; x++)
            {
                Vector2 WorldPoint = BottomLeft + Vector2.right * (x * Nodediameter + Noderadios) + Vector2.up * (y * Nodediameter + Noderadios);
                bool wall = false;


                if(Physics2D.OverlapCircle(WorldPoint, Noderadios, WallMask))
                {
                    wall = true;
                }
                grid[x, y] = new Node(wall, WorldPoint, x, y);
               // Debug.Log($"Node at ({x}, {y}): Is wall = {wall}"); // 追加
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector2(Gridworldsize.x, Gridworldsize.y));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = node.Iswall ? Color.white : Color.yellow;
                Gizmos.DrawCube(node.Position, Vector2.one * (Nodediameter - Distance));
            }

            // FinalPathのノードを赤で描画
            if (FinalPath != null)
            {
                Gizmos.color = Color.red;
                foreach (Node node in FinalPath)
                {
                    Gizmos.DrawCube(node.Position, Vector2.one * (Nodediameter - Distance));
                }
            }
        }
    }
}
