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
    //�C�x���g
    public EventData Event;
    // 3�̌ʂ�Button�ϐ�
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
        // "Save1" �Ƃ����^�O�����{�^����SaveButton1�ɑ��
        SaveButton1 = GameObject.Find("Save1").GetComponent<Button>();
        SaveButton2 = GameObject.Find("Save2").GetComponent<Button>();
        SaveButton3 = GameObject.Find("Save3").GetComponent<Button>();

        ButtonInitialize();
    }

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData1()
    {
        SaveData("/PlayerData1.json");
        Debug.Log("save1");
    }

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData2()
    {
        SaveData("/PlayerData2.json");
        Debug.Log("save2");
    }

    // �v���C���[�f�[�^��ۑ�
    public void SavePlayerData3()
    {
        SaveData("/PlayerData3.json");
        Debug.Log("save3");
    }

    private void SaveData(string Name)
    {
        GameObj = GameObject.FindGameObjectWithTag("Player");

        // �v���C���[�̈ʒu��SaveData�Ɋi�[
        SaveData Data = new SaveData
        {
            PosX = GameObj.transform.position.x,
            PosY = GameObj.transform.position.y,
            PosZ = GameObj.transform.position.z,
            InventoryItems = Inventory.GetInventoryItems(),
            EventDatas = Event.GetEvents(),
            SceneName = SceneManager.GetActiveScene().name
        };

        // SaveData��Json�`���ɕϊ�
        string Json = JsonUtility.ToJson(Data);

        // �f�X�N�g�b�v�̃p�X���擾
        string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // MaterialExporter �t�H���_�̃p�X���擾
        string FolderPath = Path.Combine(DesktopPath, "4008", "SaveData");

        // �t�H���_�����݂��Ȃ��ꍇ�͍쐬
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        // Json�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(FolderPath + Name, Json);
    }

    // �{�^���̏����ݒ�
    public void ButtonInitialize()
    {
        if (SaveButton1) SaveButton1.onClick.AddListener(OnClickButton1);
        if (SaveButton2) SaveButton2.onClick.AddListener(OnClickButton2);
        if (SaveButton3) SaveButton3.onClick.AddListener(OnClickButton3);
    }

    // �e�{�^���������ꂽ���̏���
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
