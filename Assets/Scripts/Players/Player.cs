using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        text.gameObject.SetActive(false);
       
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

}
