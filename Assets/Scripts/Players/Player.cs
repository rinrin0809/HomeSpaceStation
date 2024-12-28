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

    //�C�x���g
    public EventData Event;

    // �ړ�
    public float moveSpeed; // �ړ����x
    private float dashMoveSpeed = 450.0f;
    private float horizontal; // x
    private float vertical; // y

    //�X�^�~�i
    [SerializeField]
    private float stamina;
    private float maxStamina = 100.0f;
    private float minStamina = 0.0f;
    //�X�^�~�i�����炷���x
    private float staminaSpeed = 50.0f;

    //�X�^�~�i��0�ɂȂ������̃t���O
    private bool zeroStaminaFlg = false;

    //�X�^�~�i�Q�[�W
    public Slider slider;

    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 moveDir;

    // �A�j���[�V����
    [SerializeField]
    private Animator animatior;
    [SerializeField]
    private float animSpeed = 1.0f;
    private float dashAnimSpeed = 2.0f;
    [SerializeField]
    private bool isMoving;

    // �e�L�X�g
    //public TextMeshProUGUI text;

    // �A�C�e���̋߂����ǂ����̃t���O
    public bool isNearItem = false;
    // �߂��̃A�C�e��������

    private GameObject NearItem = null;

    private Item itemData;

    //bool isstaripos = false;

    // ���C���x���g��
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();

    //�V�[���ړ�
    public VectorValue startingPosition;

    private bool startflg = true;


    //�V�[���J��
    //public Transform targetPosition; // Inspector�Őݒ肷��ꍇ
    //[SerializeField]
    //private string NextSceneObject = "";
    //[SerializeField]
    //private string NextScene = "";

    //�X�L���`�F�b�N�̃X�R�A
    [SerializeField]
    public int Score = 0;

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

    //���������Ă��鎞�i���j
    public bool HasKeyFlg = false;
    //�M�~�b�N�ɓ����������i���j
    public bool GimicHitFlg = false;

    //�t�F�[�h�A�E�g
    private FadeOutSceneLoader fadeOutSceneLoader;
    //�V�[���J�ڂ��锻��ɓ����������̃t���O
    public bool ChangeSceneFlg = false;

    // �v���C���[�̈ʒu�ƌ�����ۑ����邽�߂̕ϐ�
    [SerializeField]
    private VectorValue playerStorage;

    //�ŔƂ��ɓ����������ɓ������~�߂�t���O�i����f�[�^���\��j
    public bool ExplainDisplayFlg = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��̃I�u�W�F�N�g��ێ�����
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //�f�o�b�O�p
        Event.Initialize();

        rb = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();

        Vector3 targetPosition = new Vector3(0.0f, -4.0f, 0.0f);

        //if(isstaripos)
        //{
        //    new Vector3(0, 0, 0);
        //    isstaripos = true;
        //}

        // �v���C���[�̌�����ݒ�
        if (playerStorage != null)
        {
            transform.rotation = playerStorage.playerRotation;  // �ۑ����ꂽ������K�p
        }

        //if (playerStorage != null)
        //{
        //    Debug.Log("Player�̏����ʒu: " + playerStorage.initialValue); // �����Ńf�o�b�O
        //    //startingPosition.initialValue = targetPosition;
        //    transform.position = playerStorage.initialValue;
        //}

        if (playerStorage != null)
        {
            Debug.Log("Player�̏����ʒu: " + playerStorage.initialValue);
            transform.position = playerStorage.initialValue; // �ۑ����ꂽ�����ʒu�Ɉړ�
        }

        //if (text != null)
        //{
        //    text.gameObject.SetActive(false);
        //}

        stamina = 100.0f;

        if (slider != null)
        {
            slider.value = stamina;
        }

        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
        Transform objectTransform = gameObject.GetComponent<Transform>();

        if (LoadManager.Instance != null)
        {
            if (LoadManager.Instance.NextSceneName != "Title" && LoadManager.Instance.NextSceneName != "Over")
            {
                Debug.Log("LoadManager.Instance.NewGamePushFlg" + LoadManager.Instance.NewGamePushFlg);

                //NewGame�{�^���������ꂽ���̃t���O
                if (LoadManager.Instance.NewGamePushFlg)
                {

                    //�����ʒu�̐ݒ�
                    objectTransform.position = targetPosition;
                    startflg = false;

                    transform.position = startingPosition.initialValue; // �v���C���[�̈ʒu��ۑ�

                    Debug.Log(objectTransform.position);
                }

                //LoadGame�{�^���������ꂽ���̃t���O
                else
                {
                    // �Z�[�u�f�[�^��ǂݍ��݁A�v���C���[�̈ʒu��ݒ�
                    LoadManager.Instance.TitleToGameLoadData();
                    Debug.Log("Load");
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        //if (BGMSoundData.BGM.Title != null)
        //{
        //    AudioManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        //}

        //�C�x���g���������Ă��鎞�܂��͊Ŕ�ǂ�ł��鎞�͏��������Ȃ�
        if (Event != null)
        {
            if (Event.IsEvent() || ExplainDisplayFlg) return;
        }

        //�V�[���J�ڂ��锻��ɓ����������̃t���Ofalse�̎������j���[���J���Ă��Ȃ���
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game1")
        {
            if (!ChangeSceneFlg && !MenuManager.Instance.GetOpenFlg())
            {
                //�V�t�g�L�[�������ꂽ��(�R�����g�A�E�g���Ă�͉̂E�̃V�t�g�L�[)
                //�X�^�~�i�ŏ��l���傫�������X�^�~�i��0�ɂȂ��Ă��Ȃ���
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
                //�V�t�g�L�[�������ꂽ��(�R�����g�A�E�g���Ă�͉̂E�̃V�t�g�L�[)
                //�X�^�~�i�ŏ��l���傫�������X�^�~�i��0�ɂȂ��Ă��Ȃ���
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
                //text.text = "�A�C�e�����E���܂���";
                itemList.Add(NearItem);
                itemDisplay.PickUpItem(itemHolder.itemData);
                HasKeyFlg = true;
                NearItem.gameObject.SetActive(false);
            }
            else
            {
                //text.text = "�A�C�e��������܂���";
            }
            //Debug.Log(NearItem.name + "���󂯎��܂�");


            //inventory.AddItem()
            //Destroy(NearItem);

        }

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Over")
        {
            //�V�[���J�ڂ��锻��ɓ����������̃t���O��false
            ChangeSceneFlg = false;
        }
    }

    // ��莞�Ԗ��ɌĂ΂��֐�
    void FixedUpdate()
    {
        //�C�x���g���������Ă��鎞�܂��͊Ŕ�ǂ�ł��鎞�͏��������Ȃ�
        if (Event != null)
        {
            if (Event.IsEvent() || ExplainDisplayFlg) return;
        }
        //�V�[���J�ڂ��锻��ɓ����������̃t���O��true�̎��͏��������Ȃ�
        if (ChangeSceneFlg) return;

        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

        //�V�t�g�L�[�������ꂽ��(�R�����g�A�E�g���Ă�͉̂E�̃V�t�g�L�[)
        //�X�^�~�i�ŏ��l���傫�������X�^�~�i��0�ɂȂ��Ă��Ȃ���
        if (Input.GetKey(KeyCode.LeftShift) /*|| Input.GetKey(KeyCode.RightShift)*/ &&
            stamina > minStamina && !zeroStaminaFlg)
        {
            //�ړ�����
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
                //H�L�[��������Ă��鎞�ɃX�^�~�i�����炵�����Ȃ���΃R�����g�A�E�g
                stamina += staminaSpeed * Time.fixedDeltaTime;
            }
        }

        else
        {
            //�ړ�����
            Move(moveSpeed, Time.fixedDeltaTime);

            if (stamina < maxStamina)
            {
                stamina += staminaSpeed * Time.fixedDeltaTime;
            }
        }
    }

    //�v���C���[�̌�������
    public void SavePlayerRotation()
    {
        if (playerStorage != null)
        {
            playerStorage.playerRotation = transform.rotation;  // �v���C���[�̌�����ۑ�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            //text.gameObject.SetActive(true);
            //text.text = "E�{�^���ŃA�C�e�����E��";

            NearItem = collision.gameObject;

        }

        if (collision.gameObject.tag == "Apple")
        {
            //Debug.Log("�A�b�v��������");
            //text.gameObject.SetActive(true);
            //text.text = "E�{�^���ŃA�C�e�����E��";
            NearItem = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            //text.gameObject.SetActive(false);
            //Debug.Log("��������܂���");
            //isNearItem = false;
            NearItem = null;

        }

        if (collision.gameObject.tag == "Apple")
        {

            //Debug.Log("��������܂���");
            //isNearItem = false;
            NearItem = null;

        }
    }

    // �A�C�e���󂯎��
    public void AddItemInventory(Item item)
    {
        if (inventory != null)
        {
            inventory.AddItem(item, true);
            Debug.Log(item.Name + "���C���x���g���ɒǉ�����܂���");
        }
        else
        {
            Debug.Log("�C���x���g�����ݒ肳��ĂȂ�");
        }
    }

    //�A�j���[�V�����̍Đ�����
    private void AnimMove(float AnimSpeed)
    {
        // �A�j���[�V�����̍Đ��X�s�[�h
        animatior.speed = AnimSpeed;

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

    //�ړ�����
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

        if (other.gameObject.CompareTag("ClearExit"))
        {
            //�V�[���J�ڂ��锻��ɓ����������̃t���O��true
            ChangeSceneFlg = true;

            // �o�b�h�G���h�i0�`50�����j
            if (Score < 50)
            {
                fadeOutSceneLoader.NewGameCallCoroutine("BadEnd");
            }

            // �m�[�}���G���h�i50�`75�����j
            else if (Score < 75)
            {
                fadeOutSceneLoader.NewGameCallCoroutine("NormalEnd");
            }

            // �O�b�h�G���h�i75�`100�ȉ��j
            else if (Score <= 100)
            {
                fadeOutSceneLoader.NewGameCallCoroutine("GoodEnd");
            }

            //fadeOutSceneLoader.NewGameCallCoroutine("Title");
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

    //public void SetNextScenePosition(Vector2 newPosition)
    //{
    //    if (playerStorage != null)
    //    {
    //        playerStorage.initialValue = newPosition;
    //    }
    //}
}
