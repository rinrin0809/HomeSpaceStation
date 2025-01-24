using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fov_script : MonoBehaviour
{
    public Transform visionObject; // ����I�u�W�F�N�g
    public LayerMask obstacleLayer; // ��Q���̃��C���[�}�X�N
    public float fieldOfViewAngle = 70f; // ����p
    public float detectionDistance = 5f; // ���m����

    public GameObject player; // �v���C���[��Transform
    internal Vector2 CurrentDirection;

    public float sightCounter = 0;
    public float sightThreshold = 3;

    [SerializeField]
    private GameOverFade game;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = FindObjectOfType<Player>()?.gameObject;
        }

        if (player == null)
        {
            Debug.LogError("�v���C���[��������Ȃ��I");
        }
        else
        {
            Debug.Log($"�������v���C���[: {player.name} (ID: {player.GetInstanceID()})");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // �v���C���[�����F�ł��邩�`�F�b�N
        bool canSeePlayer = IsPlayerInFieldOfView(player.transform, transform, fieldOfViewAngle, detectionDistance);

        if (canSeePlayer)
        {
            // ���F���Ȃ�J�E���g�����Z
            sightCounter += Time.deltaTime;
            //Debug.Log($"���F��: �J�E���g = {sightCounter}/{sightThreshold}");

            if (sightCounter >= sightThreshold)
            {
                game.gameObject.SetActive(true); // �Q�[���I�[�o�[�𔭓�
                //Debug.Log("�Q�[���I�[�o�[�𔭓��I");
            }
        }
        else
        {
            // ���F�͈͊O�Ȃ�J�E���g�����Z�b�g
            sightCounter = 0f;
            //Debug.Log("���F�͈͊O: �J�E���g���Z�b�g");
        }

        // �f�o�b�O�o��
        // Debug.Log($"�v���C���[���F���: {canSeePlayer}");
    }

    public bool IsPlayerInFieldOfView(Transform player, Transform enemy, float fieldOfViewAngle, float detectionDistance)
    {
        // �v���C���[�܂ł̕������v�Z
        // Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        // float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        // //float angle = Vector3.Angle(enemy.forward, directionToPlayer);
        //// Debug.Log($"����p�`�F�b�N: {angle}��");

        // // ���������m�͈͊O�Ȃ� false
        // if (distanceToPlayer > detectionDistance)
        // {
        //     return false;
        // }

        // // �G�̎�������i�ړ������j���擾
        // Vector3 forward = CurrentDirection; // Fov_script �� CurrentDirection ���g�p
        //// Debug.Log($"���݂̎�������: {CurrentDirection}");
        // // ����p�𔻒�
        // float angleToPlayer = Vector3.Angle(forward, directionToPlayer);
        // if (angleToPlayer > fieldOfViewAngle / 2)
        // {
        //     return false;
        // }

        // // �f�o�b�O�p���C�L���X�g�i��������j
        // Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // �������
        // Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.red); // �v���C���[����

        // // ���C�L���X�g�ŏ�Q�����`�F�b�N
        // Vector3 rayStart = enemy.position + Vector3.up * 0.5f; // ������Əォ���΂�
        // RaycastHit2D hit = Physics2D.Raycast(rayStart, directionToPlayer, distanceToPlayer);
        // Debug.Log($"directionToPlayer: {directionToPlayer}");

        // // �f�o�b�O���O��ǉ�
        // if (hit.collider != null)
        // {
        //     Debug.Log($"���C�L���X�g�q�b�g: {hit.collider.name}"); // ���ɓ������Ă邩
        // }
        // else
        // {
        //     Debug.Log("���C�L���X�g: ���ɂ�������Ȃ�����"); // ���ɂ��������ĂȂ��Ȃ�A�ʂ̌��������H
        // }

        // if (hit.collider != null && hit.collider.transform != player)
        // {
        //    // Debug.Log($"��Q���Ŏ��F�s��: {hit.collider.name}");
        //     Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.blue); // �v���C���[����
        //     return false;
        // }
        // //if (game != null)
        // //{
        // //    game.gameObject.SetActive(true);
        // //}

        // return true;

        Vector3 toPlayer = (player.position - enemy.position).normalized; // �v���C���[�ւ̕����x�N�g��

        // ������ forward ���`
        Vector3 forward = new Vector3(CurrentDirection.x, CurrentDirection.y, 0).normalized; // Z�����Œ肵��2D�ɑΉ�

        float angle = Vector3.Angle(forward, toPlayer); // forward��toPlayer�̊p�x�𑪂�

        // �������l�ȉ���X�����̓[���ɂ���i�قڐ^��or�^���Ȃ犮�S�ȏc�x�N�g���Ɂj
        if (Mathf.Abs(forward.x) < 0.1f) forward.x = 0f;

        // �������A�قډ������Ȃ�Y�������[���ɂ���
        if (Mathf.Abs(forward.y) < 0.1f) forward.y = 0f;

        // �f�o�b�O�p
        Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // �G�̐���
        Debug.DrawRay(enemy.position, toPlayer * detectionDistance, Color.red); // �v���C���[����

        // ���̊p�x�� & ��苗�����Ȃ王�F
        if (angle < fieldOfViewAngle * 0.5f && Vector3.Distance(enemy.position, player.position) <= detectionDistance)
        {
            return true;
        }

        return false;
    }

    

    public void UpdateVisionDirection(Vector3 direction)
    {

       
        if (direction.magnitude > 0.1f) // �L���ȕ����̂ݍX�V
        {
            CurrentDirection = direction.normalized; // �ړ��������X�V
        }

       // Debug.Log($"CurrentDirection: {CurrentDirection}");
        //if (direction.magnitude > 0.1f) // �L���ȕ���������ꍇ�̂ݍX�V
        //{
        //    // ���݂̕�����ۑ�
        //    CurrentDirection = direction;

        //    // ����I�u�W�F�N�g����]
        //    //if (visionObject != null)
        //    //{
        //    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //    //    visionObject.rotation = Quaternion.Euler(0, 0, angle);
        //    //}

        //    // �G�{�̂̉�]���X�V
        //    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //    transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        //    // �f�o�b�O�p���O
        //    Debug.Log($"����Ɖ�]�X�V: �ړ�����={direction}, ��]�p�x={targetAngle}");
        //}
    }
}
