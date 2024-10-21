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

    //NewGameボタンが押された時のフラグ
    public bool NewGamePushFlg = false;

    void Start()
    {
        //初期化
        Instance = this;
        NewGamePushFlg = false;
    }

    // セーブデータを読み込み、プレイヤーのデータを復元する
    public void LoadPlayerData1()
    {
        LoadData("/PlayerData1.json");
    }

    // セーブデータを読み込み、プレイヤーのデータを復元する
    public void LoadPlayerData2()
    {
        LoadData("/PlayerData2.json");
    }

    // セーブデータを読み込み、プレイヤーのデータを復元する
    public void LoadPlayerData3()
    {
        LoadData("/PlayerData3.json");
    }

    public void LoadData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        string JsonPath = LoadPath(Name);

        // ファイルが存在する場合、読み込みと位置の復元
        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            // 読み込んだデータをプレイヤーの位置に反映
            Vector3 position = new Vector3(data.PosX, data.PosY, data.PosZ);
            GameObj.transform.position = position;
        }
    }

    //タイトルからゲームシーンに遷移した際のロード
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

    //NewGameボタンが押された時
    public void NewGameButtonPush()
    {
        NewGamePushFlg = true;
        Debug.Log("NewGamePushFlg" + NewGamePushFlg);
    }

    //LoadGameボタンが押された時
    public void LoadGameButtonPush()
    {
        NewGamePushFlg = false;
        Debug.Log("NewGamePushFlg" + NewGamePushFlg);
    }

    //パスの取得
    public string LoadPath(string Name)
    {
        // デスクトップのパスを取得
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        string JsonPath = FolderPath + Name;

        return JsonPath;
    }
}