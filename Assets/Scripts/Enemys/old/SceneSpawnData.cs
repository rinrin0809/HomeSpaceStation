using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSpawnData", menuName = "SpawnData/SceneSpawnData")]
public class SceneSpawnData : ScriptableObject
{
    [System.Serializable]
    public class SceneWarpData
    {
        public string sceneName; // �V�[����
        public List<Vector2> warpPositions; // �V�[�����Ƃ̃��[�v�ʒu���X�g
        //public float warpDelay; // ���[�v�̒x�����ԁi�b�P�ʁj
        public List<float> preWarpWaitTimes; // �x�����X�g
    }
    
    public List<SceneWarpData> sceneWarpDataList; // �e�V�[�����Ƃ̃f�[�^���X�g
}
