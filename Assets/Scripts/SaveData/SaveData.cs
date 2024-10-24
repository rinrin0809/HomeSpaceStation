using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    // プレイヤーの位置
    public float PosX;
    public float PosY;
    public float PosZ;

    // インベントリデータ
    public List<InventoryItem> InventoryItems;
}