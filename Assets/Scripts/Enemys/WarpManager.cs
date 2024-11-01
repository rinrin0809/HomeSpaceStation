using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarpManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^
    [SerializeField]
    private MoveEnemy moveEnemy; // MoveEnemy�̎Q�Ɓi�G�I�u�W�F�N�g�j

    private void Awake()
    {
        Debug.Log("Start method called in WarpManager.");
        StartCoroutine(StartWarpSequenceWithDelay(2f)); // 2�b�x����Ƀ��[�v�V�[�P���X���J�n
    }

    private IEnumerator StartWarpSequenceWithDelay(float delay)
    {
        Debug.Log("Waiting for " + delay + " seconds before starting warp sequence.");
        yield return new WaitForSeconds(delay); // �w��b���ҋ@
        StartWarpSequence(); // �x����Ƀ��[�v�V�[�P���X���J�n
    }

    private void StartWarpSequence()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null && spawnData.preWarpWaitTimes.Count > 0)
        {
            Debug.Log("Starting warp sequence with multiple delays.");
            StartCoroutine(ExecuteWarpSequence(spawnData)); // ���[�v�V�[�P���X�R���[�`���J�n
        }
        else
        {
            Debug.LogWarning("No spawn data or wait times for scene: " + currentSceneName);
        }
    }

    private IEnumerator ExecuteWarpSequence(SceneSpawnData.SceneWarpData spawnData)
    {
        for (int i = 0; i < spawnData.preWarpWaitTimes.Count; i++)
        {
            float waitTime = spawnData.preWarpWaitTimes[i];
            Debug.Log("Waiting for " + waitTime + " seconds before warp #" + (i + 1));
            yield return new WaitForSeconds(waitTime);

            // �G�����[�v������
            if (moveEnemy != null)
            {
                WarpEnemyToRandomPositionWithDelay(moveEnemy, waitTime); // �x����K�p���ă��[�v
            }
        }
    }

    public void WarpEnemyToRandomPosition(MoveEnemy enemy)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData warpData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (warpData != null && warpData.warpPositions.Count > 0)
        {
            Vector2 randomPosition = warpData.warpPositions[Random.Range(0, warpData.warpPositions.Count)];
            enemy.transform.position = randomPosition; // �G�̈ʒu���X�V
            Debug.Log("Warped to position: " + randomPosition);
        }
        else
        {
            Debug.LogWarning("No spawn data found for scene: " + currentSceneName);
        }
    }

    public void WarpEnemyToRandomPositionWithDelay(MoveEnemy enemy, float delay)
    {
        StartCoroutine(WarpAfterDelay(enemy, delay));
    }

    private IEnumerator WarpAfterDelay(MoveEnemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        WarpEnemyToRandomPosition(enemy);
    }
    //private IEnumerator WarpAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (moveEnemy != null)
    //    {
    //        moveEnemy.WarpToRandomPosition();
    //    }
    //}


    //public void ChangePosition()
    //{
    //    // ���݂̃V�[�������擾
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    // ���݂̃V�[���ɑΉ����� SceneSpawnData ���擾
    //    SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

    //    // ���݂̃V�[�����ɑΉ�������W���X�g�����݂���ꍇ
    //    if (shouldChangePosition && spawnData != null && spawnData.warpPositions.Count > 0)
    //    {
    //        Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
    //        transform.position = randomPosition;
    //        Debug.Log("Position changed to: " + randomPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Spawn positions not found for scene: " + currentSceneName);
    //    }
    //}
}

