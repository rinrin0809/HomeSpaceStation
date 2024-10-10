using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 移動
    public float moveSpeed; // 移動速度
    private float horizontal; // x
    private float vertical; // y

    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 moveDir;

    // アニメーション
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
        // アニメーションの再生スピード
        animatior.speed = animSpeed;

        // 仮移動
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // 斜め移動しないようにする
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
    // 一定時間毎に呼ばれる関数
    void FixedUpdate()
    {
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

    }

    // 当たり判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("鍵があります");
        }
        //if (collision.gameObject.CompareTag("Key"))
        //{
        //    Debug.Log("鍵があります");
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("鍵がありません");
        }
    }

}
