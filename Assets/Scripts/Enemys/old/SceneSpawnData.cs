using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSpawnData", menuName = "SpawnData/SceneSpawnData")]
public class SceneSpawnData : ScriptableObject
{
    [System.Serializable]
    public class SceneWarpData
    {
        public string sceneName; // シーン名
        public List<Vector2> warpPositions; // シーンごとのワープ位置リスト
        //public float warpDelay; // ワープの遅延時間（秒単位）
        public List<float> preWarpWaitTimes; // 遅延リスト
    }
    
    public List<SceneWarpData> sceneWarpDataList; // 各シーンごとのデータリスト
}
