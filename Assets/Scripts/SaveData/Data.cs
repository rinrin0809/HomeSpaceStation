using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // このクラスのインスタンスを保持しておくための静的な変数
    private static Data instance;

    void Awake()
    {
        // 既にインスタンスが存在する場合、重複したオブジェクトを破棄する
        if (instance != null)
        {
            Destroy(gameObject); // 重複している場合は削除
        }
        else
        {
            instance = this; // インスタンスが設定されていない場合、現在のオブジェクトをインスタンスとして設定
            DontDestroyOnLoad(gameObject); // シーンをまたいでこのオブジェクトを保持する
        }
    }
}
