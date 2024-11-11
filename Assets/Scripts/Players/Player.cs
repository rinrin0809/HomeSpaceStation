using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    public static Player Instance;
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
    [SerializeField]
    private bool isMoving;

    // テキスト
    public TextMeshProUGUI text;

    // アイテムの近くかどうかのフラグ
    public bool isNearItem = false;
    // 近くのアイテムを入れる

    private GameObject NearItem = null;

    private Item itemData;

    //bool isstaripos = false;

    // 仮インベントリ
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();

    //シーン移動
    public VectorValue startingPosition;

    public List<GameObject> GetItemList
    {
        get { return itemList; }
    }

    public void SetItemList(List<GameObject> List)
    {
        itemList = new List<GameObject>(List);
    }

    public InventryData inventory;

    public InventryData GetInventory()
    {
        return inventory;
    }

    public bool GetisMoving()
    {
        return isMoving;
    }

    [SerializeField]
    private ItemDisplay itemDisplay;

    //鍵を持っている時（仮）
    public bool HasKeyFlg = false;
    //ギミックに当たった時（仮）
    public bool GimicHitFlg = false;

    //フェードアウト
    private FadeOutSceneLoader fadeOutSceneLoader;
    //シーン遷移する判定に当たった時のフラグ
    private bool ChangeSceneFlg = false;

    // プレイヤーの位置と向きを保存するための変数
    [SerializeField]
    private VectorValue playerStorage;  
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();

        //if(isstaripos)
        //{
        //    new Vector3(0, 0, 0);
        //    isstaripos = true;
        //}

        // プレイヤーの向きを設定
        if (playerStorage != null)
        {
            transform.rotation = playerStorage.playerRotation;  // 保存された向きを適用
        }

        if (text != null)
        {
            text.gameObject.SetActive(false);
        }

        stamina = 100.0f;

        if (slider != null)
        {
            slider.value = stamina;
        }

        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
        if (LoadManager.Instance != null)
        {
            if (LoadManager.Instance.NextSceneName != "Title" || LoadManager.Instance.NextSceneName != "Over")
            {
                if(LoadManager.Instance.GetLoadPlayerFlg())
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

                else
                {
                    //シーン遷移先の位置指定
                    if (startingPosition != null)
                    {
                        transform.position = startingPosition.initialValue; // プレイヤーの位置を保存
                        Debug.Log("positio" + transform.position);
                        // playerStorage.playerRotation = transform.rotation; // プレイヤーの向きを保存
                        //プレイヤーの位置を設定
                        //transform.position = startingPosition.initialValue;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //シーン遷移する判定に当たった時のフラグfalseの時かつメニューを開いていない時
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game1")
        {
            if (!ChangeSceneFlg && !MenuManager.Instance.GetOpenFlg())
            {
                //シフトキーが押されたか(コメントアウトしてるのは右のシフトキー)
                //スタミナ最小値より大きい時かつスタミナが0になっていない時
                if (Input.GetKey(KeyCode.LeftShift) /*|| Input.GetKey(KeyCode.RightShift)*/ &&
                    stamina >= minStamina && !zeroStaminaFlg)
                {
                    AnimMove(dashAnimSpeed);
                }

                else
                {
                    AnimMove(animSpeed);
                }
            }

        }

        else
        {
            if (!ChangeSceneFlg)
            {
                //シフトキーが押されたか(コメントアウトしてるのは右のシフトキー)
                //スタミナ最小値より大きい時かつスタミナが0になっていない時
                if (Input.GetKey(KeyCode.LeftShift) /*|| Input.GetKey(KeyCode.RightShift)*/ &&
                    stamina >= minStamina && !zeroStaminaFlg)
                {
                    AnimMove(dashAnimSpeed);
                }

                else
                {
                    AnimMove(animSpeed);
                }
            }
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
            if (itemHolder != null && itemHolder.itemData != null)
            {
                text.text = "アイテムを拾いました";
                itemList.Add(NearItem);
                itemDisplay.PickUpItem(itemHolder.itemData);
                HasKeyFlg = true;
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

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Over")
        {
            //シーン遷移する判定に当たった時のフラグをfalse
            ChangeSceneFlg = false;
        }
    }

    // 一定時間毎に呼ばれる関数
    void FixedUpdate()
    {
        //シーン遷移する判定に当たった時のフラグがtrueの時は処理をしない
        if (ChangeSceneFlg) return;

        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

        //シフトキーが押されたか(コメントアウトしてるのは右のシフトキー)
        //スタミナ最小値より大きい時かつスタミナが0になっていない時
        if (Input.GetKey(KeyCode.LeftShift) /*|| Input.GetKey(KeyCode.RightShift)*/ &&
            stamina > minStamina && !zeroStaminaFlg)
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

    //プレイヤーの向き調整
    public void SavePlayerRotation()
    {
        if (playerStorage != null)
        {
            playerStorage.playerRotation = transform.rotation;  // プレイヤーの向きを保存
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
        if (collision.gameObject.tag == "Key" && text != null)
        {
            text.gameObject.SetActive(false);
            //Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;

        }

        if (collision.gameObject.tag == "Apple" && text != null)
        {
            text.gameObject.SetActive(false);
            //Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;

        }
    }

    // アイテム受け取り
    public void AddItemInventory(Item item)
    {
        if (inventory != null)
        {
            inventory.AddItem(item, true);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("108RoomColl"))
        {
            GimicHitFlg = true;
            Debug.Log(GimicHitFlg);
        }

        if (other.gameObject.CompareTag("108RoomScene"))
        {
            //シーン遷移する判定に当たった時のフラグをtrue
            ChangeSceneFlg = true;
            //フェードアウト後にシーン遷移
            fadeOutSceneLoader.NewGameCallCoroutine("Game2");
            //fadeOutSceneLoader.FadeOutAndChangeRoomScene("Title");

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("108RoomColl"))
        {
            GimicHitFlg = false;
            Debug.Log(GimicHitFlg);
        }
    }
}
