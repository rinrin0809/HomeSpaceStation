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

    //イベント
    public EventData Event;

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
    private Animator animator;
    [SerializeField]
    private float animSpeed = 1.0f;
    private float dashAnimSpeed = 2.0f;
    [SerializeField]
    private bool isMoving;

    // テキスト
    //public TextMeshProUGUI text;

    // アイテムの近くかどうかのフラグ
    public bool isNearItem = false;
    // 近くのアイテムを入れる

    private GameObject NearItem = null;

    private Item itemData;

    //bool isstaripos = false;

    // 仮インベントリ
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();

    public ActionEvent actionEvent;

    //シーン移動
    public VectorValue startingPosition;

    private bool startflg = true;

    public bool ClearExitFlg = false;
    //シーン遷移
    //public Transform targetPosition; // Inspectorで設定する場合
    //[SerializeField]
    //private string NextSceneObject = "";
    //[SerializeField]
    //private string NextScene = "";

    //スキルチェックのスコア
    [SerializeField]
    public int Score = 0;

    //プレイヤーのセーブデータを更新するフラグ
    public bool UpdateSaveDataFlg = false;
    //更新するフラグ(trueの時は更新処理をするフラグ)
    public bool UpdateFlg = true;
    //ミニゲームの時に位置を設定するフラグ
    [SerializeField]
    bool MiniGameFlg = false;
    //ミニゲームの時の位置
    Vector3 MiniGamePos = new Vector3(0.0f, 0.0f, 0.0f);
    //actionEventの取得制限用フラグ
    private bool GetActFlg = true;
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
    public ItemDisplay GetItemDisplay
    {
        get { return itemDisplay; }
    }
    //ギミックに当たった時（仮）
    public bool GimicHitFlg = false;

    //フェードアウト
    private FadeOutSceneLoader fadeOutSceneLoader;
    //シーン遷移する判定に当たった時のフラグ
    public bool ChangeSceneFlg = false;

    // プレイヤーの位置と向きを保存するための変数
    [SerializeField]
    private VectorValue playerStorage;

    //看板とかに当たった時に動きを止めるフラグ（今後データ化予定）
    public bool ExplainDisplayFlg = false;

    Transform objectTransform;
    Vector3 targetPosition;

    //NewGameが押された時に初期位置を設定するフラグ
    public bool NewGameSpownFlg = false;
    public bool SkilFlg = false;

    private void Awake()
    {
        // 既にインスタンスが存在する場合、重複したオブジェクトを破棄する
        if (Instance != null)
        {
            Destroy(gameObject); // 重複している場合は削除
        }
        else
        {
            Instance = this; // インスタンスが設定されていない場合、現在のオブジェクトをインスタンスとして設定
            DontDestroyOnLoad(gameObject); // シーンをまたいでこのオブジェクトを保持する
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //デバッグ用
        Event.Initialize();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        targetPosition = new Vector3(0.0f, -4.0f, 0.0f);

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

        //if (playerStorage != null)
        //{
        //    Debug.Log("Playerの初期位置: " + playerStorage.initialValue); // ここでデバッグ
        //    //startingPosition.initialValue = targetPosition;
        //    transform.position = playerStorage.initialValue;
        //}

        if (playerStorage != null)
        {
            Debug.Log("Playerの初期位置: " + playerStorage.initialValue);
            transform.position = playerStorage.initialValue; // 保存された初期位置に移動
        }

        //if (text != null)
        //{
        //    text.gameObject.SetActive(false);
        //}

        stamina = 100.0f;

        
        objectTransform = gameObject.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!UpdateFlg) return;

        //初期位置の設定
        NewGameSpownPlayer();

        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();

        if (SceneManager.GetActiveScene().name == "Floor(B1)")
        {
            if(GetActFlg)
            {
                actionEvent = FindObjectOfType<ActionEvent>();
                GetActFlg = false;
            }
        }

        // タグ "SliderTag" が付いたすべてのオブジェクトを取得
        GameObject[] sliderObjects = GameObject.FindGameObjectsWithTag("StaminaGause");

        // オブジェクトが見つかったかチェック
        if (sliderObjects.Length > 0)
        {
            // 0番目のオブジェクトを取得
            GameObject firstSliderObject = sliderObjects[0];

            // Slider コンポーネントを取得
            slider = firstSliderObject.GetComponent<Slider>();
        }

        if (slider != null)
        {
            slider.value = stamina;
        }

        //if (BGMSoundData.BGM.Title != null)
        //{
        //    AudioManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        //}

        //イベントが発生している時または看板を読んでいる時は処理をしない
        if (Event != null)
        {
            if (Event.IsEvent() || ExplainDisplayFlg) return;
        }

        //シーン遷移する判定に当たった時のフラグがtrueの時は処理をしない
        if (ChangeSceneFlg) return;
        //セーブデータをロード
        PlayerLoadData();

        //シーン遷移する判定に当たった時のフラグfalseの時かつメニューを開いていない時
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Over")
        {
            //デバッグ用
            if (MenuManager.Instance != null)
            {
                if (!ChangeSceneFlg)
                {
                    if (!MenuManager.Instance.GetOpenFlg())
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
            }

            else
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
                //text.text = "アイテムを拾いました";
                itemList.Add(NearItem);
                itemDisplay.PickUpItem(itemHolder.itemData);

                NearItem.gameObject.SetActive(false);
            }
            else
            {
                //text.text = "アイテムがありません";
            }
        }

        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Over")
        {
            //シーン遷移する判定に当たった時のフラグをfalse
            ChangeSceneFlg = false;
        }
    }

    // 一定時間毎に呼ばれる関数
    void FixedUpdate()
    {
        if (!UpdateFlg) return;
        //ミニゲームの時の初期位置の設定
        MiniGameSetPos();
        //else
        //{
        //    isMoving = false;
        //}
        //イベントが発生している時または看板を読んでいる時は処理をしない
        if (Event != null)
        {
            if (Event.IsEvent() || ExplainDisplayFlg) return;
        }
        //シーン遷移する判定に当たった時のフラグがtrueの時は処理をしない
        if (ChangeSceneFlg) return;
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;

        if (actionEvent != null)
        {
            if (actionEvent.finishtalk && !actionEvent.inconversation || !actionEvent.finishtalk && !actionEvent.inconversation)
            {
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
                    if (actionEvent.finishtalk && !actionEvent.inconversation || !actionEvent.finishtalk && !actionEvent.inconversation) Move(moveSpeed, Time.fixedDeltaTime);

                    if (stamina < maxStamina)
                    {
                        stamina += staminaSpeed * Time.fixedDeltaTime;
                    }
                }
                animator.enabled = true;
            }

            else if (!actionEvent.finishtalk && actionEvent.inconversation)
            {

                AnimMove(0);
                DontMove(0, 0f);
                isMoving = false;
                animator.enabled = false; // 停止 // 一時停止
            }
        }

        else
        {
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
            //text.gameObject.SetActive(true);
            //text.text = "Eボタンでアイテムを拾う";

            NearItem = collision.gameObject;

        }

        if (collision.gameObject.tag == "Apple")
        {
            Debug.Log("アップルがある");
            //text.gameObject.SetActive(true);
            //text.text = "Eボタンでアイテムを拾う";
            //NearItem = collision.gameObject;

        }

        if (collision.gameObject.tag == "SkillCheck")
        {
            SkilFlg = true;
            ClearExitFlg = true;
        }

        if (collision.gameObject.tag == "ClearExit" && ClearExitFlg)
        {
            // バッドエンド（0〜50未満）
            if (Score < 50)
            {
                Debug.Log("BadEnd");
                fadeOutSceneLoader.NewGameCallCoroutine("BadEnd");
            }

            // ノーマルエンド（50〜75未満）
            else if (Score < 75)
            {
                Debug.Log("NormalEnd");
                fadeOutSceneLoader.NewGameCallCoroutine("NormalEnd");
            }

            // グッドエンド（75〜100以下）
            else if (Score <= 100)
            {
                Debug.Log("GoodEnd");
                fadeOutSceneLoader.NewGameCallCoroutine("GoodEnd");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            //text.gameObject.SetActive(false);
            //Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;

        }

        if (collision.gameObject.tag == "Apple")
        {

            //Debug.Log("鍵がありません");
            //isNearItem = false;
            NearItem = null;

        }

        if (collision.gameObject.tag == "SkillCheck")
        {
            SkilFlg = false;
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
        animator.speed = AnimSpeed;

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
            animator.SetFloat("InputX", horizontal);
            animator.SetFloat("InputY", vertical);
            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("IsMoving", isMoving);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetBool("IsMoving", isMoving);
            }
        }
    }

    //移動処理
    public void Move(float moveSpeed, float deltaTime)
    {
        rb.velocity = moveDir * moveSpeed * deltaTime;
    }
    public void DontMove(float moveSpeed, float deltaTime)
    {
        rb.velocity = moveDir * moveSpeed * deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("108RoomColl") || other.gameObject.CompareTag("Door"))
        {
            GimicHitFlg = true;
            Debug.Log(GimicHitFlg);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("108RoomColl") || other.gameObject.CompareTag("Door"))
        {
            GimicHitFlg = false;
            Debug.Log(GimicHitFlg);
        }
    }

    //NewGameが押された時の初期位置に設定
    private void NewGameSpownPlayer()
    {
        if(NewGameSpownFlg && SceneManager.GetActiveScene().name == "Floor(B1)")
        {
            this.gameObject.transform.position = new Vector3(172.09f, -27.69f,0.0f);
            NewGameSpownFlg = false;
        }
    }

    //ミニゲームの時の初期位置の設定
    private void MiniGameSetPos()
    {
        if (SceneManager.GetActiveScene().name == "SkillCheck")
        {
            if (!MiniGameFlg) // まだミニゲームの位置に変更していない場合
            {
                MiniGameFlg = true;
                this.gameObject.transform.position = new Vector3(172.09f, 50.69f, 0.0f);
            }
        }
        else
        {
            if (MiniGameFlg) // ミニゲームから戻った場合
            {
                this.gameObject.transform.position = MiniGamePos;
                MiniGameFlg = false; // 一度だけ位置を戻す
            }
            else
            {
                MiniGamePos = this.gameObject.transform.position; // 現在の位置を保存
            }
        }
    }

    //セーブデータの読み込み
    private void PlayerLoadData()
    {
        if (LoadManager.Instance != null)
        {
            if (LoadManager.Instance.NextSceneName != "Title" && LoadManager.Instance.NextSceneName != "Over")
            {
                //NewGameボタンが押された時のフラグ
                if (LoadManager.Instance.NewGamePushFlg)
                {
                    LoadManager.Instance.NewGamePushFlg = false;
                }

                //LoadGameボタンが押された時のフラグ
                else if(LoadManager.Instance.LoadGameFlg && UpdateSaveDataFlg)
                {
                    // セーブデータを読み込み、プレイヤーの位置を設定
                    LoadManager.Instance.TitleToGameLoadData();
                    LoadManager.Instance.LoadGameFlg = false;
                    UpdateSaveDataFlg = false;
                }
            }
        }
    }
}
