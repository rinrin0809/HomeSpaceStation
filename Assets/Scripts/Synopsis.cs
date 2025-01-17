using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Synopsis : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Image image;

    private Vector3 Pos;

    [SerializeField]
    float Speed = 5.0f;

    [SerializeField]
    bool TextFlg = false;
    void Start()
    {
        if (TextFlg)
        {
            Pos = new Vector3(0.0f, -210.0f, 0.0f);
            SetImageActive(false);
        }

        else
        {
            Pos = new Vector3(0.0f, -960.0f, 0.0f);
            SetTextActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pos.y += Speed * Time.deltaTime;

        if (TextFlg == true)
        {
            // TextMeshProUGUIのRectTransformを操作
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.anchoredPosition;
            newPosition.y = Pos.y;
            rectTransform.anchoredPosition = newPosition;
        }
        else
        {
            // ImageのRectTransformを操作
            RectTransform rectTransform = image.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.anchoredPosition;
            newPosition.y = Pos.y;
            rectTransform.anchoredPosition = newPosition;
        }
    }

    // TextMeshProUGUIを有効化または無効化
    public void SetTextActive(bool isActive)
    {
        if (text != null)
        {
            text.enabled = isActive;
        }
    }

    // Imageを有効化または無効化
    public void SetImageActive(bool isActive)
    {
        if (image != null)
        {
            image.enabled = isActive;
        }
    }
}
