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
    public float spawnProbability = 0.5f; // �����ʒu��ύX����m���i0�`1�͈̔́j

    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^

    [SerializeField]
    private Pathfinding pathfinding;

    private Transform player; // �v���C���[��Transform�Q��
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject); // �V�[���Ԃł��̃I�u�W�F�N�g��ێ�
    //}

    //public bool IsChasingPlayer
    //{
    //    get
    //    {
    //        if (player == null)
    //        {
    //            GameObject playerObject = GameObject.FindWithTag("Player");
    //            if (playerObject != null)
    //            {
    //                player = playerObject.transform;
    //            }
    //            else
    //            {
    //                return false; // �v���C���[��������Ȃ��ꍇ�͒ǐՕs��
    //            }
    //        }
    //        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    //        return distanceToPlayer <= pathfinding.Rafieldofvisionnge; // ��苗�����Ȃ�true
    //    }
    //}

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            pathfinding.TargetPosition = player; // �v���C���[��ǐՑΏۂɐݒ�
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");
        }
        InitializePath();
    }

    private void Update()
    {
        FollowPath();
    }

    public void InitializePath()
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

   
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // �p�X�̍ŏ�����ăX�^�[�g
    }

    public void WarpToPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
        InitializePath(); // �V�����ʒu����o�H���Čv�Z
    }
}
