using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [SerializeField] public GameObject GameObj;

    // プレイヤーデータを保存
    public void SavePlayerData1()
    {
        SaveData("/PlayerData1.json");
    }

    // プレイヤーデータを保存
    public void SavePlayerData2()
    {
        SaveData("/PlayerData2.json");
    }

    // プレイヤーデータを保存
    public void SavePlayerData3()
    {
        SaveData("/PlayerData3.json");
    }

    private void SaveData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーの位置をSaveDataに格納
        SaveData Data = new SaveData
        {
            PosX = GameObj.transform.position.x,
            PosY = GameObj.transform.position.y,
            PosZ = GameObj.transform.position.z
        };

        // SaveDataをJson形式に変換
        string Json = JsonUtility.ToJson(Data);

        // デスクトップのパスを取得
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // MaterialExporter フォルダのパスを取得
        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        // フォルダが存在しない場合は作成
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        // Jsonデータをファイルに保存
        File.WriteAllText(FolderPath + Name, Json);
    }
}
