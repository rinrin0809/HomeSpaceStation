using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro を使用するための名前空間

public class Bar : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI resultText; // UI テキストをアタッチ
    private float MoveSpeed = 2.0f;
    private int direction = 1;
    private bool isCheck;
    private bool PushFlg = false;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position,
            transform.localScale / 2.0f, 0f, groundLayer);

        if (hit)
        {
            isCheck = true;
        }
        else
        {
            isCheck = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!PushFlg)
            {
                if (isCheck)
                {
                    Debug.Log("Hit");
                    DisplayResult("Hit"); // テキストに "Hit" を表示
                }
                else
                {
                    Debug.Log("Miss");
                    DisplayResult("Miss"); // テキストに "Miss" を表示
                }

                PushFlg = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            PushFlg = false;
        }
    }

    private void FixedUpdate()
    {
        if(!PushFlg)
        {
            if (transform.position.y >= 2.45) direction = -1;

            if (transform.position.y <= -2.45) direction = 1;

            transform.position = new Vector3(0,
                transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, 0);
        }
    }

    // 結果を表示するメソッド
    private void DisplayResult(string message)
    {
        resultText.text = message;
        StartCoroutine(ClearResultText()); // 一定時間後にテキストをクリア
    }

    // テキストを一定時間後にクリアするコルーチン
    private IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0秒後に消す
        resultText.text = "";
    }
}
