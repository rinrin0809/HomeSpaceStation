using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    [SerializeField] public GameObject GameObj;

    private int SideNum = 0;
    private bool SideFlg = false;

    public void SetSideNum(int Num)
    {
        SideNum = Num;
    }

    public int GetSideNum()
    {
        return SideNum;
    }

    public void SetSideFlg(bool Flg)
    {
        SideFlg = Flg;
    }

    public bool GetSideFlg()
    {
        return SideFlg;
    }

    private int LengthNum = 0;

    public void SetLengthNum(int Num)
    {
        LengthNum = Num;
    }

    public int GetLengthNum()
    {
        return LengthNum;
    }

    //NewGame�{�^���������ꂽ���̃t���O
    public bool NewGamePushFlg = false;

    void Start()
    {
        //������
        Instance = this;
        NewGamePushFlg = false;
    }

    // �Z�[�u�f�[�^��ǂݍ��݁A�v���C���[�̃f�[�^�𕜌�����
    public void LoadPlayerData1()
    {
        LoadData("/PlayerData1.json");
    }

    // �Z�[�u�f�[�^��ǂݍ��݁A�v���C���[�̃f�[�^�𕜌�����
    public void LoadPlayerData2()
    {
        LoadData("/PlayerData2.json");
    }

    // �Z�[�u�f�[�^��ǂݍ��݁A�v���C���[�̃f�[�^�𕜌�����
    public void LoadPlayerData3()
    {
        LoadData("/PlayerData3.json");
    }

    public void LoadData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        string JsonPath = LoadPath(Name);

        // �t�@�C�������݂���ꍇ�A�ǂݍ��݂ƈʒu�̕���
        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            // �ǂݍ��񂾃f�[�^���v���C���[�̈ʒu�ɔ��f
            Vector3 position = new Vector3(data.PosX, data.PosY, data.PosZ);
            GameObj.transform.position = position;
        }
    }

    //�^�C�g������Q�[���V�[���ɑJ�ڂ����ۂ̃��[�h
    public void TitleToGameLoadData()
    {
        if (SideFlg)
        {
            switch (SideNum)
            {
                case 0:
                    LoadPlayerData1();
                    break;
                case 1:
                    LoadPlayerData2();
                    break;
                case 2:
                    LoadPlayerData3();
                    break;
            }
        }

        else
        {
            switch (LengthNum)
            {
                case 0:
                    LoadPlayerData1();
                    break;
                case 1:
                    LoadPlayerData2();
                    break;
                case 2:
                    LoadPlayerData3();
                    break;
            }
        }
    }

    //NewGame�{�^���������ꂽ��
    public void NewGameButtonPush()
    {
        NewGamePushFlg = true;
        Debug.Log("NewGamePushFlg" + NewGamePushFlg);
    }

    //LoadGame�{�^���������ꂽ��
    public void LoadGameButtonPush()
    {
        NewGamePushFlg = false;
        Debug.Log("NewGamePushFlg" + NewGamePushFlg);
    }

    //�p�X�̎擾
    public string LoadPath(string Name)
    {
        // �f�X�N�g�b�v�̃p�X���擾
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        string JsonPath = FolderPath + Name;

        return JsonPath;
    }
}