using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class NextSceneNameLoad : MonoBehaviour
{
    private float checkInterval = 5f; // チェック間隔（秒）
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            
            timer = 0f;
        }
        UpdateSceneName();
    }

    private void UpdateSceneName()
    {
        if(!LoadManager.Instance)
        {
            if (LoadManager.Instance.GetSideFlg())
            {
                switch (LoadManager.Instance.GetSideNum())
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
                switch (LoadManager.Instance.GetLengthNum())
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
    }

    public void LoadPlayerData1()
    {
        LoadData("/PlayerData1.json");
    }

    public void LoadPlayerData2()
    {
        LoadData("/PlayerData2.json");
    }

    public void LoadPlayerData3()
    {
        LoadData("/PlayerData3.json");
    }

    public void LoadData(string Name)
    {
        string JsonPath = LoadManager.Instance.LoadPath(Name);

        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            LoadManager.Instance.NextSceneName = data.SceneName;
        }
    }
}
