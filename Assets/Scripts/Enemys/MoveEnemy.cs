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

    private void Start()
    {
        InitializePath();
    }

    private void Update()
    {
        FollowPath();
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

   
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // �p�X�̍ŏ�����ăX�^�[�g
    }
}
