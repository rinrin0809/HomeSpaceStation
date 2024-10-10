using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Item itemData; // アイテムデータ参照

    [SerializeField]
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            sp.sprite = itemData.ItemSprite;
            Debug.Log("アイテムの画像が設定されました");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
