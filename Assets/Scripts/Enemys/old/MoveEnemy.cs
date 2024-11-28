using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveEnemy : MonoBehaviour
{
    public Grid grid; // �O���b�h�ւ̎Q��
    public float speed = 5f; // �G�̈ړ����x
    private int targetIndex = 0; // ���݂̃^�[�Q�b�g�m�[�h�̃C���f�b�N�X
    private List<Node> path; // ���݂̒ǐՌo�H
    public Transform playerTransform; // �v���C���[��Transform
    public float chaseRange = 10f; // �ǐՂ��J�n����͈�
    public float stopChaseDelay = 2f; // �ǐՒ�~�܂ł̒x�����ԁi�b�j

    private static bool _isChasing = false; // �ǐՏ�Ԃ�ێ�����t���O

    public EventData Event;

    // �ǐՃt���O�̃Q�b�^�[
    public static bool IsChasing
    {
        get => _isChasing;
        private set => _isChasing = value;
    }

    private float timeSincePlayerExitRange = 0f; // �v���C���[���͈͊O�ɏo�Ă���̌o�ߎ���

    private void Start()
    {
        InitializePath();

        // �V�[���؂�ւ����ɓG���������߂̃C�x���g��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // �V�[�������[�h����邽�тɌĂ΂��C�x���g�̉���
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //private void OnDisable()
    //{
    //    Destroy(gameObject); // �G�������������ۂɍ폜����
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[�����ǂݍ��܂ꂽ��ɌĂ΂��
        //DestroyEnemy();
    }

    private void Update()
    {
        //if (IsChasing)
        //{
        //    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        //    // �ǐՒ�~����
        //    if (distanceToPlayer > chaseRange)
        //    {
        //        timeSincePlayerExitRange += Time.deltaTime;

        //        if (timeSincePlayerExitRange >= stopChaseDelay)
        //        {
        //           // StopChasing(); // �ǐՒ�~
        //        }
        //    }
        //    else
        //    {
        //        timeSincePlayerExitRange = 0f; // �͈͓��Ȃ�^�C�}�[�����Z�b�g
        //    }
        if(Event.GetNameEventFlg("�����e��������"))
        {
            FollowPath();
        }
       // }
    }

    private void InitializePath()
    {
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
    }

    private void FollowPath()
    {
        if (path == null || path.Count == 0) return;

        Vector2 targetPosition = path[targetIndex].Position;
        Vector2 currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;
            if (targetIndex >= path.Count) targetIndex = 0; // ���[�v
        }
    }

    public void StartChasing()
    {
        IsChasing = true;
        timeSincePlayerExitRange = 0f; // �ǐՊJ�n���Ƀ��Z�b�g
        Debug.Log("Enemy started chasing the player!");
    }

    public void StopChasing()
    {
        IsChasing = false;
        timeSincePlayerExitRange = 0f; // �ǐՒ�~���Ƀ^�C�}�[�����Z�b�g


        Debug.Log("Enemy stopped chasing the player.");
    }

    private void DestroyEnemy()
    {
        // �G�����݂���ꍇ�ɍ폜c
        if (gameObject != null)
        {
            // �K�v�ɉ����Ĕ�A�N�e�B�u��
            Destroy(gameObject); // �G���폜
            Debug.Log("Enemy destroyed after scene change.");
        }
    }

    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // �p�X�̍ŏ�����ăX�^�[�g
    }
}