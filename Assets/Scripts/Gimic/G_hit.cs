using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_hit : MonoBehaviour
{
    public SelectGimmick selectGimmickScript;
    // ギミックを制御するための参照
    public GameObject gimmick;

    // プレイヤーがコライダに入ったかどうかを判定するフラグ
    private bool isPlayerInRange = false;

    // プレイヤーがコライダに入ったときの処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがコライダに入ったとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("プレイヤーがコライダに入った！");
        }
    }

    // プレイヤーがコライダから出たときの処理
    private void OnTriggerExit2D(Collider2D other)
    {
        // プレイヤーがコライダから出たとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("プレイヤーがコライダから出た！");
        }
    }

    // Updateは毎フレーム呼ばれる
    private void Update()
    {
        // プレイヤーがコライダに入っていて、スペースキーが押された場合
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");

            // ギミックをアクティブにする
            if (gimmick != null)
            {
                gimmick.SetActive(true);
            }
        }
        if (selectGimmickScript != null && selectGimmickScript.Ans)
        {
            Time.timeScale = 1;
            gimmick.SetActive(false); // ギミックを非表示にする
        }
    }
}
