using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player.GimicHitFlg)
        {
            for (int i = 0; i < player.GetInventory().GetSize(); i++)
            {
                foreach (var item in player.GetInventory().GetCurrentInventoryState())
                {
                    // インベントリのアイテムを取得して、ItemFlg をチェック
                    if (item.Value.item.Name == "key")
                    {
                        gameObject.SetActive(false);
                        break; // 一つ見つかったらループを抜ける
                    }
                }
            }
        }
    }
}
