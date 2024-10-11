using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // テキスト
    public TextMeshProUGUI text;

    // アイテムの近くかどうかのフラグ
    private bool isNearItem = false;
    // 近くのアイテムを入れる
    private GameObject NearItem = null;


    // 仮インベントリ
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    private List<Item> itemData = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();
        text.gameObject.SetActive(false);
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

        if (isNearItem = true && Input.GetKeyDown(KeyCode.E)&&NearItem!=null)
        {
            text.text = "アイテムを拾いました";
            itemList.Add(NearItem);  
        }
        else
        {
            text.text = "アイテムがありません";
        }
    }
    // 一定時間毎に呼ばれる関数
    void FixedUpdate()
    {
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(true);
            text.text = "Eボタンでアイテムを拾う";
            isNearItem = true;
            NearItem = collision.gameObject;

            //if(Input.GetKeyDown(KeyCode.E))
            //{
            //    Debug.Log("鍵をポケットに入れます");
            //    itemList.Add(collision.gameObject);
            //    //Destroy(collision.gameObject);
            //}
           
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(false);
            Debug.Log("鍵がありません");
            isNearItem = false;
            NearItem = null;
        }
    }

}
