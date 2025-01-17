using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    public GameObject door;  // 扉のゲームオブジェクト
    public Vector3 openPosition;  // 扉が開いたときの位置
    public float openSpeed = 2f;  // 扉が開く速度
    public bool isOpening = false;

    public SelectGimmick SG;  // SelectGimmick クラスへの参照

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
        if (SG != null && SG.Ans)
        {
            OpenDoor();
        }

        // 扉を開ける処理
        if (isOpening == true)
        {
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * 0.033f);
        }
    }

    // 扉を開けるメソッド
    public void OpenDoor()
    {
        isOpening = true;
    }
}
