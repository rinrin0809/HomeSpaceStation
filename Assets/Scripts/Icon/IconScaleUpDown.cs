using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScaleUpDown : MonoBehaviour
{
    //アイコンのスケール
    private Vector2 ScaleXY;

    public Vector2 GetScaleXY()
    {
        return ScaleXY;
    }

    //アイコンスケールの最小値
    private Vector2 MIN_SCALE_XY = new Vector2(0.9f, 0.9f);
    //アイコンスケールの最大値
    private Vector2 MAX_SCALE_XY = new Vector2(1.25f, 1.25f);
    //普通の大きさ
    private Vector2 DEFAULT_SCALE_XY = new Vector2(1.0f, 1.0f);
    //符号反転用のフラグ
    private bool UpDownFlg = false;
    //変化速度
    private float ScaleSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ScaleXY = new Vector2(1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //フラグを反転させる
        InversionFlg();
    }

    //スケールを変化させる
    public void ScaleRaise(Button[] Buttons, int Num)
    {
        if (Num < 0 || Num >= Buttons.Length) return; // 範囲外チェック

        if (!UpDownFlg)
        {
            ScaleXY.x += ScaleSpeed * Time.deltaTime;
            ScaleXY.y += ScaleSpeed * Time.deltaTime;
        }
        else
        {
            ScaleXY.x -= ScaleSpeed * Time.deltaTime;
            ScaleXY.y -= ScaleSpeed * Time.deltaTime;
        }

        for (int i = 0; i < Buttons.Length; i++)
        {
            RectTransform rectTransform = Buttons[i].gameObject.GetComponent<RectTransform>();
            rectTransform.localScale = (i == Num) ? ScaleXY : DEFAULT_SCALE_XY;
        }
    }

    //フラグを反転させる
    public void InversionFlg()
    {
        if(ScaleXY.x >= MAX_SCALE_XY.x || ScaleXY.y >= MAX_SCALE_XY.y)
        {
            UpDownFlg = true;
        }

        else if(ScaleXY.x <= MIN_SCALE_XY.x || ScaleXY.y <= MIN_SCALE_XY.y)
        {
            UpDownFlg = false;
        }
    }
}
