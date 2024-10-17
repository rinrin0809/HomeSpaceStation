//using UnityEngine;
//using UnityEngine.AI;

//public class TestEnemyAI : MonoBehaviour
//{
//    public Transform player;
//    private NavMeshAgent agent;
//    public float detectionDistance = 1f; // 障害物検知距離
//    public float avoidanceDistance = 2f; // 避ける際の距離
//    private Vector3 direction;

//    [SerializeField]
//    private float speed = 2;
//    // AIの有効/無効を制御するフラグ
//    public bool isAIEnabled = true;
//    private BoxCollider2D box2d;
//    [SerializeField]
//    private Rigidbody2D rb;
//    // 複数のRayを使って障害物の検知
//    bool obstacleDetected = false;



//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        box2d = GetComponent<BoxCollider2D>();
//        rb = GetComponent<Rigidbody2D>();
//        agent.updateRotation = false;
//        agent.updateUpAxis = false;
//    }

//    void Update()
//    {
//        if (box2d.isTrigger)
//        {
//            MoveTowardsPlayer();
//        }
//        else
//        {
//            //// AIが無効な場合の処理（例: 停止）
//            //// 障害物が見つかった場合、回避方向を計算
//            //Vector3 avoidanceDirection = Vector3.Cross(direction.normalized, Vector3.up).normalized;

//            //// 遠回りのため、元の方向に回避方向を加算
//            //direction += avoidanceDirection * avoidanceDistance;

//            //// 新しい目的地を設定
//            //Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//            //// NavMeshAgentで目的地を設定
//            //agent.SetDestination(newDestination);
//            //
//            Vector3 directionToPlayer = GetDirectionToPlayer();
//            Vector3 forward = transform.forward; // 敵の現在の向き

//            // 内積を使って比較
//            float dotProduct = Vector3.Dot(forward, directionToPlayer);
//            Debug.Log("Dot Product: " + dotProduct);

//            // 角度を使った比較
//            float angle = Vector3.Angle(forward, directionToPlayer);
//            Debug.Log("Angle to Player: " + angle);

//            // ある角度以下であればアクションを実行
//            if (angle < 80f)
//            {
//                // プレイヤーに対して正面を向いている場合の処理
//                //    AIが無効な場合の処理（例: 停止）
//                //     障害物が見つかった場合、回避方向を計算
//                Vector3 avoidanceDirection = Vector3.Cross(direction.normalized, Vector3.up).normalized;

//                //    遠回りのため、元の方向に回避方向を加算
//                direction += avoidanceDirection * avoidanceDistance;

//                //    新しい目的地を設定
//                Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//                //    NavMeshAgentで目的地を設定
//                agent.SetDestination(newDestination);

//            }

//        }
//    }

//    private void MoveTowardsPlayer()
//    {
//        // プレイヤーとの距離を計算
//        direction = (player.position - transform.position);

//        // 斜め移動を防ぐために、XまたはYのどちらか一方の移動を選択
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            direction.y = 0; // Z方向に移動
//            //Debug.Log(direction);
//        }
//        else
//        {
//            direction.x = 0; // X方向に移動
//            //Debug.Log(direction);
//        }


//        // 新しい目的地を設定
//        Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//        // NavMeshAgentで目的地を設定
//        agent.SetDestination(newDestination);
//    }

//    private Vector3 GetDirectionToPlayer()
//    {
//        Vector3 direction = player.position - transform.position;
//        direction.Normalize(); // 正規化
//        return direction;
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.tag == "Wall")
//        {
//            box2d.isTrigger = true;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.gameObject.tag == "Wall")
//        {
//            box2d.isTrigger = false;
//        }
//    }

//    // デバッグ用: Rayの可視化
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        direction = (player.position - transform.position).normalized;
//        Gizmos.DrawLine(transform.position, transform.position + direction * detectionDistance);
//    }

//}

//using UnityEngine;
//using UnityEngine.AI;

//public class TestEnemyAI : MonoBehaviour
//{
//    public Transform player; // プレイヤーのTransform
//    private NavMeshAgent agent; // NavMeshAgent
//    public float detectionDistance = 1f; // 障害物検知距離
//    public float avoidanceDistance = 2f; // 避ける際の距離
//    private Vector2 direction; // 移動方向
//    private float speed;

//    private BoxCollider2D box2d;
//    [SerializeField]
//    private Rigidbody2D rb;
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false; // 回転を有効にする
//        agent.updateUpAxis = false; // Y軸の更新を無効にする

//        box2d = GetComponent<BoxCollider2D>();
//        rb = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        if (box2d.isTrigger)
//        {

//            MoveTowardsPlayer2();
//        }
//        else
//        {
//            MoveTowardsPlayer();

//        }
//    }

//    private void MoveTowardsPlayer2()
//    {
//        // プレイヤーとの距離を計算
//        direction = (player.position - transform.position);

//        // 斜め移動を防ぐために、XまたはYのどちらか一方の移動を選択
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            direction.y = 0; // Z方向に移動
//                             //Debug.Log(direction);
//        }
//        else
//        {
//            direction.x = 0; // X方向に移動
//                             //Debug.Log(direction);
//        }


//        // 新しい目的地を設定
//        Vector2 newDestination = (Vector2)transform.position + direction.normalized + direction * speed * Time.deltaTime;

//        // NavMeshAgentで目的地を設定
//        agent.SetDestination(newDestination);
//    }


//    private void MoveTowardsPlayer()
//    {
//        // プレイヤーとの距離を計算
//        direction = (player.position - transform.position).normalized;


//        // Raycastで障害物を検出
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance))
//        {
//            if (hit.collider.CompareTag("Wall"))
//            {
//                // 障害物が見つかった場合、回避方向を計算
//                Vector2 avoidanceDirection = Vector3.Cross(direction, Vector3.up).normalized;

//                // 新しい目的地を設定（回避方向に進む）
//                Vector2 newDestination = (Vector2)transform.position + avoidanceDirection * avoidanceDistance;
//                agent.SetDestination(newDestination);

//                return; // これ以上の処理をスキップ
//            }


//        }

//        box2d.isTrigger = true;
//        // 障害物がない場合、プレイヤーの方へ向かう
//        agent.SetDestination(player.position);
//    }

//    private void OnCollisionEnter(Collision collision) //ぶつかったら消える命令文開始
//    {
//        if (collision.gameObject.CompareTag("Wall"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
//        {

//        }
//    }

//    // デバッグ用: Rayの可視化
//    private void OnDrawGizmos()
//    {
//        if (player != null)
//        {
//            Gizmos.color = Color.red;
//            Vector2 directionToPlayer = (player.position - transform.position).normalized;
//            Gizmos.DrawLine(transform.position, (Vector2)transform.position + directionToPlayer * detectionDistance);
//        }
//    }
//}
//using UnityEngine;
//using UnityEngine.AI;

//public class TestEnemyAI : MonoBehaviour
//{
//    public Transform player; // プレイヤーのTransform
//    public float detectionDistance = 1f; // 障害物検知距離
//    public float avoidanceDistance = 2f; // 回避する距離
//    public float speed = 5.0f; // 移動速度

//    private BoxCollider2D box2d;
//    private Rigidbody2D rb;
//    private NavMeshAgent agent; // NavMeshAgent

//    void Start()
//    {
//        box2d = GetComponent<BoxCollider2D>();
//        rb = GetComponent<Rigidbody2D>();

//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false; // 回転を無効にする
//        agent.updateUpAxis = false;  // 2Dの場合、Y軸を無効化
//    }

//    void Update()
//    {
//        if (player != null && agent != null)
//        {
//            // プレイヤーの位置を目的地として設定
//            agent.SetDestination(player.position);

//            //// エージェントが画面外に出ないように制限
//            //Vector3 position = transform.position;
//            //position.x = Mathf.Clamp(position.x, 0, Screen.width); // X座標を画面の幅で制限
//            //position.y = Mathf.Clamp(position.y, 0, Screen.height); // Y座標を画面の高さで制限
//            //transform.position = position;

//            Debug.Log("Agent Position: " + transform.position);
//            Debug.Log("Camera Position: " + Camera.main.transform.position);
//            // 経路の状態を確認
//            //if (agent.pathStatus == NavMeshPathStatus.PathComplete)
//            //{
//            //    Debug.Log("Path is complete. Agent can reach the destination.");
//            //    MoveTowardsPlayer(); // プレイヤーに向かって直角移動
//            //}
//            //else if (agent.pathStatus == NavMeshPathStatus.PathPartial)
//            //{
//            //    Debug.Log("Path is partial. The agent can reach part of the path.");
//            //}
//            //else if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
//            //{
//            //    Debug.Log("Path is invalid. The agent cannot reach the destination.");
//            //}
//        }
//    }

//    private void MoveTowardsPlayer()
//    {
//        // プレイヤーとの距離を計算
//        Vector2 direction = (player.position - transform.position).normalized;

//        // Raycastで障害物を検出
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);
//        if (hit.collider != null && hit.collider.CompareTag("Wall"))
//        {
//            Debug.Log("Obstacle detected: " + hit.collider.name);
//            MoveTowardsPlayerWithAvoidance(); // 障害物回避
//        }
//        else
//        {
//            // 直角移動の実装
//            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//            {
//                direction.y = 0; // Y方向を無視
//            }
//            else
//            {
//                direction.x = 0; // X方向を無視
//            }

//            // 移動
//            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

//            // 新しい位置を設定
//            transform.position = newPosition;
//        }
//    }

//    private void MoveTowardsPlayerWithAvoidance()
//    {
//        // プレイヤーとの距離を計算
//        Vector2 direction = (player.position - transform.position).normalized;

//        // Raycastで障害物を検出
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);
//        if (hit.collider != null && hit.collider.CompareTag("Wall"))
//        {
//            Debug.Log("Obstacle detected while avoiding: " + hit.collider.name);
//            // 障害物が見つかった場合、NavMeshAgentを使って経路を再計算
//            Vector2 avoidanceDirection = Vector2.Perpendicular(direction).normalized;

//            // 新しい目的地を設定（回避方向に進む）
//            Vector2 newDestination = (Vector2)transform.position + avoidanceDirection * avoidanceDistance;
//            agent.SetDestination(newDestination); // NavMeshAgentに新しい目的地を設定
//        }
//        else
//        {
//            // 障害物がない場合、NavMeshAgentでプレイヤーに向かって移動
//            agent.SetDestination(player.position);
//        }
//    }

//    // デバッグ用: Rayの可視化
//    private void OnDrawGizmos()
//    {
//        if (player != null)
//        {
//            Gizmos.color = Color.red;
//            Vector2 directionToPlayer = (player.position - transform.position).normalized;
//            Gizmos.DrawLine(transform.position, (Vector2)transform.position + directionToPlayer * detectionDistance);
//        }
//    }
//}
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyAI : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float detectionDistance = 1f; // 障害物検知距離
    public float speed = 5.0f; // 移動速度
    public float updatePathInterval = 1f; // 経路探索の更新間隔

    private NavMeshAgent agent; // NavMeshAgent
    private Vector3 targetPosition; // 目的地
    private float nextPathUpdateTime = 0f; // 次回経路更新の時間

    private Vector2 direction; // 移動方向

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // 回転を無効にする
        agent.updateUpAxis = false; // 2Dの場合、Y軸を無効化
        targetPosition = transform.position; // 初期目的地を現在位置に設定
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            
            MoveTowardsPlayer();
CheckForPathUpdate();
        }
        
    }

    private void MoveTowardsPlayer()
    {
        //// プレイヤーの位置を目的地として設定
        //targetPosition = player.position;

        //// 障害物がない場合、プレイヤーに向かって移動
        //agent.SetDestination(targetPosition);

        ////// デバッグ情報
        ////Debug.Log("Agent Position: " + transform.position);
        ////Debug.Log("Agent Velocity: " + agent.velocity);
        ///
        // プレイヤーとの距離を計算
        direction = (player.position - transform.position);

        // 斜め移動を防ぐために、XまたはYのどちらか一方の移動を選択
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0; // Z方向に移動
            //Debug.Log(direction);
        }
        else
        {
            direction.x = 0; // X方向に移動
            //Debug.Log(direction);
        }


        // 新しい目的地を設定
        Vector2 newDestination = (Vector2)transform.position + direction.normalized + direction * speed * Time.deltaTime;

        // NavMeshAgentで目的地を設定
        agent.SetDestination(newDestination);
    }

    private void CheckForPathUpdate()
    {
        //一定間隔で経路を更新
        if (Time.time >= nextPathUpdateTime)
        {
            // 新しい経路を計算
            Vector3 newPathTarget = GetNewPathTarget();

            // 新しい経路が現在の経路よりも近い場合、経路を切り替える
            if (Vector3.Distance(agent.destination, newPathTarget) > Vector3.Distance(agent.destination, targetPosition))
            {
                agent.SetDestination(newPathTarget);
            }

            // 次回経路更新の時間を設定
            nextPathUpdateTime = Time.time + updatePathInterval;
        }
    }

    private Vector3 GetNewPathTarget()
    {
        // プレイヤーの位置から障害物を考慮して新しい経路ターゲットを取得するロジックを実装
        // ここでは単純にプレイヤーの位置を返す例を示します
        return player.position; // プレイヤーの位置を返す (ここを改良して経路探索を行う)
    }

    // デバッグ用: Rayの可視化
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + directionToPlayer * detectionDistance);
        }
    }
}

