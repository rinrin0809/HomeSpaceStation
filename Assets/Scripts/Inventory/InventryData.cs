using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[CreateAssetMenu]
public class InventryData : ScriptableObject
{
    // リスト
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    //アイテムのセッター
    public void SetInventoryItems(List<InventoryItem> Item)
    {
        inventoryItems = Item;
    }

    //アイテムのゲッター
    public List<InventoryItem> GetInventoryItems()
    {
        return inventoryItems;
    }

    // スロット数
    [field: SerializeField]
    public int Size;

    public int GetSize()
    {
        return Size;
    }

    public event Action OnInventoryChanged; // 全体通知
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
    public event Action<int> OnItemRemoved;

    //public Item itemSo;

    // インベントリの初期化
    public void Initilaize()
    {
        if (inventoryItems == null || inventoryItems.Count == 0)
        {
            inventoryItems = new List<InventoryItem>();
            for(int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
    }

    // アイテムをインベントリに追加
    public void AddItem(Item item, bool Flg)
    {
        // 新しいスロットにアイテムを追加
        int emptySlotIndex = FindEmptySlot();
        if (emptySlotIndex != -1)
        {
            inventoryItems[emptySlotIndex] = new InventoryItem()
            {
                item = item,
                ItemFlg = Flg
            };
            // インベントリが更新されたことを通知
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
            
        }
        else
        {
            // 新しいスロットがない場合追加を追加
            AddItem(item,Flg);
        }
    }

    //空いてるスロットにインデックスを返す
    private int FindEmptySlot()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                return i;
            }
        }
        return -1;
    }

    public void RemoveItemByName(string itemName)
    {
        for(int i =0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].IsEmpty && inventoryItems[i].item.Name == itemName)
            {
                // アイテムを削除する
                inventoryItems[i] = InventoryItem.GetEmptyItem();
                OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
                return;
            }
        }
        Debug.Log("アイテムが見つからない");
    }

    // 現在のインベントリの状態を取得する
    
    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    // アイテムリセット
    public void ResetInventory()
    {
        Debug.Log("aaaaaaaaaaaaaa");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i] = InventoryItem.GetEmptyItem();
        }

        
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }
}

[System.Serializable]
public struct InventoryItem
{
    public Item item;

    //[SerializeField]
    public bool IsEmpty => item == null;

    public bool ItemFlg;

    // 新しいアイテムを設定
    public InventoryItem SetItem(Item newItem)
    {
        return new InventoryItem()
        {
            item = newItem,
        };
    }

    // 空のアイテムスロットを作成　
    public static InventoryItem GetEmptyItem()
    {
        return new InventoryItem
        {
            item = null
        };
    }
}
