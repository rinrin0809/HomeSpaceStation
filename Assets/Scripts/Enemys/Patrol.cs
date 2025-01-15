using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // ����|�C���g�̃��X�g
    public List<Transform> positions;

    // �����_������p�̃|�C���g���X�g
    public List<Transform> randomPositions;
    public float randomPatrolChance = 0.5f; // �����_��������J�n����m��
    public float speed = 2f; // �ړ����x
    public float rotationSpeed = 180f; // ��]���x

    // �v���C���[�֘A�ݒ�
    public Transform player; // �v���C���[�̈ʒu
    public float fieldOfViewAngle = 90f; // �O������p
    public float detectionDistance = 5f; // �v���C���[���m����
    public float behaviorResetTime = 3f; // �s�����Z�b�g����
    public float lookAtDelay = 1f; // �v���C���[�������܂ł̒x��
    public float backFieldOfViewAngle = 90f; // �������p�i���g�p�j
    public float backDetectionDistance = 3f; // �w�㌟�m����


    private int currentTargetIndex = 0; // ���݂̏���|�C���g�C���f�b�N�X
    private bool isRandomPatrol = false; // �����_�����񒆂��ǂ���
    private Transform randomTarget; // �����_�������̃^�[�Q�b�g

    private bool isLookingAtPlayer = false; // �v���C���[�𒍎������ǂ���
    private bool isPreparingToLookAtPlayer = false; // �v���C���[�𒍎����������ǂ���
    private Coroutine resetBehaviorCoroutine; // �s�����Z�b�g�p�R���[�`��

    private bool isMovementStopped = false; // �������~�܂��Ă��邩
    private Transform returnPosition; // ���̈ʒu�i���g�p�j
    public Transform randomPatrolFagPosition; // �����_������̊J�n�n�_
    public float detectionRange = 0.5f; // ����t���O���m�͈�

    public Vector3 CurrentDirection { get; private set; } // ���݂̈ړ�����

    public Animator animator; // �A�j���[�^�[�Q��



    void Update()
    {
        // �v���C���[�𒍎����̏ꍇ�A���̏�����D��
        if (isLookingAtPlayer)
        {
            LookPlayer();
        }
        else
        {
            CheckForBackDetection();


            // �����_�����񒆂��A�ʏ폄�񂩂�I��
            if (isRandomPatrol)
            {
                MoveToRandomPosition();
            }
            else
            {
                CheckForRandomPatrolFag(); // �����_������t���O�̃`�F�b�N
                MoveToPosition(); // �ʏ폄��
            }

            // �v���C���[��������ɂ��邩���`�F�b�N
            CheckForPlayer();
        }
    }

    // �v���C���[��������ɂ��邩�`�F�b�N
    private void CheckForPlayer()
    {
        if (player == null || isPreparingToLookAtPlayer || isLookingAtPlayer) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionDistance) // ���m���������m�F
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.up, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle / 2) // ��������m�F
            {
                isPreparingToLookAtPlayer = true;
                StartCoroutine(DelayLookAtPlayer()); // �v���C���[���������J�n
            }
        }
    }

    // �v���C���[������x��������R���[�`��
    private IEnumerator DelayLookAtPlayer()
    {
        yield return new WaitForSeconds(lookAtDelay); // �x�����Ԃ�҂�
        isPreparingToLookAtPlayer = false;
        isLookingAtPlayer = true;

        // �s�����Z�b�g�p�R���[�`�����~���A�V�����J�n
        if (resetBehaviorCoroutine != null)
        {
            StopCoroutine(resetBehaviorCoroutine);
        }
        resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
    }

    // �v���C���[�𒍎����鏈��
    private void LookPlayer()
    {
        if (player == null) return;

        // �v���C���[�̕������v�Z
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        SetAnimationParameters(directionToPlayer); // �A�j���[�V�����X�V

        // �G���v���C���[�����ɉ�]
        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (resetBehaviorCoroutine == null)
        {
            resetBehaviorCoroutine = StartCoroutine(ResetBehaviorAfterDelay());
        }
    }

    // �s�����Z�b�g�p�R���[�`��
    private IEnumerator ResetBehaviorAfterDelay()
    {
        yield return new WaitForSeconds(behaviorResetTime); // ���Z�b�g���Ԃ�҂�
        isLookingAtPlayer = false;
    }

    // �����_������t���O���`�F�b�N
    private void CheckForRandomPatrolFag()
    {
        if (randomPatrolFagPosition == null) return;

        float distanceToFag = Vector3.Distance(transform.position, randomPatrolFagPosition.position);
        if (distanceToFag < detectionRange && randomPositions.Count > 0)
        {
            randomTarget = randomPositions[Random.Range(0, randomPositions.Count)]; // �����_���^�[�Q�b�g��ݒ�
            isRandomPatrol = true;
        }
    }

    // �ʏ폄��̈ړ�
    private void MoveToPosition()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex];
        MoveTowards(target);

        // �^�[�Q�b�g�n�_�ɓ��B�����玟�̒n�_��
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % positions.Count;
        }
    }

    // �����_������̈ړ�
    private void MoveToRandomPosition()
    {
        if (randomTarget == null) return;

        MoveTowards(randomTarget);

        // �^�[�Q�b�g�n�_�ɓ��B�����烉���_������I��
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            isRandomPatrol = false;
        }
    }

    // �^�[�Q�b�g�Ɍ������Ĉړ�
    private void MoveTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        CurrentDirection = direction; // ���݂̈ړ��������X�V

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        SetAnimationParameters(direction); // �A�j���[�V�����X�V
    }

    private bool IsPlayerInBackField()
    {
        if (player == null) return false;

        // �v���C���[�Ƃ̋������v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > backDetectionDistance) return false;

        // �v���C���[�̕������v�Z
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // �w������i-transform.up�j�Ƃ̊p�x���v�Z
        float angleToPlayer = Vector3.Angle(-transform.up, directionToPlayer);

        // �w��̎���p���ɂ��邩�m�F
        return angleToPlayer <= backFieldOfViewAngle / 2;
    }

    private void CheckForBackDetection()
    {
        if (IsPlayerInBackField())
        {
          
            // �K�v�ɉ����ăA�j���[�V������ǉ��̋����������Őݒ�
            Debug.Log("�U���������: �v���C���[���w��ɂ��܂��I");
        }
    }

    // �v���C���[���w��ɂ���ꍇ�A�U���������
    private IEnumerator TurnAround()
    {
        isMovementStopped = true;

        // �����~�܂��Ă����]�J�n
        yield return new WaitForSeconds(0.5f);

        // �v���C���[�̕������v�Z
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // ��]�̖ڕW�l���v�Z
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

        // ���炩�ɉ�]���鏈��
        float timeToRotate = 1f; // ��]�ɂ����鎞�ԁi�b�j
        float elapsedTime = 0f;

        while (elapsedTime < timeToRotate)
        {
            // ���Ԃ������ĉ�]
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return null; // �t���[�����Ƃɏ����𑱂���
        }

        // �ŏI�I�Ƀ^�[�Q�b�g��]�ɓ��B
        transform.rotation = targetRotation;

        // �����҂��Ă��瓮���ĊJ
        yield return new WaitForSeconds(0.5f);
        isMovementStopped = false;
    }

    // �A�j���[�V�����p�����[�^��ݒ�
    private void SetAnimationParameters(Vector3 direction)
    {
        if (animator == null) return;

        if (direction.magnitude < 0.1f) // �ړ����Ă��Ȃ��ꍇ
        {
            animator.SetBool("IsMoving", false);
            return;
        }

        animator.SetBool("IsMoving", true);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    // �f�o�b�O�p�̎���p��`��
    private void OnDrawGizmos()
    {
        // �����̎���p�̃f�o�b�O�\��
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);


        //// �w�㌟�m�͈͂̃f�o�b�O�\��
        //Gizmos.color = Color.red;
        //Vector3 back = -transform.up;
        //Vector3 backLeftBoundary = Quaternion.Euler(0, 0, backFieldOfViewAngle / 2) * back * backDetectionDistance;
        //Vector3 backRightBoundary = Quaternion.Euler(0, 0, -backFieldOfViewAngle / 2) * back * backDetectionDistance;

        //Gizmos.DrawLine(transform.position, transform.position + backLeftBoundary);
        //Gizmos.DrawLine(transform.position, transform.position + backRightBoundary);
    }
}
