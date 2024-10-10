using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // �ړ�
    public float moveSpeed; // �ړ����x
    private float horizontal; // x
    private float vertical; // y

    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 moveDir;

    // �A�j���[�V����
    [SerializeField]
    private Animator animatior;
    [SerializeField]
    private float animSpeed = 1.0f;
    private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // �A�j���[�V�����̍Đ��X�s�[�h
        animatior.speed = animSpeed;

        // ���ړ�
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // �΂߈ړ����Ȃ��悤�ɂ���
        if (horizontal != 0)
        {
            vertical = 0;
        }
        moveDir = new Vector3(horizontal, vertical).normalized;

        if (horizontal != 0 || vertical != 0)
        {
            //Debug.Log(isMoving);
            animatior.SetFloat("InputX", horizontal);
            animatior.SetFloat("InputY", vertical);
            if (!isMoving)
            {
                isMoving = true;
                animatior.SetBool("IsMoving", isMoving);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animatior.SetBool("IsMoving", isMoving);
            }
        }
    }
    // ��莞�Ԗ��ɌĂ΂��֐�
    void FixedUpdate()
    {
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

    }

    // �����蔻��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("��������܂�");
        }
        //if (collision.gameObject.CompareTag("Key"))
        //{
        //    Debug.Log("��������܂�");
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("��������܂���");
        }
    }

}
