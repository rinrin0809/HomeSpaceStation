using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryDescription : MonoBehaviour
{

    // �I�𒆂̃A�C�e����
    [SerializeField]
    private TMP_Text ItemName;
    // �A�C�e������
    [SerializeField]
    private TMP_Text description;
    
    // Start is called before the first frame update
    void Start()
    {
        // �������H
        ResetDescription();
    }

    public void ResetDescription()
    {
        ItemName.text = "";
        description.text = "";
    }

    public void SetDescription(string itemName, string itemdescription)
    {
        ItemName.text = itemName;
        description.text = itemdescription;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}