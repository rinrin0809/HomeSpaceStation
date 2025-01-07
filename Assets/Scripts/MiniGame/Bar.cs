using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro を使用するための名前空間

public class Bar : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI resultText; // UI テキストをアタッチ
    //移動速度
    private float MIN_MOVE_SPEED = 3.0f;
    private float MAX_MOVE_SPEED = 10.0f;
    private float MoveSpeed = 0.0f;
    //符号反転用
    private int direction = 1;
    //判定用フラグ
    private bool isCheck;
    //入力された時のフラグ
    private bool PushFlg = false;
    //スコア
    private int Score = 0;
    //足される速度
    private int AddScore = 1;
    //スコアの上限
    private int MAX_SCORE = 100;
    // 一定時間（秒）
    public float DelayTime = 500.0f;
    // 加算を完了させるまでの時間
    public float Duration = 100.0f;
    // 加算するまでの時間
    private float MAX_ADD_TIMER = 2.0f;
    private float AddTimer = 2.0f;
    //上限
    public float LimitPosY = 5.0f;
    private void Start()
    {
        RandMoveSpeed();
    }

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position,
            transform.localScale / 2.0f, 0f, groundLayer);

        if (Score < MAX_SCORE)
        {
            AddTimer -= Time.deltaTime;

            if (AddTimer <= 0.0f)
            {
                Score += AddScore;
                AddTimer = MAX_ADD_TIMER;
            }
            DisplayResult(Score.ToString());
        }

        else
        {
            DisplayResult("Clear");
        }


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

                    Score += 5;

                    if (Score > MAX_SCORE) Score = 100;
                }
                else
                {
                    Debug.Log("Miss");
                    Score -= 5;

                    if (Score < 0) Score = 0;
                }

                PushFlg = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PushFlg = false;
            RandMoveSpeed();
        }

        if (Player.Instance != null) Player.Instance.Score = Score;
    }

    private void FixedUpdate()
    {
        if (!PushFlg)
        {
            if (transform.position.y >= LimitPosY) direction = -1;

            if (transform.position.y <= -LimitPosY) direction = 1;

            transform.position = new Vector3(0,
                transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, 0);
        }
    }

    // 結果を表示するメソッド
    private void DisplayResult(string message)
    {
        resultText.text = message;
        //StartCoroutine(ClearResultText()); // 一定時間後にテキストをクリア
    }

    // テキストを一定時間後にクリアするコルーチン
    private IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0秒後に消す
        resultText.text = "";
    }

    //移動速度をランダムで変更
    private void RandMoveSpeed()
    {
        MoveSpeed = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);
        Debug.Log("MoveSpeed" + MoveSpeed);
    }
}
