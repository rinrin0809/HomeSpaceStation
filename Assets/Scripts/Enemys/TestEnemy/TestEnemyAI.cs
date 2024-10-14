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
        public Transform target; // 行動ごとの目的地
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

        return null; // ここには到達しないはず
    }

    void Update()
    {
        // エージェントが移動中かどうかをチェックする場合
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // 移動が完了した後の処理をここに追加できます
            // 例えば、新しい行動を選択するなど
            SelectAndMove();
        }
    }

    //public Transform player;
    //public float moveSpeed = 2f;

    //private Vector2[] path;
    //private int currentPathIndex;
    //private float pathCalculationInterval = 0.5f; // 経路計算の間隔（秒）

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
    //            Debug.Log($"Distance to player: {distanceToPlayer}"); // デバッグ用
    //            if (distanceToPlayer < 10f) // 追跡を開始する距離
    //            {
    //                Debug.Log("Calculating path..."); // デバッグ用
    //                path = AStarAI.CalculatePath(transform.position, player.position);
    //                if (path != null && path.Length > 0)
    //                {
    //                    MoveAlongPath();
    //                }
    //                else
    //                {
    //                    Debug.Log("No path found."); // デバッグ用
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
    //        Debug.Log("No path available."); // パスが無い場合のデバッグ用
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

    //        // 到達判定の精度を上げる
    //        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // 近い場合に次のノードへ
    //        {
    //            Debug.Log("Reached target, moving to next.");
    //            currentPathIndex++;
    //        }
    //    }
    //}

    //public void ResetPath()
    //{
    //    currentPathIndex = 0; // 経路のインデックスをリセット
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
