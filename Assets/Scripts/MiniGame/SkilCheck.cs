using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkilCheck : MonoBehaviour
{
    //スコア
    [SerializeField] public int Score;
    //スコアの上限
    public int MAX_SCORE = 100;
    [SerializeField] private TextMeshProUGUI resultText; // UI テキストをアタッチ

    //足されるスコア（時間経過）
    private int AddScore = 1;
    // 加算するまでの時間
    private float MAX_ADD_TIMER = 2.0f;
    private float AddTimer = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //時間経過でスコアを加算
        TimeAddScore();

        if (Score <= MAX_SCORE)
        {
            DisplayResult(Score.ToString());
        }

        else
        {
            DisplayResult("Clear");
        }

        if (Player.Instance != null) Player.Instance.Score = Score;
    }

    // 結果を表示するメソッド
    public void DisplayResult(string message)
    {
        resultText.text = message;
        //StartCoroutine(ClearResultText()); // 一定時間後にテキストをクリア
    }

    // テキストを一定時間後にクリアするコルーチン
    public IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0秒後に消す
        resultText.text = "";
    }

    //時間経過でスコアを加算
    private void TimeAddScore()
    {
        if (Score < MAX_SCORE)
        {
            AddTimer -= Time.deltaTime;

            if (AddTimer <= 0.0f)
            {
                Score += AddScore;
                AddTimer = MAX_ADD_TIMER;
            }
        }
    }
}
