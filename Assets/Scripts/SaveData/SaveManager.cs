using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] public GameObject GameObj;
    [SerializeField] private InventryData Inventory;
    //イベント
    public EventData Event;
    // 3つの個別のButton変数
    [SerializeField] private Button SaveButton1;
    [SerializeField] private Button SaveButton2;
    [SerializeField] private Button SaveButton3;

    private int LengthNum = 0;

    public bool OpenSaveMenuFlg = false;

    public void SetLengthNum(int Num)
    {
        LengthNum = Num;
    }

    public int GetLengthNum()
    {
        return LengthNum;
    }

    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!OpenSaveMenuFlg) return;
        // "Save1" というタグを持つボタンをSaveButton1に代入
        SaveButton1 = GameObject.Find("Save1").GetComponent<Button>();
        SaveButton2 = GameObject.Find("Save2").GetComponent<Button>();
        SaveButton3 = GameObject.Find("Save3").GetComponent<Button>();

        ButtonInitialize();
    }

    // プレイヤーデータを保存
    public void SavePlayerData1()
    {
        SaveData("/PlayerData1.json");
        Debug.Log("save1");
    }

    // プレイヤーデータを保存
    public void SavePlayerData2()
    {
        SaveData("/PlayerData2.json");
        Debug.Log("save2");
    }

    // プレイヤーデータを保存
    public void SavePlayerData3()
    {
        SaveData("/PlayerData3.json");
        Debug.Log("save3");
    }

    private void SaveData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーの位置をSaveDataに格納
        SaveData Data = new SaveData
        {
            PosX = GameObj.transform.position.x,
            PosY = GameObj.transform.position.y,
            PosZ = GameObj.transform.position.z,
            InventoryItems = Inventory.GetInventoryItems(),
            EventDatas = Event.GetEvents(),
            SceneName = SceneManager.GetActiveScene().name
        };

        // SaveDataをJson形式に変換
        string Json = JsonUtility.ToJson(Data);

        // デスクトップのパスを取得
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // MaterialExporter フォルダのパスを取得
        string FolderPath = Path.Combine(DesktopPath, "4008", "SaveData");

        // フォルダが存在しない場合は作成
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        // Jsonデータをファイルに保存
        File.WriteAllText(FolderPath + Name, Json);
    }

    // ボタンの初期設定
    public void ButtonInitialize()
    {
        if (SaveButton1) SaveButton1.onClick.AddListener(OnClickButton1);
        if (SaveButton2) SaveButton2.onClick.AddListener(OnClickButton2);
        if (SaveButton3) SaveButton3.onClick.AddListener(OnClickButton3);
    }

    // 各ボタンが押された時の処理
    private void OnClickButton1()
    {
        SavePlayerData1();
    }

    private void OnClickButton2()
    {
        SavePlayerData2();
    }

    private void OnClickButton3()
    {
        SavePlayerData3();
    }
}
