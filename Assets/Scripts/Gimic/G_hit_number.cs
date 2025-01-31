using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_hit_number : MonoBehaviour
{
    public InputNumber inputumber;
    public GameObject gimmick;
    public GameObject kabe;
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

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");

            // ギミックをアクティブにする
            if (gimmick != null)
            {
                gimmick.SetActive(true);
            }
        }
        if (/*inputumber != null &&*/ inputumber.Ans || Input.GetKeyDown(KeyCode.Backspace))
        {
            Time.timeScale = 1;
            gimmick.SetActive(false); // ギミックを非表示にする
            kabe.SetActive(false);
        }
    }
}
