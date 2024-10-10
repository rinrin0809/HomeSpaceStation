using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadManager : MonoBehaviour
{
    [SerializeField] public GameObject GameObj;

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

        // デスクトップのパスを取得
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        string JsonPath = FolderPath + Name;

        // ファイルが存在する場合、読み込みと位置の復元
        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            // 読み込んだデータをプレイヤーの位置に反映
            Vector3 position = new Vector3(data.PosX, data.PosY, data.PosZ);
            GameObj.transform.position = position;

            Debug.Log("Player position loaded: " + Json);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}