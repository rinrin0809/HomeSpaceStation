using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���g�p���邽�߂̖��O���

public class Bar : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI resultText; // UI �e�L�X�g���A�^�b�`
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
                    DisplayResult("Hit"); // �e�L�X�g�� "Hit" ��\��
                }
                else
                {
                    Debug.Log("Miss");
                    DisplayResult("Miss"); // �e�L�X�g�� "Miss" ��\��
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

    // ���ʂ�\�����郁�\�b�h
    private void DisplayResult(string message)
    {
        resultText.text = message;
        StartCoroutine(ClearResultText()); // ��莞�Ԍ�Ƀe�L�X�g���N���A
    }

    // �e�L�X�g����莞�Ԍ�ɃN���A����R���[�`��
    private IEnumerator ClearResultText()
    {
        yield return new WaitForSeconds(5.0f); // 5.0�b��ɏ���
        resultText.text = "";
    }
}
