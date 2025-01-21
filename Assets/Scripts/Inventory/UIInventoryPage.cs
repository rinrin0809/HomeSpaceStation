using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static UnityEditor.Progress;
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

    public List<UIInventoryItem> listUIItems = new List<UIInventoryItem>();

    [SerializeField]
    private int Num = 0;

    //�ړ����̃C���^�[�o���̎��ԏ��
    const float MAX_TIME = 10.0f;
    //�ړ����̃C���^�[�o���̎���
    float time = MAX_TIME;

    private string ShowName = "";

    // Start is called before the first frame update
    void Start()
    {
        inventory.OnInventoryUpdated += UpdateUI;
    }

    private void Awake()
    {
        itemDescription.ResetDescription();
    }

    private void Update()
    {
        MoveNum();
    }

    // �ݒ肵�Ă���T�C�Y���X���b�g����
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("slot����");
            uiItem.transform.SetParent(contentPanel);
            listUIItems.Add(uiItem);
            Debug.Log("listCoutnt:" + listUIItems.Count);
        }
    }

    // �������X�V
    internal void UpdateDescription(string name, string description, Sprite ItemSprite)
    {
        itemDescription.SetDescription(name, description, ItemSprite);

    }

    internal void UpdateData(int itemIndex, string itemName/*, Sprite ItemSprite*/)
    {

        if (listUIItems.Count > itemIndex != null)
        {
           
            listUIItems[itemIndex].SetData(itemName/*, ItemSprite*/);
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
                uiItem.SetData(item.item.name/*,item.item.ItemSprite*/);


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
        Debug.Log("true");
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


    private void MoveNum()
    {
        if (inventory.GetInventoryItems().Count == 0) return;

        time--;
        //if (MenuManager.Instance.GetActiveMenu() == MenuType.ItemMenu)
        {
            if (Num > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    if (time < 0.0f)
                    {
                        time = MAX_TIME;
                        Num -= 1;
                    }
                }
            }

            else if (Num == 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    time = MAX_TIME;

                    // Find the first non-null item in inventory and set it to Num
                    for (int i = 0; i < inventory.GetInventoryItems().Count; i++)
                    {
                        if (inventory.GetInventoryItems()[i].item != null)
                        {
                            Num = i;
                        }
                    }
                }
            }

            if (Num < listUIItems.Count - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    if (time < 0.0f)
                    {
                        time = MAX_TIME;
                        Num += 1;

                        // Check if the selected item is null
                        if (inventory.GetInventoryItems()[Num].item == null)
                        {
                            Num = 0; // Reset Num if the item at the current index is null
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Num = 0;
            }

            else
            {
                UpdateItemColors();
            }
        }

        if (inventory.GetInventoryItems()[Num].item != null)
        {
            // �A�C�e���̐����Ăяo��
            string itemname = inventory.GetInventoryItems()[Num].item.name;
            string itenDes = inventory.GetInventoryItems()[Num].item.Descripton;
            Sprite itemImage = inventory.GetInventoryItems()[Num].item.ItemSprite;
            UpdateDescription(itemname, itenDes,itemImage);
        }
    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < listUIItems.Count; i++)
        {
            Image itemImage = listUIItems[i].GetComponent<Image>();

            if (itemImage != null)
            {
                // Num�Ԗڂ̃A�C�e���͗΁A����ȊO�͔��ɐݒ�
                itemImage.color = (i == Num) ? Color.green : Color.white;
            }
        }
    }
}
