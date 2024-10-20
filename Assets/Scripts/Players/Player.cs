using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 移動
    public float moveSpeed; // 移動速度
    private float dashMoveSpeed = 450.0f;
    private float horizontal; // x
    private float vertical; // y

    //スタミナ
    [SerializeField]
    private float stamina;
    private float maxStamina = 100.0f;
    private float minStamina = 0.0f;
    //スタミナを減らす速度
    private float staminaSpeed = 50.0f;

    //スタミナが0になった時のフラグ
    private bool zeroStaminaFlg = false;

    //スタミナゲージ
    public Slider slider;

    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 moveDir;

    // アニメーション
    [SerializeField]
    private Animator animatior;
    [SerializeField]
    private float animSpeed = 1.0f;
    private float dashAnimSpeed = 2.0f;
    private bool isMoving;

    // テキスト
    public TextMeshProUGUI text;

    // アイテムの近くかどうかのフラグ
    public bool isNearItem = false;
    // 近くのアイテムを入れる
    
    private GameObject NearItem = null;

    private Item itemData;

    // 仮インベントリ
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();

    public InventryData inventory;
    [SerializeField]
    private ItemDisplay itemDisplay;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();

        if(text != null)
        {
            text.gameObject.SetActive(false);
        }

        stamina = 100.0f;

        if (slider != null)
        {
            slider.value = stamina;
        }

        //ゲームシーンの時
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if(LoadManager.Instance != null)
            {
                //NewGameボタンが押された時のフラグ
                if (LoadManager.Instance.NewGamePushFlg)
                {
                    //初期位置の設定
                    Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    Transform objectTransform = gameObject.GetComponent<Transform>();
                    objectTransform.position = targetPosition;
                    Debug.Log(objectTransform.position);
                }

                //LoadGameボタンが押された時のフラグ
                else
                {
                    // セーブデータを読み込み、プレイヤーの位置を設定
                    LoadManager.Instance.TitleToGameLoadData();
                    Debug.Log("Load");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Hキーが押されたか確認 (KeyCode.H はHキー)
        if (Input.GetKey(KeyCode.H) && stamina >= minStamina && !zeroStaminaFlg)
        {
            AnimMove(dashAnimSpeed);
        }

        else
        {
            AnimMove(animSpeed);
        }

        if (slider != null)
        {
            slider.value = stamina;
        }

        if (stamina == minStamina)
        {
            zeroStaminaFlg = true;
        }

        else if (stamina == maxStamina)
        {
            zeroStaminaFlg = false;
        }

        if (stamina < minStamina)
        {
            stamina = minStamina;
        }

        else if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if (/*isNearItem = true && */Input.GetKeyDown(KeyCode.E) && NearItem != null)
        {
            ItemDisplay itemHolder = NearItem.GetComponent<ItemDisplay>();
            if (itemHolder!=null &&itemHolder.itemData!=null)
            {
                text.text = "アイテムを拾いました";
                itemList.Add(NearItem);
                itemDisplay.PickUpItem(itemHolder.itemData);

                NearItem.gameObject.SetActive(false);
            }
            else
            {
                text.text = "アイテムがありません";
            }
            //Debug.Log(NearItem.name + "を受け取ります");


            //inventory.AddItem()
            //Destroy(NearItem);

        }
       
    }
    // 一定時間毎に呼ばれる関数
    void FixedUpdate()
    {
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

        // Hキーが押されたか確認 (KeyCode.H はHキー)
        if (Input.GetKey(KeyCode.H) && stamina > minStamina && !zeroStaminaFlg)
        {
            //移動処理
            Move(dashMoveSpeed, Time.fixedDeltaTime);

            if (horizontal != 0 || vertical != 0)
            {
                if (stamina > minStamina)
                {
                    stamina -= staminaSpeed * Time.fixedDeltaTime;
                }
            }

            else
            {
                //Hキーが押されている時にスタミナを減らしたくなければコメントアウト
                stamina += staminaSpeed * Time.fixedDeltaTime;
            }
        }

        else
        {
            //移動処理
            Move(moveSpeed, Time.fixedDeltaTime);

            if (stamina < maxStamina)
            {
                stamina += staminaSpeed * Time.fixedDeltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(true);
            text.text = "Eボタンでアイテムを拾う";
            
            NearItem = collision.gameObject;
         
        }

        if (collision.gameObject.tag == "Apple")
        {
            //Debug.Log("アップルがある");
            text.gameObject.SetActive(true);
            text.text = "Eボタンでアイテムを拾う";
            NearItem = collision.gameObject;
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(false);
            Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;
            
        }

        if (collision.gameObject.tag == "Apple")
        {
            text.gameObject.SetActive(false);
            Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;

        }
    }

    // アイテム受け取り
    public void AddItemInventory(Item item)
    {
        if (inventory != null)
        {
            inventory.AddItem(item);
            Debug.Log(item.Name + "がインベントリに追加されました");
        }
        else
        {
            Debug.Log("インベントリが設定されてない");
        }
    }

    //アニメーションの再生処理
    private void AnimMove(float AnimSpeed)
    {
        // アニメーションの再生スピード
        animatior.speed = AnimSpeed;

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

    //移動処理
    private void Move(float moveSpeed, float deltaTime)
    {
        rb.velocity = moveDir * moveSpeed * deltaTime;
    }
}
