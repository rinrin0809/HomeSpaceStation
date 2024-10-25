using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventoryPage : MonoBehaviour
{
    //slot 
    [SerializeField]
    private UIInventoryItem slotPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    // 説明文
    [SerializeField]
    private UIInventoryDescription itemDescription;

    // inventoryのインスタンスを保持
    public InventryData inventory;

    List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();

    // Start is called before the first frame update
    void Start()
    {
        inventory.OnInventoryUpdated += UpdateUI;
    }

    private void Awake()
    {
        itemDescription.ResetDescription();
    }

    // 設定しているサイズ分スロット複製
    public void InitializeInventoryUI(int inventorysize)
    {
        for(int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listUIItems.Add(uiItem);
        }
       
        
    }

    // 説明文更新
    internal void UpdateDescription(int itemIndex, string name, string description)
    {
        itemDescription.SetDescription(name, description);
       
    }

    internal void UpdateData(int itemIndex,string itemName)
    {
        if (listUIItems.Count > itemIndex != null)
        {
            listUIItems[itemIndex].SetData(itemName);
        }
    }

    // インベントリ―更新
    private void UpdateUI(Dictionary<int, InventoryItem> updateInventory)
    {
        foreach(var entory in updateInventory)
        {
            int index = entory.Key; // インベントリ内のアイテムインデックス
            InventoryItem item = entory.Value; // インベントリ内のアイテム

            UIInventoryItem uiItem = listUIItems[index]; //　UiInventoryItemのリストから対応するUIInventoryItemを取得
            uiItem.SetData(item.item.name);

        }
    }

   public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        Time.timeScale = 0.0f;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }



}
