using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarpManager : MonoBehaviour
{
    [SerializeField]
    private SceneSpawnData sceneSpawnData; // �V�[�����Ƃ̃X�|�[���f�[�^

    public bool shouldChangePosition = true;
    void Start()
    {
        Debug.Log("Waiting to start warp coroutine.");
        // �����Ŏw�肵�����Ԃ��o�߂�����ɃR���[�`�����Ăяo��
        Invoke("StartWarpCoroutineWithDelay", 2f); // 2�b�̒x���ŊJ�n
    }

    private void StartWarpCoroutineWithDelay()
    {
        Debug.Log("Starting DelayedChangePosition coroutine after initial delay.");
        StartCoroutine(DelayedChangePosition());
    }

    private IEnumerator DelayedChangePosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData.SceneWarpData spawnData = sceneSpawnData.sceneWarpDataList.Find(data => data.sceneName == currentSceneName);

        if (spawnData != null)
        {
            Debug.Log("Scene delay: " + spawnData.preWarpWaitTime);
            yield return new WaitForSeconds(spawnData.preWarpWaitTime);

            Debug.Log("Delay completed, executing warp.");

            if (shouldChangePosition && spawnData.warpPositions.Count > 0)
            {
                Vector2 randomPosition = spawnData.warpPositions[Random.Range(0, spawnData.warpPositions.Count)];
                transform.position = randomPosition;
                Debug.Log("Warped to position: " + randomPosition);
            }
            else
            {
                Debug.LogWarning("No spawn positions found for scene: " + currentSceneName);
            }
        }
        else
        {
            Debug.LogWarning("No spawn data for scene: " + currentSceneName);
        }
    }

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

