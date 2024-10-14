using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TestEnemyAI : MonoBehaviour
{

    //public Transform player;
    //NavMeshAgent agent;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    agent.SetDestination(player.position);
    //}

    [System.Serializable]
    public class Behavior
    {
        public string name;
        public float weight;
        public Transform target; // �s�����Ƃ̖ړI�n
    }

    public Behavior[] behaviors;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SelectAndMove();
    }

    void SelectAndMove()
    {
        Behavior selectedBehavior = SelectBehavior();
        if (selectedBehavior != null)
        {
           // navMeshAgent.SetDestination(target);
        }
    }

    Behavior SelectBehavior()
    {
        float totalWeight = 0f;
        foreach (var behavior in behaviors)
        {
            totalWeight += behavior.weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        foreach (var behavior in behaviors)
        {
            if (randomValue < behavior.weight)
            {
                return behavior;
            }
            randomValue -= behavior.weight;
        }

        return null; // �����ɂ͓��B���Ȃ��͂�
    }

    void Update()
    {
        // �G�[�W�F���g���ړ������ǂ������`�F�b�N����ꍇ
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // �ړ�������������̏����������ɒǉ��ł��܂�
            // �Ⴆ�΁A�V�����s����I������Ȃ�
            SelectAndMove();
        }
    }

    //public Transform player;
    //public float moveSpeed = 2f;

    //private Vector2[] path;
    //private int currentPathIndex;
    //private float pathCalculationInterval = 0.5f; // �o�H�v�Z�̊Ԋu�i�b�j

    //void Start()
    //{
    //    StartCoroutine(CalculatePathCoroutine());
    //}

    //IEnumerator CalculatePathCoroutine()
    //{
    //    while (true)
    //    {
    //        if (player != null)
    //        {
    //            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    //            Debug.Log($"Distance to player: {distanceToPlayer}"); // �f�o�b�O�p
    //            if (distanceToPlayer < 10f) // �ǐՂ��J�n���鋗��
    //            {
    //                Debug.Log("Calculating path..."); // �f�o�b�O�p
    //                path = AStarAI.CalculatePath(transform.position, player.position);
    //                if (path != null && path.Length > 0)
    //                {
    //                    MoveAlongPath();
    //                }
    //                else
    //                {
    //                    Debug.Log("No path found."); // �f�o�b�O�p
    //                }
    //            }
    //        }
    //        yield return new WaitForSeconds(pathCalculationInterval);
    //    }
    //}

    //void Update()
    //{
    //    if (path != null && currentPathIndex < path.Length)
    //    {
    //        MoveAlongPath();
    //    }
    //    else
    //    {
    //        Debug.Log("No path available."); // �p�X�������ꍇ�̃f�o�b�O�p
    //    }
    //}

    //void MoveAlongPath()
    //{
    //    Debug.Log($"Moving along path. Current index: {currentPathIndex}");
    //    if (path != null && currentPathIndex < path.Length)
    //    {
    //        Vector2 targetPosition = path[currentPathIndex];
    //        Debug.Log($"Moving to target position: {targetPosition}");
    //        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    //        // ���B����̐��x���グ��
    //        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // �߂��ꍇ�Ɏ��̃m�[�h��
    //        {
    //            Debug.Log("Reached target, moving to next.");
    //            currentPathIndex++;
    //        }
    //    }
    //}

    //public void ResetPath()
    //{
    //    currentPathIndex = 0; // �o�H�̃C���f�b�N�X�����Z�b�g
    //}

    //private void OnDrawGizmos()
    //{
    //    if (path != null && path.Length > 0)
    //    {
    //        Gizmos.color = Color.red;
    //        for (int i = 0; i < path.Length; i++)
    //        {
    //            Gizmos.DrawSphere(path[i], 0.1f);
    //        }
    //    }
    //}
}
