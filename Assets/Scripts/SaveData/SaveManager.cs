using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [SerializeField] public GameObject GameObj;

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData1()
    {
        SaveData("/PlayerData1.json");
    }

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData2()
    {
        SaveData("/PlayerData2.json");
    }

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData3()
    {
        SaveData("/PlayerData3.json");
    }

    private void SaveData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        // �v���C���[�̈ʒu��SaveData�Ɋi�[
        SaveData Data = new SaveData
        {
            PosX = GameObj.transform.position.x,
            PosY = GameObj.transform.position.y,
            PosZ = GameObj.transform.position.z
        };

        // SaveData��Json�`���ɕϊ�
        string Json = JsonUtility.ToJson(Data);

        // �f�X�N�g�b�v�̃p�X���擾
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // MaterialExporter �t�H���_�̃p�X���擾
        string FolderPath = Path.Combine(DesktopPath, "HomeSpaceStation", "SaveData");

        // �t�H���_�����݂��Ȃ��ꍇ�͍쐬
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        // Json�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(FolderPath + Name, Json);
    }
}
