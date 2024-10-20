using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // シーンをまたいでこのオブジェクトを保持する
    }
}
