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

    private List<InventoryItem> initialItems = new List<InventoryItem>();

    // 仮置きのインベントリを開くボタン
    [SerializeField]
    private Button inventoryButton;
    //[SerializeField]
    //private Button inventoryMenuButton;
    [SerializeField]
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        PrepareUI();
        if(inventoryButton) inventoryButton.onClick.AddListener(OnClickButton);
        //if(inventoryMenuButton) inventoryMenuButton.onClick.AddListener(OnClickButton);
        inventoryUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManager.Instance != null)
        {
            if (MenuManager.Instance.GetOpenMenuFlg())
            {
                obj.SetActive(true);
                OnClick();
            }

            else
            {
                obj.SetActive(false);
            }
        }
        
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        Debug.Log("サイズをセット" + inventoryData.Size);
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
                    item.Value.item.Name,item.Value.item.ItemSprite);
            }
        }
    }
  
    private void OnClickButton()
    {
        Debug.Log("true2");
        ToggleInventoryUI();
    }

    //押された時の処理
    void OnClick()
    {
        if (Input.GetKeyUp(KeyCode.Backspace) && MenuManager.Instance.GetOpenItemMenuFlg())
        {
            Debug.Log("Closing only the Item Menu, keeping Main Menu active");
            //if (inventoryMenuButton) inventoryMenuButton.onClick.Invoke();  // Toggle item menu button if necessary
            MenuManager.Instance.BackToMenu();
        }
    }
}
