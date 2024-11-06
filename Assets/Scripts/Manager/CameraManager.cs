using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 追従する対象を決める変数
    public GameObject target;
    // カメラの初期位置を記憶するための変数
    Vector3 pos;             

    // Start is called before the first frame update
    void Start()
    {
        //カメラの初期位置を変数posに入れる
        pos = Camera.main.gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        // cameraPosという変数を作り、追従する対象の位置を入れる
        Vector3 cameraPos = target.transform.position; 

        // もし対象の縦位置が0より大きい場合
        if (target.transform.position.y > 0)
        {
            // カメラの縦位置に対象の位置を入れる
            cameraPos.y = target.transform.position.y;   
        }

        // カメラの奥行きの位置に-10を入れる
        cameraPos.z = -10; 
        //　カメラの位置に変数cameraPosの位置を入れる
        Camera.main.gameObject.transform.position = cameraPos; 

    }
}
