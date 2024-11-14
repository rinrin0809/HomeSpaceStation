using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleIconMove : MonoBehaviour
{
    //横移動時のフラグ
    [SerializeField] private bool SideFlg;
    //UIの位置
    RectTransform RectTransformIns;
    //アイコン（矢印）の移動
    IconMove IconMoveIns;
    //アイコンの大きさ
    IconScaleUpDown IconScaleIns;
    //タイトルシーンの管理クラス
    private TitleSceneManager SceneManagerIns;
    //ボタンの配列
    [SerializeField] private Button[] Buttons;

    void Start()
    {
        //UIの位置取得
        RectTransformIns = gameObject.GetComponent<RectTransform>();
        //アイコンの移動取得
        IconMoveIns = gameObject.GetComponent<IconMove>();
        //アイコンの大きさ取得
        IconScaleIns = gameObject.GetComponent<IconScaleUpDown>();
        // SceneManager の取得
        if (SceneManagerIns == null)
        {
            SceneManagerIns = Object.FindAnyObjectByType<TitleSceneManager>(); // 自動取得
        }
    }

    // Update is called once per frame
    void Update()
    {
        //現在のシーンの種類取得
        SceneType Type = SceneManagerIns.GetSceneActiveMenu();
        //タイトル
        if(Type == SceneType.TitleMenu)
        {
            //移動処理
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            //押された時の処理
            OnClick();
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                IconScaleIns.ScaleRaise(Buttons, SideNum);
            }

            else
            {
                //Debug.Log("Title Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                IconScaleIns.ScaleRaise(Buttons, LengthNum);
            }
            //Debug.Log("TitleIconMove");
        }
    }

    //押された時の処理
    void OnClick()
    {
        // エンターキーが押されたか確認 (KeyCode.Return はエンターキー)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 横移動の場合
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[SideNum] != null)
                {
                    Buttons[SideNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                    IconScaleIns.ScaleRaise(Buttons, SideNum);
                }
            }

            else
            {
                //Debug.Log("Title Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[LengthNum] != null)
                {
                    Buttons[LengthNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                }
            }
        }
    }
}
