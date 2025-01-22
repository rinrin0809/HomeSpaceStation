using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> positions; // �ʏ폄��|�C���g
    public List<Transform> randomPositions; // �����_������|�C���g
    public float speed = 2f; // �ړ����x
    public float randomPatrolChance = 0.3f; // �����_������̊m��
    public float fieldOfViewAngle = 90f; // �O������p
    public float detectionDistance = 5f; // �v���C���[���m����
  
    public Transform player; // �v���C���[��Transform
    public Fov_script fovScript; // ���쐧��p�X�N���v�g

    private int currentTargetIndex = 0; // ���݂̏���|�C���g
    private Transform randomTarget; // �����_������̃^�[�Q�b�g
    private bool isRandomPatrol = false; // ���݃����_�����񒆂��ǂ���
    private Vector3 currentDirection; // ���݂̈ړ�����
   
    private bool isStopped = false; // ��~�����ǂ���
    [SerializeField]
    private float stopDuration = 2f; // ��~����
    private float stopTimer = 0f; // ��~�^�C�}�[

    [SerializeField]
    private Animator animator;

    private float detectionHoldTime = 0.2f; // �v���C���[�����F���Ă����Ԃ�ێ����鎞��
    private float detectionTimer = 0f; // ���F��Ԃ̕ێ��^�C�}�[

   
    bool isPlayerVisible;

    [SerializeField]
    private Transform reversePoint; // ���񔽓]�|�C���g

    private void Update()
    {
        // ��~���̏���
        if (isStopped)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
                ResumePatrolling();
            }
            //Debug.Log($"��~��: isStopped={isStopped}, stopTimer={stopTimer}");
            return; // ��~���͂���ȏ�̏������s��Ȃ�
        }

        isPlayerVisible = fovScript != null && fovScript.IsPlayerInFieldOfView(player, transform, fieldOfViewAngle, detectionDistance);
        // ���F�`�F�b�N
        if (isPlayerVisible)
        {
            StopAndDetectPlayer();
            return; // ���F�����ꍇ�A���̏����𒆒f
        }
      

        // ���񏈗�
        if (isRandomPatrol)
        {
            RandomPatrolMovement();
        }
        else
        {
            PatrolMovement();
        }
    }


    // �ʏ폄�񏈗�
    private void PatrolMovement()
    {
        if (positions.Count == 0) return;

        Transform target = positions[currentTargetIndex]; // ���̏���|�C���g
        MoveTowards(target); // �^�[�Q�b�g�Ɍ������Ĉړ�

        // �^�[�Q�b�g�n�_�ɓ��B�����玟�̒n�_�ɐi��
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            //currentTargetIndex = (currentTargetIndex + 1) % positions.Count;

            // �����^�[�Q�b�g�� reversePoint �Ɉ�v����Ȃ珄�񃋁[�g�𔽓]
            if (reversePoint != null && target.position == reversePoint.position)
            {
                positions.Reverse(); // ���񃋁[�g�𔽓]
                currentTargetIndex = 1; // ���]��̎��̃|�C���g��
                Debug.Log("���񃋁[�g�𔽓]�I");
            }
            else
            {
                // ���]���Ȃ��ꍇ�͒ʏ�̏���𑱍s
                currentTargetIndex = (currentTargetIndex + 1) % positions.Count;
            }
        }
    }

    // �����_�����񏈗�
    private void RandomPatrolMovement()
    {
        if (randomTarget == null) return;

        MoveTowards(randomTarget); // �����_���^�[�Q�b�g�Ɍ������Ĉړ�

        // �^�[�Q�b�g�n�_�ɓ��B�����烉���_������I��
        if (Vector3.Distance(transform.position, randomTarget.position) < 0.1f)
        {
            isRandomPatrol = false; // �ʏ폄��ɖ߂�
            randomTarget = null;
        }
    }

    // �^�[�Q�b�g�Ɍ������Ĉړ�
    private void MoveTowards(Transform target)
    {
        // �^�[�Q�b�g�ւ̕������v�Z
        Vector3 direction = (target.position - transform.position).normalized;

        //// ����̕������X�V
        if (fovScript != null)
        {
            fovScript.CurrentDirection = direction; // ���C�L���X�g�������X�V
            fovScript.UpdateVisionDirection(direction);
        }
        // �p�g���[�����Ɏ�����X�V
        if (fovScript != null)
        {
            fovScript.UpdateVisionDirection(direction); // ���݂̈ړ�����������ɔ��f
        }


        // �^�[�Q�b�g�Ɍ������Ĉړ�
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // ���݂̈ړ��������X�V
        UpdateAnimationDirection(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == reversePoint)
        {
           
            // �Փˎ��ɉ�������̏��������s�������ꍇ�́A�����ɋL�q
            Debug.Log("����������t�]���܂���");
        }
    }
    // �A�j���[�V�����̕������X�V
    private void UpdateAnimationDirection(Vector3 direction)
    {
        if (animator != null)
        {
            // X, Y�����̒l���A�j���[�^�[�ɐݒ�
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);

            // �ړ����t���O��L���ɂ���
            animator.SetBool("IsMoving", direction.magnitude > 0);
        }
    }

    // �����_��������J�n���邩����
    private void CheckRandomPatrolStart()
    {
        if (randomPositions.Count == 0 || isRandomPatrol) return;

        // ���m���Ń����_��������J�n
        if (Random.value < randomPatrolChance)
        {
            randomTarget = randomPositions[Random.Range(0, randomPositions.Count)];
            isRandomPatrol = true;
        }
    }

    // �v���C���[�����F�����ۂɒ�~�������J�n
    private void StopAndDetectPlayer()
    {
        isStopped = true; // ��~�t���O��ݒ�
        stopTimer = stopDuration; // ��~�^�C�}�[�����Z�b�g

        // Debug.Log($"��~�������J�n���܂���: isStopped={isStopped}, stopTimer={stopTimer}");
      
        // �A�j���[�V�������~��Ԃɐݒ�
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
        Debug.Log($"��~: {stopDuration}�b");
    }

  
    // ��~�I����ɏ�����ĊJ
    private void ResumePatrolling()
    {
        Debug.Log("������ĊJ");
    }
}
