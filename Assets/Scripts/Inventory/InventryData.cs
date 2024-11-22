using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[CreateAssetMenu]
public class InventryData : ScriptableObject
{
    // ���X�g
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    //�A�C�e���̃Z�b�^�[
    public void SetInventoryItems(List<InventoryItem> Item)
    {
        inventoryItems = Item;
    }

    //�A�C�e���̃Q�b�^�[
    public List<InventoryItem> GetInventoryItems()
    {
        return inventoryItems;
    }

    // �X���b�g��
    [field: SerializeField]
    public int Size;

    public int GetSize()
    {
        return Size;
    }

    public event Action OnInventoryChanged; // �S�̒ʒm
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
    public event Action<int> OnItemRemoved;

    //public Item itemSo;

    // �C���x���g���̏�����
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

    // �A�C�e�����C���x���g���ɒǉ�
    public void AddItem(Item item, bool Flg)
    {
        // �V�����X���b�g�ɃA�C�e����ǉ�
        int emptySlotIndex = FindEmptySlot();
        if (emptySlotIndex != -1)
        {
            inventoryItems[emptySlotIndex] = new InventoryItem()
            {
                item = item,
                ItemFlg = Flg
            };
            // �C���x���g�����X�V���ꂽ���Ƃ�ʒm
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
            
        }
        else
        {
            // �V�����X���b�g���Ȃ��ꍇ�ǉ���ǉ�
            AddItem(item,Flg);
        }
    }

    //�󂢂Ă�X���b�g�ɃC���f�b�N�X��Ԃ�
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
                // �A�C�e�����폜����
                inventoryItems[i] = InventoryItem.GetEmptyItem();
                OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
                return;
            }
        }
        Debug.Log("�A�C�e����������Ȃ�");
    }

    // ���݂̃C���x���g���̏�Ԃ��擾����
    
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

    // �A�C�e�����Z�b�g
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

    // �V�����A�C�e����ݒ�
    public InventoryItem SetItem(Item newItem)
    {
        return new InventoryItem()
        {
            item = newItem,
        };
    }

    // ��̃A�C�e���X���b�g���쐬�@
    public static InventoryItem GetEmptyItem()
    {
        return new InventoryItem
        {
            item = null
        };
    }
}
