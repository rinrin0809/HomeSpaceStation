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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepareUI()
    {
        
    }
}
