using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;         // �G�̈ړ����x
    public Transform player;             // �v���C���[��Transform
    public float gridSize = 1f;          // �O���b�h�̃T�C�Y
    public float raycastDistance = 1.0f; // Raycast�̋���
    public LayerMask obstacleLayer;      // ��Q���̃��C���[�}�X�N

    private Vector2 targetPosition;      // �G�����Ɉړ�����ʒu
    private List<Vector2> path = new List<Vector2>(); // A*�o�H�T���̌���

    Rigidbody2D rb;
    void Start()
    {
        obstacleLayer = LayerMask.GetMask("Obstacles");
        rb = GetComponent<Rigidbody2D>();
        // �v���C���[��T��
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // �����ʒu���O���b�h�ɃX�i�b�v
        targetPosition = SnapToGrid(transform.position);
    }

    void Update()
    {
        // A*�A���S���Y�����g�p���ăv���C���[�ւ̍ŒZ�o�H���v�Z
        path = FindPath(transform.position, player.position);

        if (path != null && path.Count > 0)
        {
            // �o�H�ɏ]���Ĉړ�
            targetPosition = path[0];  // ���̃^�[�Q�b�g�ʒu���o�H�̐擪�ɐݒ�
            path.RemoveAt(0);          // �o�H�̐擪���폜
        }

        // �^�[�Q�b�g�ʒu�Ɍ������Ĉړ�
        MoveToTarget();
    }

    // �^�[�Q�b�g�ʒu�Ɍ������Ĉړ�
    void MoveToTarget()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        }
    }

    // �O���b�h�ɃX�i�b�v����
    Vector2 SnapToGrid(Vector2 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector2(x, y);
    }

    // A*�A���S���Y���Ōo�H��T������֐�
    List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        // �J�n�n�_�ƃS�[���n�_���O���b�h�ɃX�i�b�v
        start = SnapToGrid(start);
        target = SnapToGrid(target);

        // �T���ɕK�v�ȃ��X�g���`
        List<Node> openList = new List<Node>();  // ���T���m�[�h
        HashSet<Vector2> closedList = new HashSet<Vector2>();  // �T���ς݃m�[�h���ʒu�x�[�X�ŕۑ�

        // �J�n�m�[�h���쐬����openList�ɒǉ�
        Node startNode = new Node(start, null, 0, GetDistance(start, target));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // F�l���ŏ��̃m�[�h���擾
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F || (openList[i].F == currentNode.F && openList[i].H < currentNode.H))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode.Position);  // ���W��closedList�ɒǉ�

            // �S�[���ɓ��B�����ꍇ�A�o�H��Ԃ�
            if (currentNode.Position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            // ���͂̃m�[�h��T��
            foreach (Vector2 direction in GetNeighbours())
            {
                Vector2 neighbourPos = currentNode.Position + direction;

                // ��Q�������邩�A�T���ς݂̈ʒu���ǂ����`�F�b�N
                if (IsBlocked(neighbourPos) || closedList.Contains(neighbourPos))
                {
                    continue;
                }

                float newMovementCostToNeighbour = currentNode.G + GetDistance(currentNode.Position, neighbourPos);
                Node neighbourNode = new Node(neighbourPos, currentNode, newMovementCostToNeighbour, GetDistance(neighbourPos, target));

                // openList�ɓ����ʒu�̃m�[�h���Ȃ����m�F
                if (!openList.Exists(n => n.Position == neighbourPos) || newMovementCostToNeighbour < neighbourNode.G)
                {
                    openList.Add(neighbourNode);
                }
            }
        }

        return null; // �o�H��������Ȃ������ꍇ
    }

    // ���͂̈ړ�������Ԃ�
    List<Vector2> GetNeighbours()
    {
        return new List<Vector2>
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };
    }

    // �ړ���ɏ�Q�������邩�`�F�b�N
    bool IsBlocked(Vector2 position)
    {
        //Vector2 direction = targetPosition - (Vector2)transform.position;  // �ړI�n�ւ̕������v�Z
        //// Raycast���g���ď�Q�������o
        //RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, raycastDistance, obstacleLayer);
        //// return hit.collider != null; // ��Q��������ꍇ��true��Ԃ�

        //Debug.DrawRay(transform.position, direction * raycastDistance, Color.red);  // Ray�̉���

        //// ��Q�������m���ꂽ���ǂ���
        //if (hit.collider != null)
        //{
        //    return true;
        //}
        //return false;

        // �ړI�n�ւ̕������v�Z
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();  // ���K�����ĕ����x�N�g����P�ʃx�N�g����

        // Raycast�𔭎˂��A��Q�������邩�ǂ����m�F
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleLayer);

        // �f�o�b�O�p��Ray������
        Debug.DrawRay(transform.position, direction * raycastDistance, Color.red, 1.0f);  // 1�b�ԕ\��

        if (hit.collider != null)
        {
            Debug.Log("��Q�������m����܂���: " + hit.collider.name);  // ���m���ꂽ�I�u�W�F�N�g�̖��O�����O�ɕ\��
            return true;  // ��Q�������m
        }

        return false;  // ��Q�����Ȃ�
    }

    // �o�H�����ǂ��Ė߂�֐�
    List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse(); // �o�H�𔽓]
        return path;
    }

    // �ʒu�̍����擾
    float GetDistance(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    // �m�[�h�N���X�iA*�A���S���Y���p�j
    class Node
    {
        public Vector2 Position;
        public Node Parent;
        public float G; // �J�n�_����̃R�X�g
        public float H; // �S�[���܂ł̐���R�X�g

        public float F { get { return G + H; } } // F�l = G + H

        public Node(Vector2 position, Node parent, float g, float h)
        {
            this.Position = position;
            this.Parent = parent;
            this.G = g;
            this.H = h;
        }

        // Vector2����ɔ�r����
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
