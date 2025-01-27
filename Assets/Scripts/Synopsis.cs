using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Synopsis : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public Image image;

    private Vector3 Pos;

    [SerializeField]
    float Speed = 5.0f;

    private float MAX_SPEED = 125.0f;
    private float NORMAL_SPEED = 15.0f;

    [SerializeField]
    bool SpacePushFlg = false;

    private RectTransform rectTransform;

    void Start()
    {
        Pos = new Vector3(0.0f, -960.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Pos.y += Speed * Time.deltaTime;

        // ImageÇÃRectTransformÇëÄçÏ
        rectTransform = image.GetComponent<RectTransform>();
        Vector3 newPosition = rectTransform.anchoredPosition;
        newPosition.y = Pos.y;
        rectTransform.anchoredPosition = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (SpacePushFlg)
            //{
            //    SpacePushFlg = false;
            //}

            //else
            //{
            //    SpacePushFlg = true;
            //}
            SpacePushFlg = true;
        }

        if(SpacePushFlg)
        {
            Speed = MAX_SPEED;
        }

        else
        {
            Speed = NORMAL_SPEED;
        }
    }
}
