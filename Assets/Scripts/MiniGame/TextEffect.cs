using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    //��莞�Ԍ�ɏ����I�u�W�F�N�g
    [SerializeField] GameObject Obj;
    
    //�\�����鎞��
    [SerializeField]
    private const float MAX_RENDER_TIME = 5.0f;
    [SerializeField]
    private float RenderTime = MAX_RENDER_TIME;

    //�������炷���x
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
