using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    // �v���C���[�̈ʒu
    public float PosX;
    public float PosY;
    public float PosZ;

    //�V�[����
    public string SceneName = "";

    // �C���x���g���f�[�^
    public List<InventoryItem> InventoryItems;
}