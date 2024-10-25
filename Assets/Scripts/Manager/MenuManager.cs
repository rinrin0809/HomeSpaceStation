using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//���j���[�̎��
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

    //���j���[���J���Ă��邩�̃t���O
    private bool OpenMenuFlg = false;

    //���j���[���J���Ă���t���O�擾
    public bool GetOpenMenuFlg()
    {
        return OpenMenuFlg;
    }

    //�e��p�l���̐錾
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject ItemMenuPanel;
    [SerializeField] GameObject SaveMenuPanel;
    [SerializeField] GameObject LoadMenuPanel;

    // ���݂ǂ̃��j���[���A�N�e�B�u����ێ�����
    private MenuType ActiveMenu = MenuType.None;

    //���݂ǂ̃��j���[���A�N�e�B�u����ێ�����
    public MenuType GetActiveMenu()
    {
        return ActiveMenu;
    }

    void Start()
    {
        Instance = this;
    }

    //�X�V����
    void Update()
    {
        //���j���[�\��
        OpenMenu();
        //Debug.Log("Player position saved: " + ActiveMenu);
    }

    //���j���[���\������Ă��邩�̃t���O�̏�Ԏ擾
    public bool GetOpenFlg()
    {
        //M�L�[�i���j���������OpenMenuFlg��false�̎���true
        if (Input.GetKeyDown(KeyCode.M) && !OpenMenuFlg)
        {
            OpenMenuFlg = true;
        }

        //M�L�[�i���j���������OpenMenuFlg��true�̎���false
        else if (Input.GetKeyDown(KeyCode.M) && OpenMenuFlg)
        {
            OpenMenuFlg = false;
        }

        return OpenMenuFlg;
    }

    //���j���[�\��
    public void OpenMenu()
    {
        //OpenMenuFlg��true�̎��͍ŏ��̃��j���[�\��
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

        //OpenMenuFlg��false�̎��̓��j���[�����
        else
        {
            CloseMenu();
        }
    }

    //MenuPanel��ItemButton�������ꂽ�Ƃ��̏���
    public void ItemMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        ItemMenuPanel.SetActive(true);
        ActiveMenu = MenuType.ItemMenu;
    }

    //MenuPanel��SaveButton�������ꂽ�Ƃ��̏���
    public void SaveMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(true);
        ActiveMenu = MenuType.SaveMenu;
    }

    //MenuPanel��LoadButton�������ꂽ�Ƃ��̏���
    public void LoadMenuDescription()
    {
        MainMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(true);
        ActiveMenu = MenuType.LoadMenu;
    }

    //�ŏ��̃��j���[�\��
    public void BackToMenu()
    {
        MainMenuPanel.SetActive(true);
        ItemMenuPanel.SetActive(false);
        SaveMenuPanel.SetActive(false);
        LoadMenuPanel.SetActive(false);
        ActiveMenu = MenuType.MainMenu;
    }

    //���j���[�����
    public void CloseMenu()
    {
        //���[�h���j���[�̎�
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
                        ItemMenuPanel.SetActive(false);
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
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
                        ItemMenuPanel.SetActive(false);
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
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
                        ItemMenuPanel.SetActive(false);
                        SaveMenuPanel.SetActive(false);
                        LoadMenuPanel.SetActive(false);
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

        //����ȊO�̎�
        else
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
}
