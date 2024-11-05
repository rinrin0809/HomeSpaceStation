using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public GameObject exclamationMark; // !マーク 
    public Transform ActionObject;
    public Vector3 offset = new Vector3(0, 1, 0);

    private GameObject exclamationMarkClone; // クローン参照
    [SerializeField]
    private Canvas canvas; // canvas 参照
    float scale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Canvasの取得
        canvas = FindObjectOfType<Canvas>();
     

        // Canvasの存在確認
        if (canvas != null)
        {
            exclamationMarkClone = Instantiate(exclamationMark,canvas.transform);
            exclamationMarkClone.transform.localScale *= scale;
            exclamationMarkClone.SetActive(false);
        }
        else
        {
            Debug.Log("canvasがない");
        }
     
        
    }

    // Update is called once per frame
    void Update()
    {
        exclamationMarkClone.transform.position = Camera.main.WorldToScreenPoint(ActionObject.transform.position + offset);
       
    }

    // 当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exclamationMarkClone.SetActive(true);
            Debug.Log("！マーク表示");
        }
    }

    // 当たり判定が外れた時の処理
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            exclamationMarkClone.SetActive(false);
            Debug.Log("！マーク非表示");
        }
    }
}
