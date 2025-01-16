using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_hit : MonoBehaviour
{
    public GameObject gimmick;
    // プレイヤーがコライダに入ったときにアクションを実行
    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーのタグを確認（プレイヤーには "Player" タグを付けている前提）
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            // プレイヤーがコライダに入ったときのアクション
            Debug.Log("プレイヤーがコライダに入りました！");
            // ここにアクションを追加する
            // 例: 扉を開ける、ダメージを与えるなど
            gimmick.gameObject.SetActive(true);
        }
    }

    // オプションで、プレイヤーがコライダから出たときにアクションを実行したい場合
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("プレイヤーがコライダから出ました！");
            // ここに終了時のアクションを追加する
            gimmick.gameObject.SetActive(false);
        }
    }
}
