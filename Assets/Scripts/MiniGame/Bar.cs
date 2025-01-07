using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���g�p���邽�߂̖��O���

public class Bar : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI resultText; // UI �e�L�X�g���A�^�b�`
    //�ړ����x
    private float MIN_MOVE_SPEED = 3.0f;
    private float MAX_MOVE_SPEED = 10.0f;
    private float MoveSpeed = 0.0f;
    //�������]�p
    private int direction = 1;
    //����p�t���O
    private bool isCheck;
    //���͂��ꂽ���̃t���O
    private bool PushFlg = false;
    //�X�R�A
    private int Score = 0;
    //������鑬�x
    private int AddScore = 1;
    //�X�R�A�̏��
    private int MAX_SCORE = 100;
    // ��莞�ԁi�b�j
    public float DelayTime = 500.0f;
    // ���Z������������܂ł̎���
    public float Duration = 100.0f;
    // ���Z����܂ł̎���
    private float MAX_ADD_TIMER = 2.0f;
    private float AddTimer = 2.0f;
    //���
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

    // ���ʂ�\�����郁�\�b�h
    private void DisplayResult(string message)
    {
        resultText.text = message;
        //StartCoroutine(ClearResultText()); // ��莞�Ԍ�Ƀe�L�X�g���N���A
    }

    // �e�L�X�g����莞�Ԍ�ɃN���A����R���[�`��
    private IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0�b��ɏ���
        resultText.text = "";
    }

    //�ړ����x�������_���ŕύX
    private void RandMoveSpeed()
    {
        MoveSpeed = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);
        Debug.Log("MoveSpeed" + MoveSpeed);
    }
}
