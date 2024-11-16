using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlackShadowMove : MonoBehaviour
{
    //イベントのデータ
    public EventData Event;
    //黒い影の位置
    private Transform GetTransform;
    //移動位置
    private Vector3 MovePos;
    // 黒い影の移動速度
    public float speed = 7f; 

    // Start is called before the first frame update
    void Start()
    {
        GetTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動処理
        Move();
    }

    //移動処理
    private void Move()
    {
        if(Event.GetNameEventActionFlg("黒い影が逃げる"))
        {
            //xだけ固定
            MovePos.x = GetTransform.position.x;
            //Y座標だけ移動
            MovePos.y += speed * Time.deltaTime;
            //Transformに代入
            GetTransform.position = MovePos;
        }
    }
}
