using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using System.Runtime.CompilerServices;

public class UIInventoryPage : MonoBehaviour
{
    //slot 
    [SerializeField]
    private UIInventoryItem slotPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    // ������
    [SerializeField]
    private UIInventoryDescription itemDescription;
  

    // inventory�̃C���X�^���X��ێ�
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

    // �ݒ肵�Ă���T�C�Y���X���b�g����
    public void InitializeInventoryUI(int inventorysize)
    {
        for(int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listUIItems.Add(uiItem);
        }
       
        
    }

    // �������X�V
    internal void UpdateDescription(int itemIndex, string name, string description)
    {
        itemDescription.SetDescription(name, description);
       
    }

    internal void UpdateData(int itemIndex,string itemName)
    {
       
        if (listUIItems.Count > itemIndex != null)
        {
            Debug.Log("1");
            listUIItems[itemIndex].SetData(itemName);
        }
    }

    // �C���x���g���\�X�V
    private void UpdateUI(Dictionary<int, InventoryItem> updateInventory)
    {
       
        for (int i = 0; i < listUIItems.Count; i++)
        {
            if (updateInventory.ContainsKey(i))
            {
                InventoryItem item = updateInventory[i]; // �C���x���g�����̃A�C�e��
                UIInventoryItem uiItem = listUIItems[i]; // UiInventoryItem�̃��X�g����Ή�����UIInventoryItem���擾
                uiItem.SetData(item.item.name);
            }
            else
            {
                // �A�C�e�����Ȃ��ꍇ�A�X���b�g�����Z�b�g
                listUIItems[i].ResetData();
            }
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
        // �S�X���b�g�̃f�[�^�����Z�b�g
        foreach (var item in listUIItems)
        {
            item.ResetData();                            
        }
        Time.timeScale = 1.0f;
    }



}
