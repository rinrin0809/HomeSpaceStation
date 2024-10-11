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

    // ���C���x���g��
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    private List<Item> itemData = new List<Item>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            text.text = "E�{�^���ŃA�C�e�����E��";
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("�����|�P�b�g�ɓ���܂�");
                itemList.Add(collision.gameObject);
                //Destroy(collision.gameObject);
            }
           
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("��������܂���");
        }
    }

  

}
