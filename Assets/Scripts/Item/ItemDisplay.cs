using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Item itemData; // �A�C�e���f�[�^�Q��

    [SerializeField]
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            sp.sprite = itemData.ItemSprite;
            Debug.Log("�A�C�e���̉摜���ݒ肳��܂���");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
