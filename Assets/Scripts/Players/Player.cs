using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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
    private bool isMoving;

    // �e�L�X�g
    public TextMeshProUGUI text;

    // �A�C�e���̋߂����ǂ����̃t���O
    public bool isNearItem = false;
    // �߂��̃A�C�e��������
    
    private GameObject NearItem = null;

    private Item itemData;

    // ���C���x���g��
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

        //�Q�[���V�[���̎�
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if(LoadManager.Instance != null)
            {
                //NewGame�{�^���������ꂽ���̃t���O
                if (LoadManager.Instance.NewGamePushFlg)
                {
                    //�����ʒu�̐ݒ�
                    Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    Transform objectTransform = gameObject.GetComponent<Transform>();
                    objectTransform.position = targetPosition;
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
        // H�L�[�������ꂽ���m�F (KeyCode.H ��H�L�[)
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
                text.text = "�A�C�e�����E���܂���";
                itemList.Add(NearItem);
                itemDisplay.PickUpItem(itemHolder.itemData);

                NearItem.gameObject.SetActive(false);
            }
            else
            {
                text.text = "�A�C�e��������܂���";
            }
            //Debug.Log(NearItem.name + "���󂯎��܂�");


            //inventory.AddItem()
            //Destroy(NearItem);

        }
       
    }
    // ��莞�Ԗ��ɌĂ΂��֐�
    void FixedUpdate()
    {
        //rigidbody2d.velocity = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;

        // H�L�[�������ꂽ���m�F (KeyCode.H ��H�L�[)
        if (Input.GetKey(KeyCode.H) && stamina > minStamina && !zeroStaminaFlg)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(true);
            text.text = "E�{�^���ŃA�C�e�����E��";
            
            NearItem = collision.gameObject;
         
        }

        if (collision.gameObject.tag == "Apple")
        {
            //Debug.Log("�A�b�v��������");
            text.gameObject.SetActive(true);
            text.text = "E�{�^���ŃA�C�e�����E��";
            NearItem = collision.gameObject;
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.gameObject.SetActive(false);
            Debug.Log("��������܂���");
            //isNearItem = false;
            NearItem = null;
            
        }

        if (collision.gameObject.tag == "Apple")
        {
            text.gameObject.SetActive(false);
            Debug.Log("��������܂���");
            //isNearItem = false;
            NearItem = null;

        }
    }

    // �A�C�e���󂯎��
    public void AddItemInventory(Item item)
    {
        if (inventory != null)
        {
            inventory.AddItem(item);
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
}
