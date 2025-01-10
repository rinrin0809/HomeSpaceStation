using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkilCheck : MonoBehaviour
{
    //�X�R�A
    [SerializeField] public int Score;
    //�X�R�A�̏��
    public int MAX_SCORE = 100;
    [SerializeField] private TextMeshProUGUI resultText; // UI �e�L�X�g���A�^�b�`

    //�������X�R�A�i���Ԍo�߁j
    private int AddScore = 1;
    // ���Z����܂ł̎���
    private float MAX_ADD_TIMER = 2.0f;
    private float AddTimer = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍo�߂ŃX�R�A�����Z
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

    // ���ʂ�\�����郁�\�b�h
    public void DisplayResult(string message)
    {
        resultText.text = message;
        //StartCoroutine(ClearResultText()); // ��莞�Ԍ�Ƀe�L�X�g���N���A
    }

    // �e�L�X�g����莞�Ԍ�ɃN���A����R���[�`��
    public IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0�b��ɏ���
        resultText.text = "";
    }

    //���Ԍo�߂ŃX�R�A�����Z
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
