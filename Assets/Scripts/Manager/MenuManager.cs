using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
    public static MenuManager Instance;

    //メニューを開いているかのフラグ
    private bool OpenMenuFlg = false;

    //メニューを開いているフラグ取得
    public bool GetOpenMenuFlg()
    {
        return OpenMenuFlg;
    }

    //アイテムメニューを開いているかのフラグ
    private bool OpenItemMenuFlg = false;

    //アイテムメニューを開いているフラグ取得
    public bool GetOpenItemMenuFlg()
    {
        return OpenItemMenuFlg;
    }

    //各種パネルの宣言
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject SaveMenuPanel;
    [SerializeField] GameObject LoadMenuPanel;

    // 現在どのメニューがアクティブかを保持する
    private MenuType ActiveMenu = MenuType.None;

    //現在どのメニューがアクティブかを保持する
    public MenuType GetActiveMenu()
    {
        return ActiveMenu;
    }

    void Start()
    {
        Instance = this;
    }

    //更新処理
    void Update()
    {
        //メニュー表示
        OpenMenu();

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            if (ActiveMenu == MenuType.SaveMenu || ActiveMenu == MenuType.LoadMenu)
            {
                BackToMenu();
            }
        }
    }

    //メニューが表示されているかのフラグの状態取得
    public bool GetOpenFlg()
    {
        if (Input.GetKeyDown(KeyCode.M) && !OpenMenuFlg)
        {
            OpenMenuFlg = true;
        }

        else if (Input.GetKeyDown(KeyCode.Backspace) && OpenMenuFlg && ActiveMenu == MenuType.MainMenu)
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
            if (!SaveMenuPanel.activeSelf && !LoadMenuPanel.activeSelf && !OpenItemMenuFlg/*ItemMenuPanel.activeSelf*/)
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
        OpenItemMenuFlg = true;
        ActiveMenu = MenuType.ItemMenu;
    }

    //MenuPanelでSaveButtonが押されたときの処理
    public void SaveMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(true);
        SaveManager.Instance.OpenSaveMenuFlg = true;
        ActiveMenu = MenuType.SaveMenu;
    }

    //MenuPanelでLoadButtonが押されたときの処理
    public void LoadMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(true);
        LoadManager.Instance.OpenLoadMenuFlg = true;
        ActiveMenu = MenuType.LoadMenu;
    }

    //最初のメニュー表示
    public void BackToMenu()
    {
        MainMenuPanel.SetActive(true);
        OpenItemMenuFlg = false;
        SaveMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(false);
        SaveManager.Instance.OpenSaveMenuFlg = false;
        LoadManager.Instance.OpenLoadMenuFlg = false;
        ActiveMenu = MenuType.MainMenu;
    }

    //メニューを閉じる
    public void CloseMenu()
    {
        //ロードメニューの時
        if (ActiveMenu == MenuType.LoadMenu)
        {
            string FileName = "";
            switch (LoadManager.Instance.GetSideNum())
            {
                case 0:
                    FileName = LoadManager.Instance.LoadPath("/PlayerData1.json");
                    if (File.Exists(FileName))
                    {
                        MainMenuPanel.SetActive(false);
                        OpenItemMenuFlg = false;
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
                        LoadManager.Instance.OpenLoadMenuFlg = false;
                        OpenMenuFlg = false;
                        ActiveMenu = MenuType.MainMenu;
                        Time.timeScale = 1;
                    }

                    else
                    {
                        BackToMenu();
                    }
                    break;
                case 1:
                    FileName = LoadManager.Instance.LoadPath("/PlayerData2.json");
                    if (File.Exists(FileName))
                    {
                        MainMenuPanel.SetActive(false);
                        OpenItemMenuFlg = false;
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
                        LoadManager.Instance.OpenLoadMenuFlg = false;
                        OpenMenuFlg = false;
                        ActiveMenu = MenuType.MainMenu;
                        Time.timeScale = 1;
                    }

                    else
                    {
                        BackToMenu();
                    }
                    break;
                case 2:
                    FileName = LoadManager.Instance.LoadPath("/PlayerData3.json");
                    if (File.Exists(FileName))
                    {
                        MainMenuPanel.SetActive(false);
                        OpenItemMenuFlg = false;
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
                        LoadManager.Instance.OpenLoadMenuFlg = false;
                        OpenMenuFlg = false;
                        ActiveMenu = MenuType.MainMenu;
                        Time.timeScale = 1;
                    }

                    else
                    {
                        BackToMenu();
                    }
                    break;
            }
        }

        else
        {
            MainMenuPanel.SetActive(false);
            OpenItemMenuFlg = false;
            SaveMenuPanel.SetActive(false);
            LoadMenuPanel.SetActive(false);
            LoadManager.Instance.OpenLoadMenuFlg = false;
            OpenMenuFlg = false;
            ActiveMenu = MenuType.MainMenu;
            Time.timeScale = 1;
        }
    }
}
