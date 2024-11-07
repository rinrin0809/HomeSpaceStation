using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOverLoadIcon : MonoBehaviour
{
    //横移動時のフラグ
    [SerializeField] private bool SideFlg;

    //UIの位置
    RectTransform RectTransformIns;
    //アイコン（矢印）の移動
    IconMove IconMoveIns;
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
        // AllSceneManager の取得
        if (SceneManagerIns == null)
        {
            SceneManagerIns = Object.FindAnyObjectByType<TitleSceneManager>(); // 自動取得
        }
    }

    // Update is called once per frame
    void Update()
    {
        //現在のメニューの種類取得
        SceneType Type = SceneManagerIns.GetSceneActiveMenu();

        //ロードメニューの時
        if (Type == SceneType.LoadMenu)
        {
            //移動処理
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            //押された時の処理
            OnClick();
            //Debug.Log("LoadIconMove");
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
                }

                LoadManager.Instance.SetSideNum(SideNum);
                LoadManager.Instance.SetSideFlg(SideFlg);
            }

            else
            {
                //Debug.Log("Load Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[LengthNum] != null)
                {
                    Buttons[LengthNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                }

                LoadManager.Instance.SetLengthNum(LengthNum);
                LoadManager.Instance.SetSideFlg(SideFlg);
            }
        }
    }
}
