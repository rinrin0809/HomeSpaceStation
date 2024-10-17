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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        itemDescription.ResetDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
