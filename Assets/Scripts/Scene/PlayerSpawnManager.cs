using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    public  static string spawnPointTag = "PlayerSpawnPosition"; // 初期位置を設定するオブジェクトのタグ

    private void Awake()
    {
        // プレイヤーを探す
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // "SpawnPoint" タグのオブジェクトを探す
            GameObject spawnPoint = GameObject.FindWithTag(spawnPointTag);
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
                Debug.Log($"プレイヤーを初期ポジション {spawnPoint.transform.position} に移動しました");
            }
            else
            {
                Debug.LogWarning($"タグ '{spawnPointTag}' のオブジェクトが見つかりませんでした");
            }
        }
        else
        {
            Debug.LogWarning("プレイヤーが見つかりませんでした");
        }
    }

}
