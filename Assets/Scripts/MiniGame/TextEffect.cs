using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    //一定時間後に消すオブジェクト
    [SerializeField] GameObject Obj;
    
    //表示する時間
    [SerializeField]
    private const float MAX_RENDER_TIME = 5.0f;
    [SerializeField]
    private float RenderTime = MAX_RENDER_TIME;

    //↑を減らす速度
    [SerializeField]
    private const float SPEED = 5.0f;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Obj.activeSelf)
        {
            RenderTime -= SPEED * Time.deltaTime;

            if (RenderTime <= 0.0f) Obj.SetActive(false);
        }

        else
        {
            RenderTime = MAX_RENDER_TIME;
        }
    }
}
