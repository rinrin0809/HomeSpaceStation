using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // 初期のローカル回転を記録
        initialRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // 親オブジェクトの回転を無視して初期回転を保持
        transform.localRotation = initialRotation;
    }
}
