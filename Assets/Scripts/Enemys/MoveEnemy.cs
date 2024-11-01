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
    public Vector2 pos;
    public float k = 0;
    public float spawnProbability = 0.5f; // �����ʒu��ύX����m���i0�`1�͈̔́j
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^
    // �ړ��\�ȍ��W�̃��X�g
    //private List<Vector2> spawnPositions = new List<Vector2>
    //{
    //    new Vector2(-4, -5),
    //    new Vector2(-4, 4),
    //    new Vector2(4, 4)
    //};


    private void Start()
    {
        SetInitialPosition();
        InitializePath();

        //// 50%�̊m���ŏ����ʒu��ύX����
        //bool shouldChangePosition = Random.value > k;

        //if (shouldChangePosition && sceneSpawnData != null && sceneSpawnData.spawnPositions.Count > 0)
        //{
        //    // ScriptableObject���烉���_���Ȉʒu��I��Ŕz�u
        //    Vector2 randomPosition = sceneSpawnData.spawnPositions[Random.Range(0, sceneSpawnData.spawnPositions.Count)];
        //    transform.position = randomPosition;
        //    Debug.Log("Position changed to: " + randomPosition);
        //}
        //else
        //{
        //    Debug.Log("Position not changed, using initial position.");
        //}

        //// �����p�X�̐ݒ�
        //if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        //{
        //    path = grid.FinalPath;
        //    targetIndex = 0;
        //}
    }

    private void Update()
    {
        FollowPath();
        //// �p�X�����݂��邩���m�F
        //if (path != null && path.Count > 0)
        //{
        //    // ���݂̃^�[�Q�b�g�m�[�h�Ɍ������Ĉړ�
        //    Vector2 targetPosition = path[targetIndex].Position;
        //    Vector2 currentPosition = transform.position;

        //    // �^�[�Q�b�g�m�[�h�ւ̈ړ�
        //    Vector2 direction = (targetPosition - currentPosition).normalized;
        //    transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        //    // �m�[�h�ɓ��B�����玟�̃m�[�h�ֈړ�
        //    if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        //    {
        //        targetIndex++; // ���̃m�[�h��

        //        // �Ō�̃m�[�h�ɓ��B������ǐՂ��I�����邩���[�v���J��Ԃ�
        //        if (targetIndex >= path.Count)
        //        {
        //            targetIndex = 0; // ���[�v���čĂэŏ��̃m�[�h��
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("No path to follow.");
        //}
    }

    // �����ʒu��ύX���郁�\�b�h
    private void SetInitialPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (warpData != null && warpData.warpPositions.Count > 0)
        {
            bool shouldChangePosition = Random.value < spawnProbability;
            if (shouldChangePosition)
            {
                Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
                transform.position = randomPosition;
               // Debug.Log("Position changed to: " + randomPosition);
            }
            else
            {
               // Debug.Log("Position not changed, using initial position.");
            }
        }
        else
        {
            //Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
        }
    }

    // �����p�X�̐ݒ�
    private void InitializePath()
    {
        if (grid.FinalPath != null && grid.FinalPath.Count > 0)
        {
            path = grid.FinalPath;
            targetIndex = 0;
        }
        else
        {
           // Debug.LogWarning("No initial path found.");
        }
    }

    // �p�X�ɏ]���Ĉړ����郁�\�b�h
    private void FollowPath()
    {
        if (path == null || path.Count == 0)
        {
           // Debug.Log("No path to follow.");
            return;
        }

        Vector2 targetPosition = path[targetIndex].Position;
        Vector2 currentPosition = transform.position;

        // �^�[�Q�b�g�m�[�h�ւ̈ړ�
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        // �m�[�h�ɓ��B�����玟�̃m�[�h�ֈړ�
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;

            // �Ō�̃m�[�h�ɓ��B������ǐՂ����[�v����
            if (targetIndex >= path.Count)
            {
                targetIndex = 0;
            }
        }
    }

    // �O������p�X���X�V���郁�\�b�h
    public void UpdatePath(List<Node> newPath)
    {
        path = newPath;
        targetIndex = 0; // �p�X�̍ŏ�����ăX�^�[�g
    }
}
