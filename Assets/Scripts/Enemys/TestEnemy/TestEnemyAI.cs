//using UnityEngine;
//using UnityEngine.AI;

//public class TestEnemyAI : MonoBehaviour
//{
//    public Transform player;
//    private NavMeshAgent agent;
//    public float detectionDistance = 1f; // ��Q�����m����
//    public float avoidanceDistance = 2f; // ������ۂ̋���
//    private Vector3 direction;

//    [SerializeField]
//    private float speed = 2;
//    // AI�̗L��/�����𐧌䂷��t���O
//    public bool isAIEnabled = true;
//    private BoxCollider2D box2d;
//    [SerializeField]
//    private Rigidbody2D rb;
//    // ������Ray���g���ď�Q���̌��m
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
//            //// AI�������ȏꍇ�̏����i��: ��~�j
//            //// ��Q�������������ꍇ�A���������v�Z
//            //Vector3 avoidanceDirection = Vector3.Cross(direction.normalized, Vector3.up).normalized;

//            //// �����̂��߁A���̕����ɉ����������Z
//            //direction += avoidanceDirection * avoidanceDistance;

//            //// �V�����ړI�n��ݒ�
//            //Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//            //// NavMeshAgent�ŖړI�n��ݒ�
//            //agent.SetDestination(newDestination);
//            //
//            Vector3 directionToPlayer = GetDirectionToPlayer();
//            Vector3 forward = transform.forward; // �G�̌��݂̌���

//            // ���ς��g���Ĕ�r
//            float dotProduct = Vector3.Dot(forward, directionToPlayer);
//            Debug.Log("Dot Product: " + dotProduct);

//            // �p�x���g������r
//            float angle = Vector3.Angle(forward, directionToPlayer);
//            Debug.Log("Angle to Player: " + angle);

//            // ����p�x�ȉ��ł���΃A�N�V���������s
//            if (angle < 80f)
//            {
//                // �v���C���[�ɑ΂��Đ��ʂ������Ă���ꍇ�̏���
//                //    AI�������ȏꍇ�̏����i��: ��~�j
//                //     ��Q�������������ꍇ�A���������v�Z
//                Vector3 avoidanceDirection = Vector3.Cross(direction.normalized, Vector3.up).normalized;

//                //    �����̂��߁A���̕����ɉ����������Z
//                direction += avoidanceDirection * avoidanceDistance;

//                //    �V�����ړI�n��ݒ�
//                Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//                //    NavMeshAgent�ŖړI�n��ݒ�
//                agent.SetDestination(newDestination);

//            }

//        }
//    }

//    private void MoveTowardsPlayer()
//    {
//        // �v���C���[�Ƃ̋������v�Z
//        direction = (player.position - transform.position);

//        // �΂߈ړ���h�����߂ɁAX�܂���Y�̂ǂ��炩����̈ړ���I��
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            direction.y = 0; // Z�����Ɉړ�
//            //Debug.Log(direction);
//        }
//        else
//        {
//            direction.x = 0; // X�����Ɉړ�
//            //Debug.Log(direction);
//        }


//        // �V�����ړI�n��ݒ�
//        Vector2 newDestination = transform.position + direction.normalized + direction * speed * Time.deltaTime;

//        // NavMeshAgent�ŖړI�n��ݒ�
//        agent.SetDestination(newDestination);
//    }

//    private Vector3 GetDirectionToPlayer()
//    {
//        Vector3 direction = player.position - transform.position;
//        direction.Normalize(); // ���K��
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

//    // �f�o�b�O�p: Ray�̉���
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
//    public Transform player; // �v���C���[��Transform
//    private NavMeshAgent agent; // NavMeshAgent
//    public float detectionDistance = 1f; // ��Q�����m����
//    public float avoidanceDistance = 2f; // ������ۂ̋���
//    private Vector2 direction; // �ړ�����
//    private float speed;

//    private BoxCollider2D box2d;
//    [SerializeField]
//    private Rigidbody2D rb;
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false; // ��]��L���ɂ���
//        agent.updateUpAxis = false; // Y���̍X�V�𖳌��ɂ���

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
//        // �v���C���[�Ƃ̋������v�Z
//        direction = (player.position - transform.position);

//        // �΂߈ړ���h�����߂ɁAX�܂���Y�̂ǂ��炩����̈ړ���I��
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            direction.y = 0; // Z�����Ɉړ�
//                             //Debug.Log(direction);
//        }
//        else
//        {
//            direction.x = 0; // X�����Ɉړ�
//                             //Debug.Log(direction);
//        }


//        // �V�����ړI�n��ݒ�
//        Vector2 newDestination = (Vector2)transform.position + direction.normalized + direction * speed * Time.deltaTime;

//        // NavMeshAgent�ŖړI�n��ݒ�
//        agent.SetDestination(newDestination);
//    }


//    private void MoveTowardsPlayer()
//    {
//        // �v���C���[�Ƃ̋������v�Z
//        direction = (player.position - transform.position).normalized;


//        // Raycast�ŏ�Q�������o
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance))
//        {
//            if (hit.collider.CompareTag("Wall"))
//            {
//                // ��Q�������������ꍇ�A���������v�Z
//                Vector2 avoidanceDirection = Vector3.Cross(direction, Vector3.up).normalized;

//                // �V�����ړI�n��ݒ�i�������ɐi�ށj
//                Vector2 newDestination = (Vector2)transform.position + avoidanceDirection * avoidanceDistance;
//                agent.SetDestination(newDestination);

//                return; // ����ȏ�̏������X�L�b�v
//            }


//        }

//        box2d.isTrigger = true;
//        // ��Q�����Ȃ��ꍇ�A�v���C���[�̕��֌�����
//        agent.SetDestination(player.position);
//    }

//    private void OnCollisionEnter(Collision collision) //�Ԃ�����������閽�ߕ��J�n
//    {
//        if (collision.gameObject.CompareTag("Wall"))//����������Tagutukeru�Ƃ����^�O������I�u�W�F�N�g����Ł`�Ƃ��������̉�
//        {

//        }
//    }

//    // �f�o�b�O�p: Ray�̉���
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
//    public Transform player; // �v���C���[��Transform
//    public float detectionDistance = 1f; // ��Q�����m����
//    public float avoidanceDistance = 2f; // ������鋗��
//    public float speed = 5.0f; // �ړ����x

//    private BoxCollider2D box2d;
//    private Rigidbody2D rb;
//    private NavMeshAgent agent; // NavMeshAgent

//    void Start()
//    {
//        box2d = GetComponent<BoxCollider2D>();
//        rb = GetComponent<Rigidbody2D>();

//        agent = GetComponent<NavMeshAgent>();
//        agent.updateRotation = false; // ��]�𖳌��ɂ���
//        agent.updateUpAxis = false;  // 2D�̏ꍇ�AY���𖳌���
//    }

//    void Update()
//    {
//        if (player != null && agent != null)
//        {
//            // �v���C���[�̈ʒu��ړI�n�Ƃ��Đݒ�
//            agent.SetDestination(player.position);

//            //// �G�[�W�F���g����ʊO�ɏo�Ȃ��悤�ɐ���
//            //Vector3 position = transform.position;
//            //position.x = Mathf.Clamp(position.x, 0, Screen.width); // X���W����ʂ̕��Ő���
//            //position.y = Mathf.Clamp(position.y, 0, Screen.height); // Y���W����ʂ̍����Ő���
//            //transform.position = position;

//            Debug.Log("Agent Position: " + transform.position);
//            Debug.Log("Camera Position: " + Camera.main.transform.position);
//            // �o�H�̏�Ԃ��m�F
//            //if (agent.pathStatus == NavMeshPathStatus.PathComplete)
//            //{
//            //    Debug.Log("Path is complete. Agent can reach the destination.");
//            //    MoveTowardsPlayer(); // �v���C���[�Ɍ������Ē��p�ړ�
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
//        // �v���C���[�Ƃ̋������v�Z
//        Vector2 direction = (player.position - transform.position).normalized;

//        // Raycast�ŏ�Q�������o
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);
//        if (hit.collider != null && hit.collider.CompareTag("Wall"))
//        {
//            Debug.Log("Obstacle detected: " + hit.collider.name);
//            MoveTowardsPlayerWithAvoidance(); // ��Q�����
//        }
//        else
//        {
//            // ���p�ړ��̎���
//            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//            {
//                direction.y = 0; // Y�����𖳎�
//            }
//            else
//            {
//                direction.x = 0; // X�����𖳎�
//            }

//            // �ړ�
//            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

//            // �V�����ʒu��ݒ�
//            transform.position = newPosition;
//        }
//    }

//    private void MoveTowardsPlayerWithAvoidance()
//    {
//        // �v���C���[�Ƃ̋������v�Z
//        Vector2 direction = (player.position - transform.position).normalized;

//        // Raycast�ŏ�Q�������o
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);
//        if (hit.collider != null && hit.collider.CompareTag("Wall"))
//        {
//            Debug.Log("Obstacle detected while avoiding: " + hit.collider.name);
//            // ��Q�������������ꍇ�ANavMeshAgent���g���Čo�H���Čv�Z
//            Vector2 avoidanceDirection = Vector2.Perpendicular(direction).normalized;

//            // �V�����ړI�n��ݒ�i�������ɐi�ށj
//            Vector2 newDestination = (Vector2)transform.position + avoidanceDirection * avoidanceDistance;
//            agent.SetDestination(newDestination); // NavMeshAgent�ɐV�����ړI�n��ݒ�
//        }
//        else
//        {
//            // ��Q�����Ȃ��ꍇ�ANavMeshAgent�Ńv���C���[�Ɍ������Ĉړ�
//            agent.SetDestination(player.position);
//        }
//    }

//    // �f�o�b�O�p: Ray�̉���
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
    public Transform player; // �v���C���[��Transform
    public float detectionDistance = 1f; // ��Q�����m����
    public float speed = 5.0f; // �ړ����x
    public float updatePathInterval = 1f; // �o�H�T���̍X�V�Ԋu

    private NavMeshAgent agent; // NavMeshAgent
    private Vector3 targetPosition; // �ړI�n
    private float nextPathUpdateTime = 0f; // ����o�H�X�V�̎���

    private Vector2 direction; // �ړ�����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // ��]�𖳌��ɂ���
        agent.updateUpAxis = false; // 2D�̏ꍇ�AY���𖳌���
        targetPosition = transform.position; // �����ړI�n�����݈ʒu�ɐݒ�
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
        //// �v���C���[�̈ʒu��ړI�n�Ƃ��Đݒ�
        //targetPosition = player.position;

        //// ��Q�����Ȃ��ꍇ�A�v���C���[�Ɍ������Ĉړ�
        //agent.SetDestination(targetPosition);

        ////// �f�o�b�O���
        ////Debug.Log("Agent Position: " + transform.position);
        ////Debug.Log("Agent Velocity: " + agent.velocity);
        ///
        // �v���C���[�Ƃ̋������v�Z
        direction = (player.position - transform.position);

        // �΂߈ړ���h�����߂ɁAX�܂���Y�̂ǂ��炩����̈ړ���I��
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0; // Z�����Ɉړ�
            //Debug.Log(direction);
        }
        else
        {
            direction.x = 0; // X�����Ɉړ�
            //Debug.Log(direction);
        }


        // �V�����ړI�n��ݒ�
        Vector2 newDestination = (Vector2)transform.position + direction.normalized + direction * speed * Time.deltaTime;

        // NavMeshAgent�ŖړI�n��ݒ�
        agent.SetDestination(newDestination);
    }

    private void CheckForPathUpdate()
    {
        //���Ԋu�Ōo�H���X�V
        if (Time.time >= nextPathUpdateTime)
        {
            // �V�����o�H���v�Z
            Vector3 newPathTarget = GetNewPathTarget();

            // �V�����o�H�����݂̌o�H�����߂��ꍇ�A�o�H��؂�ւ���
            if (Vector3.Distance(agent.destination, newPathTarget) > Vector3.Distance(agent.destination, targetPosition))
            {
                agent.SetDestination(newPathTarget);
            }

            // ����o�H�X�V�̎��Ԃ�ݒ�
            nextPathUpdateTime = Time.time + updatePathInterval;
        }
    }

    private Vector3 GetNewPathTarget()
    {
        // �v���C���[�̈ʒu�����Q�����l�����ĐV�����o�H�^�[�Q�b�g���擾���郍�W�b�N������
        // �����ł͒P���Ƀv���C���[�̈ʒu��Ԃ���������܂�
        return player.position; // �v���C���[�̈ʒu��Ԃ� (���������ǂ��Čo�H�T�����s��)
    }

    // �f�o�b�O�p: Ray�̉���
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

