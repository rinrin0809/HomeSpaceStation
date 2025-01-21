using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
{
    // �A�C�e�����̕\���e�L�X�g
    [SerializeField]
    private TMP_Text itemNameText;
    [SerializeField]
    Image ItemImage;
    // �I�����Ă���̂��킩��₷������UI
    //[SerializeField]
    //private Image borderImage;

    // ������
    // �A�C�e���̑I������
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

    // �N���b�N�C�x���g����
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (empty)
            return;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this); // �E�N���b�N����
        }
        else
        {
            OnItemClicked?.Invoke(this); // �ʏ�N���b�N����
        }
    }

   
}
