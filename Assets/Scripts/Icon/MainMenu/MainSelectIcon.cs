using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSelectIcon : MonoBehaviour
{
    //横移動時のフラグ
    [SerializeField] private bool SideFlg;

    //UIの位置
    RectTransform RectTransformIns;
    //アイコン（矢印）の移動
    IconMove IconMoveIns;
    //メニューの管理クラス
    private MenuManager MenuManagerIns;

    //ボタンの配列
    public Button[] Buttons;

    void Start()
    {
        //UIの位置取得
        RectTransformIns = gameObject.GetComponent<RectTransform>();
        //アイコンの移動取得
        IconMoveIns = gameObject.GetComponent<IconMove>();
        // MenuManager の取得
        if (MenuManagerIns == null)
        {
            MenuManagerIns = Object.FindAnyObjectByType<MenuManager>(); // 自動取得
        }
    }

    // Update is called once per frame
    void Update()
    {
        //現在のメニューの種類取得
        MenuType Type = MenuManagerIns.GetActiveMenu();
        
        //メインメニューの時
        if (Type == MenuType.MainMenu && !Input.GetKey(KeyCode.Backspace) && !Input.GetKeyDown(KeyCode.Backspace))
        {
            //移動処理
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            //押された時の処理
            OnClick();

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                IconMoveIns.ResetNum();
            }
        }
    }

    //押された時の処理
    void OnClick()
    {
        // スペースキーが押されたか確認 (KeyCode.Return はエンターキー)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Enter key pressed");
            // 横移動の場合
            if (SideFlg)
            {
                int SideNum = IconMoveIns.GetSideNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[SideNum] != null)
                {
                    IconMoveIns.ResetNum();
                    Buttons[SideNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                }
            }

            else
            {
                int LengthNum = IconMoveIns.GetLengthNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[LengthNum] != null)
                {
                    IconMoveIns.ResetNum();
                    Buttons[LengthNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                }
            }
        }

        else if(MenuManagerIns.GetOpenFlg() && Input.GetKeyDown(KeyCode.M))
        {
            IconMoveIns.ResetNum();
        }
    }
}
