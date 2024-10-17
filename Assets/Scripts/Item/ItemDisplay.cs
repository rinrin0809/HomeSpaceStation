
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Item itemData; // アイテムデータ参照

    [SerializeField]
    private SpriteRenderer sp;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            sp.sprite = itemData.ItemSprite;
            Debug.Log("アイテムの画像が設定されました");
        }

        player = FindObjectOfType<Player>();


    }

    public void PickUpItem()
    {
        if (itemData != null && player != null)
        {
            player.AddItemInventory(itemData);
            Debug.Log("アイテムインベントリに追加しました");
        }
        else if(itemData != null)
        {
            Debug.Log("playerない");
        }
        else if (player != null)
        {
            Debug.Log("アイテムない");
        }
        else
        {
            Debug.Log("なんもない");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
}
