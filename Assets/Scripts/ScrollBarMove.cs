using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを扱う際に必要

public class ScrollBarMove : MonoBehaviour
{
    public Scrollbar scrollBar;

    [SerializeField] private int count;
    [SerializeField] private float timeReset = 0.1f;
    [SerializeField] private float time;
    [SerializeField] private float Speed = 0.01f;

    private const int ZERO = 0;
    private const int ONE = 1;
    private const int NINE = 9;

    private const float ZERO_TIME = 0.0f;

    void Update()
    {
        ////メニューを開いている時
        //if (MenuManager.Instance.GetOpenMenuFlg())
        //{
        //    time += Speed;

        //    if (time > timeReset)
        //    {
        //        if (Input.GetKey(KeyCode.UpArrow))
        //        {
        //            if (count < ONE)
        //            {
        //                count++;
        //                time = ZERO_TIME;
        //            }
        //        }

        //        else if (Input.GetKey(KeyCode.DownArrow))
        //        {
        //            if (count > ZERO)
        //            {
        //                count--;
        //                time = ZERO_TIME;
        //            }
        //        }
        //    }
        //    scrollBar.value = count;
        //}

        //else
        //{
        //    count = ZERO;
        //}
    }
}
