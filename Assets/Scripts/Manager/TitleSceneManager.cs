using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シーンでの画面の種類
public enum SceneType
{
    TitleMenu,
    LoadMenu,
    OverMenu,
    None
}

public class TitleSceneManager : MonoBehaviour
{
    //ロードメニューを開いているかのフラグ
    bool OpenLoadMenuFlg = false;

    [SerializeField] GameObject ScenePanel;
    [SerializeField] GameObject LoadMenuPanel;
    public EventData eventflag;

    // 現在どのシーンが表示されているアクティブかを保持する
    private SceneType ActiveMenu = SceneType.None;

    //現在どのメニューがアクティブかを保持する
    public SceneType GetSceneActiveMenu()
    {
        return ActiveMenu;
    }

    void Start()
    {
        ScenePanel.SetActive(true);
        ActiveMenu = SceneType.TitleMenu;
    }

    //更新処理
    void Update()
    {
        //メニュー表示
        OpenLoadMenu();
    }

    //メニューが表示されているかのフラグの状態取得
    public bool GetOpenLoadMenuFlg()
    {
        //エンターキーが押されてOpenMenuFlgがfalseの時はtrue
        if (Input.GetKeyDown(KeyCode.Return) && !OpenLoadMenuFlg)
        {
            OpenLoadMenuFlg = true;
        }

        return OpenLoadMenuFlg;
    }

    //メニュー表示
    public void OpenLoadMenu()
    {
        //OpenMenuFlgがtrueの時は最初のメニュー表示
        if (GetOpenLoadMenuFlg())
        {
            if (!LoadMenuPanel.activeSelf)
            {
                ScenePanel.SetActive(true);
            }
            else
            {
                ScenePanel.SetActive(false);
            }
        }

        //Mキーが押されたら
        if (Input.GetKeyDown(KeyCode.M) && OpenLoadMenuFlg)
        {
            CloseMenu();
        }
    }

    //ロードメニュー表示
    public void LoadMenu()
    {
        ScenePanel.SetActive(false);
        LoadMenuPanel.SetActive(true);
        ActiveMenu = SceneType.LoadMenu;
        //Debug.Log("Open LoadMenu");
    }

    //メニューを閉じる
    public void CloseMenu()
    {
        ScenePanel.SetActive(true);
        LoadMenuPanel.SetActive(false);
        ActiveMenu = SceneType.TitleMenu;
        //Debug.Log("Close LoadMenu");
    }
}
