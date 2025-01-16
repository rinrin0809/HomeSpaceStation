using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryDescription : MonoBehaviour
{

    // 選択中のアイテム名
    [SerializeField]
    private TMP_Text ItemName;
    // アイテム説明
    [SerializeField]
    private TMP_Text description;
    //スプライト
    [SerializeField]
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化？
        ResetDescription();
    }

    public void ResetDescription()
    {
        ItemName.text = "";
        description.text = "";
    }

    public void SetDescription(string itemName, string itemdescription,Sprite ItemSprite)
    {
        ItemName.text = itemName;
        description.text = itemdescription;
        image.sprite = ItemSprite;
    }

    
}
