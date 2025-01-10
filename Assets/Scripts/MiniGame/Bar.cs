using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    //移動速度
    private float MIN_MOVE_SPEED = 1.0f;
    private float MAX_MOVE_SPEED = 1.0f;
    [SerializeField] private float MoveSpeed = 0.0f;
    //符号反転用
    private int direction = 1;
    //判定用フラグ
    private bool isCheck;
    //入力された時のフラグ
    private bool PushFlg = false;
    //足されるスコア（Good）
    private int GoodAddScore = 3;
    //足されるスコア（Great）
    private int GreatAddScore = 5;
    // 一定時間（秒）
    public float DelayTime = 500.0f;
    // 加算を完了させるまでの時間
    public float Duration = 100.0f;
    //上限
    public float LimitPosY = 5.0f;
    //ゾーンに止まった時のフラグ
    private bool GoodHit = false;
    private bool GreatHit = false;
    //ランダムで速度変化するフラグ
    private bool RandomSpeedFlg = true;
    //スキルチェックを非表示にするフラグ
    [SerializeField] private bool OffFlg = false;

    private SkilCheck skilCheck;
    [SerializeField] GameObject SkilCheckObj;
    [SerializeField] GameObject YesOffObj;

    private void Start()
    {
        parentObject = this.transform.parent.gameObject;
        skilCheck = SkilCheckObj.GetComponent<SkilCheck>();

        RandMoveSpeed();
    }

    private void Update()
    {
        //
        if (YesOffObj != null && YesOffObj.activeSelf) return;
        //スキルチェックのスコア処理
        SkilCheckScore();


        if (Input.GetKeyDown(KeyCode.Return))
        {
            PushFlg = false;
            RandMoveSpeed();
        }

        if(OffFlg)
        {
            OffFlg = false;
            PushFlg = false;
            RandMoveSpeed();
            parentObject.SetActive(false);
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

    //移動速度をランダムで変更
    private void RandMoveSpeed()
    {
        MoveSpeed = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);
        MoveSpeed = Mathf.Clamp(MoveSpeed, 0.1f, MAX_MOVE_SPEED);
        Debug.Log("MoveSpeed" + MoveSpeed);
    }

    //バーがどのゾーンで泊まったかの判定
    private void ZoneBar()
    {
        if (transform.position.y >= -0.1f && transform.position.y <= 0.1f)
        {
            GreatHit = true;
            GoodHit = false;
        }

        else if (transform.position.y >= -0.73f && transform.position.y <= 0.75f)
        {
            GoodHit = true;
            GreatHit = false;
        }

        else
        {
            GoodHit = false;
            GreatHit = false;
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

    //スキルチェックのスコア処理
    private void SkilCheckScore()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //バーがどのゾーンで泊まったかの判定
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
