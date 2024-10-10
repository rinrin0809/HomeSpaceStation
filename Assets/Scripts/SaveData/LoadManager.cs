using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadManager : MonoBehaviour
{
    [SerializeField] public GameObject GameObj;

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

        // �f�X�N�g�b�v�̃p�X���擾
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        string JsonPath = FolderPath + Name;

        // �t�@�C�������݂���ꍇ�A�ǂݍ��݂ƈʒu�̕���
        if (File.Exists(JsonPath))
        {
            string Json = File.ReadAllText(JsonPath);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            // �ǂݍ��񂾃f�[�^���v���C���[�̈ʒu�ɔ��f
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