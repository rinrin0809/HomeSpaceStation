using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//メニューの種類
public enum MenuType
{
    MainMenu,
    ItemMenu,
    SaveMenu,
    LoadMenu,
    None
}

public class MenuManager : MonoBehaviour
{
    //メニューを開いているかのフラグ
    bool OpenMenuFlg = false;

    //各種パネルの宣言
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject ItemMenuPanel;
    [SerializeField] GameObject SaveMenuPanel;
    [SerializeField] GameObject LoadMenuPanel;

    // 現在どのメニューがアクティブかを保持する
    private MenuType ActiveMenu = MenuType.None;

    //現在どのメニューがアクティブかを保持する
    public MenuType GetActiveMenu()
    {
        return ActiveMenu;
    }

    //更新処理
    void Update()
    {
        //メニュー表示
        OpenMenu();
        //Debug.Log("Player position saved: " + ActiveMenu);
    }

    //メニューが表示されているかのフラグの状態取得
    public bool GetOpenFlg()
    {
        //Mキー（仮）が押されてOpenMenuFlgがfalseの時はtrue
        if (Input.GetKeyDown(KeyCode.M) && !OpenMenuFlg)
        {
            OpenMenuFlg = true;
        }

        //Mキー（仮）が押されてOpenMenuFlgがtrueの時はfalse
        else if (Input.GetKeyDown(KeyCode.M) && OpenMenuFlg)
        {
            OpenMenuFlg = false;
        }

        return OpenMenuFlg;
    }

    //メニュー表示
    public void OpenMenu()
    {
        //OpenMenuFlgがtrueの時は最初のメニュー表示
        if (GetOpenFlg())
        {
            if (!SaveMenuPanel.activeSelf && !LoadMenuPanel.activeSelf && !ItemMenuPanel.activeSelf)
            {
                MainMenuPanel.SetActive(true);
            }
            else
            {
                MainMenuPanel.SetActive(false);
            }
            Time.timeScale = 0;
        }

        //OpenMenuFlgがfalseの時はメニューを閉じる
        else
        {
            CloseMenu();
        }
    }

    //MenuPanelでItemButtonが押されたときの処理
    public void ItemMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        ItemMenuPanel.SetActive(true);
        ActiveMenu = MenuType.ItemMenu;
    }

    //MenuPanelでSaveButtonが押されたときの処理
    public void SaveMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(true);
        ActiveMenu = MenuType.SaveMenu;
    }

    //MenuPanelでLoadButtonが押されたときの処理
    public void LoadMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(true);
        ActiveMenu = MenuType.LoadMenu;
    }

    //最初のメニュー表示
    public void BackToMenu()
    {
        MainMenuPanel.SetActive(true);
        ItemMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(false);
        ActiveMenu = MenuType.MainMenu;
    }

    //メニューを閉じる
    public void CloseMenu()
    {
        MainMenuPanel.SetActive(false);
        ItemMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(false);
        OpenMenuFlg = false;
        ActiveMenu = MenuType.MainMenu;
        Time.timeScale = 1;
    }
}
