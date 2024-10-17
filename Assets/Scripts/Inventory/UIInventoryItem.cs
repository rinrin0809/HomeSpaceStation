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

    // 選択しているのをわかりやすくするUI
    //[SerializeField]
    //private Image borderImage;

    // 仮実装
    // アイテムの選択処理
    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        
    }

    public void ResetData()
    {
        itemNameText.text = "";
        empty = true;
    }

    public void SetData(string ItemName)
    {
        if (!string.IsNullOrEmpty(ItemName))
        {
            itemNameText.text = ItemName;
            empty = false;
        }
        else
        {
            ResetData();
        }
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
