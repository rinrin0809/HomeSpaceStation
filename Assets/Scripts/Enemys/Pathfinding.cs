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
    private MoveEnemy enemyMovement; // �G�̒Ǐ]�X�N���v�g���Q�Ƃ���ϐ�

    private float pathUpdateInterval = 0.5f; // �o�H�X�V�̊Ԋu
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
       
        enemyMovement = FindObjectOfType<MoveEnemy>(); // �G�̒Ǐ]�X�N���v�g���擾
        if (enemyMovement == null)
        {
           // Debug.LogError("EnemyFollowPath script not found in the scene.");
        }
    }
    private void Update()
    {
        
        // �v���C���[�̈ʒu���擾
        if (TargetPosition == null)
        {
           // Debug.LogError("TargetPosition is not set.");
            return;
        }

        timeSinceLastUpdate += Time.deltaTime;

        // �G�ƃv���C���[�̋������v�Z
        float distanceToPlayer = Vector2.Distance(StartPosition.position, TargetPosition.position);

        // ���̋������ł��A�o�H�X�V�̊Ԋu���o�߂����ꍇ�Ɍo�H���Čv�Z
        if (distanceToPlayer < 20f && timeSinceLastUpdate >= pathUpdateInterval) // 10f�͓K�X����
        {
            timeSinceLastUpdate = 0f; // �^�C�}�[�����Z�b�g
            FindPath(StartPosition.position, TargetPosition.position);

            // �p�X�����������ꍇ�͓G�ɒʒm
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

       // Debug.Log($"Start Node: {StartNode.Position}, Target Node: {TargetNode.Position}"); // �ǉ�

        if (TargetNode.Iswall)
        {
           // Debug.Log("Target node is a wall, finding nearest walkable node.");
            TargetNode = GetNearestWalkableNode(TargetNode);
        }

        // OpenList��ClosedList�̏�����
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

            // �ړI�̃m�[�h�ɓ��B�����ꍇ
            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
               return;
            }

            // �אڃm�[�h���擾
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
                break; // �ŏ��Ɍ�������������m�[�h��Ԃ�
            }
        }

        return nearestNode;
    }

    // �ŏI�I�ȃp�X���m�肳���郁�\�b�h
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

        // �G�̌o�H���X�V
        if (enemyMovement != null)
        {
            enemyMovement.UpdatePath(FinalPath);
        }
    }

    // �}���n�b�^���������v�Z���郁�\�b�h
    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);
        return ix + iy;
    }
}

