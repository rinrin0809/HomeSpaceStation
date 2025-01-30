using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOff : MonoBehaviour
{
    //消すオブジェクト
    [SerializeField] private GameObject GameObj;
    //消すオブジェクトの色
    [SerializeField] private Color ObjColor;
    //消すオブジェクトのイメージ
    Image ObjImage;
    //透明度
    private float Alpha = 1.0f;
    //徐々に消す時間
    private float AlphaTime = 2.0f;
    void Start()
    {
        //初期化
        ObjColor = new Color(0.0f,0.0f,0.0f,1.0f);
        //消すオブジェクトのイメージ取得
        ObjImage = GameObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //透明度を更新
        ObjImage.color = ObjColor;

        //プレイヤーが動いている時またはメニューを開いている時
        if (Player.Instance.GetisMoving())
        {
            if (ObjColor.a < 0.0f)
            {
                ObjColor.a = 0.0f;
            }

            else
            {
                ObjColor.a -= AlphaTime * Time.deltaTime;
            }
        }

        else
        {
            if(ObjColor.a > 1.0f)
            {
                ObjColor.a = 1.0f;
            }

            else
            {
                ObjColor.a += AlphaTime * Time.deltaTime;
            }
        }

        if (MenuManager.Instance.GetOpenMenuFlg())
        {
            GameObj.SetActive(false);
        }

        else
        {
            GameObj.SetActive(true);
        }
    }
}
