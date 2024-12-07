using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSelectIcon : MonoBehaviour
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
    [SerializeField] private Button[] Buttons;

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

        //ロードメニューの時
        if (Type == MenuType.LoadMenu && !Input.GetKey(KeyCode.Backspace) && !Input.GetKeyDown(KeyCode.Backspace))
        {
            //移動処理
            RectTransformIns.position = IconMoveIns.Move(SideFlg, RectTransformIns.position);
            int LengthNum = IconMoveIns.GetLengthNum();
            if(SaveManager.Instance != null) SaveManager.Instance.SetLengthNum(LengthNum);
            if(LoadManager.Instance != null) LoadManager.Instance.SetLengthNum(LengthNum);
            //押された時の処理
            OnClick();

            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                IconMoveIns.ResetNum();
            }
        }
    }

    //押された時の処理
    void OnClick()
    {
        // スペースキーが押されたか確認 (KeyCode.Return はエンターキー)
        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.Backspace) && !Input.GetKeyDown(KeyCode.Backspace))
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

                if (LoadManager.Instance != null)
                {
                    LoadManager.Instance.SetSideNum(SideNum);
                    LoadManager.Instance.SetSideFlg(SideFlg);
                }
            }

            else
            {
                Debug.Log("Enter key pressed Load");

                int LengthNum = IconMoveIns.GetLengthNum();
                // ボタンが設定されている場合、ボタンのonClickイベントを呼び出す
                if (Buttons[LengthNum] != null)
                {
                    IconMoveIns.ResetNum();
                    Buttons[LengthNum].onClick.Invoke(); // ボタンのクリックイベントを呼び出す
                }
                if (SaveManager.Instance != null) SaveManager.Instance.SetLengthNum(LengthNum);

                if (LoadManager.Instance != null)
                {
                    LoadManager.Instance.SetLengthNum(LengthNum);
                    LoadManager.Instance.SetSideFlg(SideFlg);
                }
            }
        }
    }
}
