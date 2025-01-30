using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMover : MonoBehaviour
{
    public GameObject door;  // 扉のゲームオブジェクト
    public Vector3 openPosition;  // 扉が開いたときの位置
    public float openSpeed = 2f;  // 扉が開く速度
    public bool isOpening = false;
    public bool RockFlg = false;
    public InputNumber inputnumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("開始時 " + door.transform.localPosition);
        if (door == null)
        {
            Debug.LogError("Door not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!RockFlg)
        {
            // 扉を開ける処理
            if (isOpening == true)
            {
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
            }
            if (isOpening == true && !inputnumber)
            {
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
            }
        }
       
    }
}
