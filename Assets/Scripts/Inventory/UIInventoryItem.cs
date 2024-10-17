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

    // �I�����Ă���̂��킩��₷������UI
    //[SerializeField]
    //private Image borderImage;

    // ������
    // �A�C�e���̑I������
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
