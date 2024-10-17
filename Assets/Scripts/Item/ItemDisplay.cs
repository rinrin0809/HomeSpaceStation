
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Item itemData; // �A�C�e���f�[�^�Q��

    [SerializeField]
    private SpriteRenderer sp;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            sp.sprite = itemData.ItemSprite;
            Debug.Log("�A�C�e���̉摜���ݒ肳��܂���");
        }

        player = FindObjectOfType<Player>();


    }

    public void PickUpItem()
    {
        if (itemData != null && player != null)
        {
            player.AddItemInventory(itemData);
            Debug.Log("�A�C�e���C���x���g���ɒǉ����܂���");
        }
        else if(itemData != null)
        {
            Debug.Log("player�Ȃ�");
        }
        else if (player != null)
        {
            Debug.Log("�A�C�e���Ȃ�");
        }
        else
        {
            Debug.Log("�Ȃ���Ȃ�");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
}
