using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�V�[���ł̉�ʂ̎��
public enum SceneType
{
    TitleMenu,
    LoadMenu,
    OverMenu,
    None
}

public class TitleSceneManager : MonoBehaviour
{
    //���[�h���j���[���J���Ă��邩�̃t���O
    bool OpenLoadMenuFlg = false;

    [SerializeField] GameObject ScenePanel;
    [SerializeField] GameObject LoadMenuPanel;
    public EventData eventflag;

    // ���݂ǂ̃V�[�����\������Ă���A�N�e�B�u����ێ�����
    private SceneType ActiveMenu = SceneType.None;

    //���݂ǂ̃��j���[���A�N�e�B�u����ێ�����
    public SceneType GetSceneActiveMenu()
    {
        return ActiveMenu;
    }

    void Start()
    {
        ScenePanel.SetActive(true);
        ActiveMenu = SceneType.TitleMenu;
    }

    //�X�V����
    void Update()
    {
        //���j���[�\��
        OpenLoadMenu();
    }

    //���j���[���\������Ă��邩�̃t���O�̏�Ԏ擾
    public bool GetOpenLoadMenuFlg()
    {
        //�G���^�[�L�[���������OpenMenuFlg��false�̎���true
        if (Input.GetKeyDown(KeyCode.Return) && !OpenLoadMenuFlg)
        {
            OpenLoadMenuFlg = true;
        }

        return OpenLoadMenuFlg;
    }

    //���j���[�\��
    public void OpenLoadMenu()
    {
        //OpenMenuFlg��true�̎��͍ŏ��̃��j���[�\��
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

        //M�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.M) && OpenLoadMenuFlg)
        {
            CloseMenu();
        }
    }

    //���[�h���j���[�\��
    public void LoadMenu()
    {
        ScenePanel.SetActive(false);
        LoadMenuPanel.SetActive(true);
        ActiveMenu = SceneType.LoadMenu;
        //Debug.Log("Open LoadMenu");
    }

    //���j���[�����
    public void CloseMenu()
    {
        ScenePanel.SetActive(true);
        LoadMenuPanel.SetActive(false);
        ActiveMenu = SceneType.TitleMenu;
        //Debug.Log("Close LoadMenu");
    }
}
