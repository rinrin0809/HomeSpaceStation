using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;        //シーン名で遷移先判別
    public Vector2 playerPosition;    //シーン遷移後のプレイヤー位置
    public VectorValue playerStorage; //プレイヤーの位置を保存

    private Player player;

    [SerializeField]
    public string ItemName = "";
    // 鍵が必要かどうかを判別するフラグ
    public bool requiresKey = false;

    //シーン移動
    private VectorValue startingPosition;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerStorage.initialValue = new Vector3(0.0f, -4.0f, 0.0f);
    }

    //シーン遷移
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // 鍵が必要かどうかチェック
            if (!requiresKey || (requiresKey && HasRequiredItem()))
            {
                // プレイヤーの位置を保存し、シーンを切り替え
                playerStorage.initialValue = playerPosition;
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("鍵を持っていないため、シーン遷移できません。");
            }
        }
    }

    // 必要なアイテムを持っているか確認
    private bool HasRequiredItem()
    {
        if (player != null)
        {
            for (int i = 0; i < player.GetInventory().GetSize(); i++)
            {
                foreach (var item in player.GetInventory().GetCurrentInventoryState())
                {
                    if (item.Value.item.Name == ItemName)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}