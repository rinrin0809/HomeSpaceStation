using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    [SerializeField] public GameObject GameObj;
    private int SideNum = 0;
    private bool SideFlg = false;
    [SerializeField] private InventryData Inventory;
    //イベント
    public EventData Event;
    [SerializeField] public string NextSceneName = "";
    public SaveData data;

    [SerializeField] private bool LoadPlayerFlg = false;

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

    [SerializeField]
    public bool NewGamePushFlg = false;

    public bool OpenLoadMenuFlg = false;

    void Start()
    {
        Instance = this;
        NewGamePushFlg = false;
    }

    public void LoadPlayerData1()
    {
        LoadData("/PlayerData1.json");
        Debug.Log("PlayerData1");
    }

    public void LoadPlayerData2()
    {
        LoadData("/PlayerData2.json");
        Debug.Log("PlayerData2");
    }

    public void LoadPlayerData3()
    {
        LoadData("/PlayerData3.json");
        Debug.Log("PlayerData3");
    }

    public void LoadData(string Name)
    {
        NewGamePushFlg = false;
        Debug.Log("NewGamePushFlg" + NewGamePushFlg);
        GameObj = GameObject.FindGameObjectWithTag("Player");

        string JsonPath = LoadPath(Name);

        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            data = JsonUtility.FromJson<SaveData>(Json);

            Vector3 position = new Vector3(data.PosX, data.PosY, data.PosZ);
            GameObj.transform.position = position;
            Inventory.SetInventoryItems(data.InventoryItems);
        }
    }

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

    public void LoadSavedScene()
    {
        // シーン遷移の前にセーブデータをロードしておく
        TitleToGameLoadData();

        // シーンを非同期でロードする
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextSceneName);

        // シーンがロードされるまで待機
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        TitleToGameLoadData();
    }

    public string LoadPath(string Name)
    {
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");
        string JsonPath = FolderPath + Name;
        return JsonPath;
    }

    public string GetFilePathBySideNum(int sideNum)
    {
        switch (sideNum)
        {
            case 0:
                Debug.Log("save1");
                return LoadManager.Instance.LoadPath("/PlayerData1.json");
            case 1:
                Debug.Log("save2");
                return LoadManager.Instance.LoadPath("/PlayerData2.json");
            case 2:
                Debug.Log("save3");
                return LoadManager.Instance.LoadPath("/PlayerData3.json");
            default:
                Debug.Log("save4");
                return "";
        }
    }

    public string GetFilePathByLengthNum(int lengthNum)
    {
        switch (lengthNum)
        {
            case 0:
                return LoadManager.Instance.LoadPath("/PlayerData1.json");
            case 1:
                return LoadManager.Instance.LoadPath("/PlayerData2.json");
            case 2:
                return LoadManager.Instance.LoadPath("/PlayerData3.json");
            default:
                return "";
        }
    }
}
