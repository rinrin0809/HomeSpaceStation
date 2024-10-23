using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    // インベントリUI参照
    [SerializeField]
    public UIInventoryPage inventoryUI;
    [SerializeField]
    private InventryData inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    // 仮置きのインベントリを開くボタン
    [SerializeField]
    private Button inventoryButton;

    // Start is called before the first frame update
    void Start()
    {
        PrepareUI();
        inventoryButton.onClick.AddListener(OnClickButton);
        inventoryUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        //Debug.Log("サイズをセット" + inventoryData.Size);
    }
    
    public void ToggleInventoryUI()
    {
        if (inventoryUI.isActiveAndEnabled)
        {
            inventoryUI.Hide();
        }
        else
        {
            inventoryUI.Show();
           
            foreach(var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.Name);
            }
        }
    }
  
    private void OnClickButton()
    {
        ToggleInventoryUI();
    }

     
}
