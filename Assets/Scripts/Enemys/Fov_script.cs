using System.Collections;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public Transform visionObject; // ����I�u�W�F�N�g
    public LayerMask obstacleLayer; // ��Q���̃��C���[�}�X�N
    public float fieldOfViewAngle = 70f; // ����p
    public float detectionDistance = 5f; // ���m����

    private Transform player; // �v���C���[��Transform
    internal Vector2 CurrentDirection;

    public float sightCounter = 0;
    public float sightThreshold = 3;

    [SerializeField]
    private GameOverFade game;

    private void Start()
    {
        // �v���C���[���擾�i�^�O�Ō����j
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        // �v���C���[�����F�ł��邩�`�F�b�N
        bool canSeePlayer = IsPlayerInFieldOfView(player, transform, fieldOfViewAngle, detectionDistance);

        if (canSeePlayer)
        {
            // ���F���Ȃ�J�E���g�����Z
            sightCounter += Time.deltaTime;
            Debug.Log($"���F��: �J�E���g = {sightCounter}/{sightThreshold}");

            if (sightCounter >= sightThreshold)
            {
                game.gameObject.SetActive(true); // �Q�[���I�[�o�[�𔭓�
                Debug.Log("�Q�[���I�[�o�[�𔭓��I");
            }
        }
        else
        {
            // ���F�͈͊O�Ȃ�J�E���g�����Z�b�g
            sightCounter = 0f;
            Debug.Log("���F�͈͊O: �J�E���g���Z�b�g");
        }

        // �f�o�b�O�o��
        // Debug.Log($"�v���C���[���F���: {canSeePlayer}");
    }

    public bool IsPlayerInFieldOfView(Transform player, Transform enemy, float fieldOfViewAngle, float detectionDistance)
    {
        // �v���C���[�܂ł̕������v�Z
        Vector3 directionToPlayer = (player.position - enemy.position).normalized;
        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        // ���������m�͈͊O�Ȃ� false
        if (distanceToPlayer > detectionDistance)
        {
            return false;
        }

        // �G�̎�������i�ړ������j���擾
        Vector3 forward = CurrentDirection; // Fov_script �� CurrentDirection ���g�p

        // ����p�𔻒�
        float angleToPlayer = Vector3.Angle(forward, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2)
        {
            return false;
        }

        // �f�o�b�O�p���C�L���X�g�i��������j
        Debug.DrawRay(enemy.position, forward * detectionDistance, Color.green); // �������
        Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.red); // �v���C���[����

        // ���C�L���X�g�ŏ�Q�����`�F�b�N
        RaycastHit2D hit = Physics2D.Raycast(enemy.position, directionToPlayer, distanceToPlayer);
        if (hit.collider != null && hit.collider.transform != player)
        {
           // Debug.Log($"��Q���Ŏ��F�s��: {hit.collider.name}");
            Debug.DrawRay(enemy.position, directionToPlayer * distanceToPlayer, Color.blue); // �v���C���[����
            return false;
        }

        //Debug.Log("�v���C���[�����F���܂����I");
        //if (game != null)
        //{
        //    game.gameObject.SetActive(true);
        //}
       
        return true;
    }

    

    public void UpdateVisionDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.1f) // �L���ȕ����̂ݍX�V
        {
            CurrentDirection = direction.normalized; // �ړ��������X�V
        }
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
