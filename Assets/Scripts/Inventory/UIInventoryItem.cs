using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
{
    // アイテム名の表示テキスト
    [SerializeField]
    private TMP_Text itemNameText;
    [SerializeField]
    Image ItemImage;
    // 選択しているのをわかりやすくするUI
    //[SerializeField]
    //private Image borderImage;

    // 仮実装
    // アイテムの選択処理
    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        ItemImage.gameObject.SetActive(false);
    }

    public void ResetData()
    {
        //Debug.Log("reset");
        itemNameText.text = "";
        empty = true;
    }

    public void textReset()
    {
        //Debug.Log("reset");
        itemNameText.text = "";
    }

    public void SetData(string ItemName/*,Sprite ItemSprite*/)
    {
        //Debug.Log("3");
        ItemImage.gameObject.SetActive(true);
        itemNameText.text = ItemName;
        //ItemImage.sprite = ItemSprite;
        empty = string.IsNullOrEmpty(ItemName);
      
    }

    // クリックイベント処理
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (empty)
            return;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this); // 右クリック処理
        }
        else
        {
            OnItemClicked?.Invoke(this); // 通常クリック処理
        }
    }

   
}
