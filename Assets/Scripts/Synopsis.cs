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
            // TextMeshProUGUI��RectTransform�𑀍�
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.anchoredPosition;
            newPosition.y = Pos.y;
            rectTransform.anchoredPosition = newPosition;
        }
        else
        {
            // Image��RectTransform�𑀍�
            RectTransform rectTransform = image.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.anchoredPosition;
            newPosition.y = Pos.y;
            rectTransform.anchoredPosition = newPosition;
        }
    }

    // TextMeshProUGUI��L�����܂��͖�����
    public void SetTextActive(bool isActive)
    {
        if (text != null)
        {
            text.enabled = isActive;
        }
    }

    // Image��L�����܂��͖�����
    public void SetImageActive(bool isActive)
    {
        if (image != null)
        {
            image.enabled = isActive;
        }
    }
}
