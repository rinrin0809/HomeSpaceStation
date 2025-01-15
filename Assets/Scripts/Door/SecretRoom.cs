using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    public GameObject door;  // 扉のゲームオブジェクト
    public Vector3 openPosition;  // 扉が開いたときの位置
    public float openSpeed = 2f;  // 扉が開く速度
    private bool isOpening = false;

    private SelectGimmick SG;  // Mover クラスへの参照

    // Start is called before the first frame update
    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door not assigned in the inspector!");
        }

        // Mover コンポーネントを取得
        SG = FindObjectOfType<SelectGimmick>();  // シーン内の Mover を取得
        if (SG == null)
        {
            Debug.LogError("Mover not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mover の isMoving フラグが true なら扉を開ける
        if (SG != null && SG.Ans)
        {
            OpenDoor();
        }

        // 扉を開ける処理
        if (isOpening)
        {
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, openPosition, openSpeed * Time.deltaTime);
        }
    }

    // 扉を開けるメソッド
    public void OpenDoor()
    {
        isOpening = true;
    }
}
