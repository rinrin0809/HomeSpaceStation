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
            listUIItems[itemIndex].SetData(itemName);
        }
    }

    // �C���x���g���\�X�V
    private void UpdateUI(Dictionary<int, InventoryItem> updateInventory)
    {
        foreach(var entory in updateInventory)
        {
            int index = entory.Key; // �C���x���g�����̃A�C�e���C���f�b�N�X
            InventoryItem item = entory.Value; // �C���x���g�����̃A�C�e��

            UIInventoryItem uiItem = listUIItems[index]; //�@UiInventoryItem�̃��X�g����Ή�����UIInventoryItem���擾
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
