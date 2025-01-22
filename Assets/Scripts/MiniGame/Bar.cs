using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    //�ړ����x
    [SerializeField] private float MIN_MOVE_SPEED = 7.0f;
    [SerializeField] private float MAX_MOVE_SPEED = 15.0f;
    [SerializeField] private float MoveSpeed = 0.0f;
    //�������]�p
    private int direction = 1;
    //����p�t���O
    private bool isCheck;
    //���͂��ꂽ���̃t���O
    private bool PushFlg = false;
    //�������X�R�A�iGood�j
    private int GoodAddScore = 3;
    //�������X�R�A�iGreat�j
    private int GreatAddScore = 5;
    // ��莞�ԁi�b�j
    public float DelayTime = 500.0f;
    // ���Z������������܂ł̎���
    public float Duration = 100.0f;
    //���
    public float LimitPosY = 5.0f;
    //�]�[���Ɏ~�܂������̃t���O
    private bool GoodHit = false;
    private bool GreatHit = false;
    //�����_���ő��x�ω�����t���O
    private bool RandomSpeedFlg = true;
    //�X�L���`�F�b�N���\���ɂ���t���O
    [SerializeField] private bool OffFlg = false;

    private SkilCheck skilCheck;

    [SerializeField] GameObject SkilCheckObj;

    [SerializeField] GameObject YesOffObj;

    private Vector3 Pos;

    [SerializeField] float PushIntervalTime = 1.0f;

    //Good�G�t�F�N�g�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject GoodEffect;
    //Greate�G�t�F�N�g�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject GreatEffect;
    //Bad�G�t�F�N�g�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject BadObject;

    private void Start()
    {
        Pos = new Vector3(transform.parent.gameObject.transform.position.x, 0, transform.parent.gameObject.transform.position.z);

        parentObject = this.transform.parent.gameObject;
        skilCheck = SkilCheckObj.GetComponent<SkilCheck>();

        RandMoveSpeed();
    }

    private void Update()
    {
        //
        if (YesOffObj != null && YesOffObj.activeSelf) return;
        //�X�L���`�F�b�N�̃X�R�A����
        SkilCheckScore();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PushFlg = false;
            RandMoveSpeed();
        }

        if(OffFlg)
        {
            if(PushIntervalTime < 0.0f)
            {
                PushFlg = false;
                parentObject.SetActive(false);
                RandMoveSpeed();
                this.transform.position = Pos;
                OffFlg = false;
                PushIntervalTime = 2.0f;
            }
        }

        if(PushFlg)
        {
            PushIntervalTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!PushFlg)
        {
            if (transform.position.y >= LimitPosY) direction = -1;

            if (transform.position.y <= -LimitPosY) direction = 1;

            transform.position = new Vector3(transform.position.x,
                transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, 0);
        }
    }

    //�ړ����x�������_���ŕύX
    private void RandMoveSpeed()
    {
        MoveSpeed = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);
        MoveSpeed = Mathf.Clamp(MoveSpeed, 0.1f, MAX_MOVE_SPEED);
        Debug.Log("MoveSpeed" + MoveSpeed);
    }

    //�o�[���ǂ̃]�[���Ŕ��܂������̔���
    private void ZoneBar()
    {
        if (transform.position.y >= -0.1f && transform.position.y <= 0.1f)
        {
            GreatHit = true;
            GoodHit = false;
            GreatEffect.SetActive(true);
        }

        else if (transform.position.y >= -0.73f && transform.position.y <= 0.75f)
        {
            GoodHit = true;
            GreatHit = false;
            GoodEffect.SetActive(true);
        }

        else
        {
            GoodHit = false;
            GreatHit = false;
            BadObject.SetActive(true);
        }

        if (GoodHit || GreatHit)
        {
            isCheck = true;
        }
        else
        {
            isCheck = false;
        }
    }

    //�X�L���`�F�b�N�̃X�R�A����
    private void SkilCheckScore()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�o�[���ǂ̃]�[���Ŕ��܂������̔���
            ZoneBar();

            if (!PushFlg)
            {
                if (isCheck)
                {
                    Debug.Log("Hit");

                    if (GoodHit)
                    {
                        skilCheck.Score += GoodAddScore;
                        Debug.Log("GoodHit");
                    }

                    else if (GreatHit)
                    {
                        skilCheck.Score += GreatAddScore;
                        Debug.Log("GreatHit");
                    }
                    OffFlg = true;
                }
                else
                {
                    Debug.Log("Miss");
                    skilCheck.Score -= 5;

                    if (skilCheck.Score < 0) skilCheck.Score = 0;
                    
                    OffFlg = true;
                }
                PushFlg = true;
            }
        }
    }
}
