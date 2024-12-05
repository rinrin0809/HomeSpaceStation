/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // 右に移動
        Left   // 左に移動
    }

    public DirectionType direction = DirectionType.Right; // 各オブジェクトごとの方向設定
    public float moveSpeed = 3f; // 移動速度
    public float leftLimit = -5f; // 左の移動制限
    public float rightLimit = 5f; // 右の移動制限
    public float time = 3.0f; // 方向を変えるまでの時間
    public List<GameObject> targetObjects; // 移動させたいオブジェクトのリスト

    private Vector3 moveDirection; // 移動方向

    // Start is called before the first frame update
    void Start()
    {
        // targetObjectsに設定されたオブジェクトごとに方向を決定
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject == null) continue;

            // DirectionTypeに基づいて移動方向を設定
            Door targetDoor = targetObject.GetComponent<Door>(); // Door スクリプトを取得
            if (targetDoor != null)
            {
                // 各オブジェクトごとに方向を設定
                if (targetDoor.direction == DirectionType.Right)
                {
                    targetDoor.moveDirection = new Vector3(1f, 0f, 0f); // 右に移動
                }
                else
                {
                    targetDoor.moveDirection = new Vector3(-1f, 0f, 0f); // 左に移動
                }
            }
            else
            {
                Debug.LogWarning("ターゲットオブジェクトにDoorスクリプトがアタッチされていません: " + targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject targetObject in targetObjects)
        {
            if (targetObject == null) continue;

            Door targetDoor = targetObject.GetComponent<Door>(); // Door スクリプトを取得
            if (targetDoor == null) continue;

            // 現在の位置を取得
            float currentX = targetObject.transform.position.x;

            // 移動の限界をチェックして、オブジェクトがリミットを超えないように制御
            if (currentX <= leftLimit || currentX >= rightLimit)
            {
                // リミットに到達した場合、位置をリミットに合わせる
                targetObject.transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), targetObject.transform.position.y, targetObject.transform.position.z);

                // timeが0より小さくならないように処理
                if (time <= 0.0f)
                {
                    // 方向を反転
                    targetDoor.moveDirection = -targetDoor.moveDirection;
                    time = 3.0f;  // timeをリセットして、再度反転まで待機
                }

                time -= Time.deltaTime; // timeを減少させる
            }

            // オブジェクトを移動
            targetObject.transform.Translate(targetDoor.moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // 右に移動
        Left   // 左に移動
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject; // 対象オブジェクト
        public DirectionType direction; // 移動方向
    }

    public List<DoorObject> doorObjects; // 移動させたいオブジェクトとその方向を設定するリスト

    public float moveSpeed = 3f; // 移動速度
    public float leftLimit = -5f; // 左の移動制限
    public float rightLimit = 5f; // 右の移動制限
    public float time = 3.0f; // 方向を変えるまでの時間

    private void Start()
    {
        // doorObjects の設定に基づいて各オブジェクトの移動方向を決定
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // DirectionType に基づいて移動方向を設定
            Door targetDoor = doorObject.targetObject.GetComponent<Door>(); // Door スクリプトを取得
            if (targetDoor != null) // Door スクリプトがアタッチされているかチェック
            {
                if (doorObject.direction == DirectionType.Right)
                {
                    targetDoor.SetMoveDirection(new Vector3(1f, 0f, 0f)); // 右に移動
                }
                else
                {
                    targetDoor.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // 左に移動
                }
            }
            else
            {
                Debug.LogWarning("ターゲットオブジェクトにDoorスクリプトがアタッチされていません: " + doorObject.targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // 方向に応じた移動処理
            Door targetDoor = doorObject.targetObject.GetComponent<Door>();
            if (targetDoor != null)
            {
                targetDoor.Move();
            }
        }
    }

    // 移動方向を設定するメソッド
    public void SetMoveDirection(Vector3 direction)
    {
        this.moveDirection = direction;
    }

    // オブジェクトを移動するメソッド
    private Vector3 moveDirection; // 移動方向

    public void Move()
    {
        // 現在の位置を取得
        float currentX = transform.position.x;

        // 移動の限界をチェックして、オブジェクトがリミットを超えないように制御
        if (currentX <= leftLimit || currentX >= rightLimit)
        {
            // リミットに到達した場合、位置をリミットに合わせる
            transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), transform.position.y, transform.position.z);

            // timeが0より小さくならないように処理
            if (time <= 0.0f)
            {
                // 方向を反転
                moveDirection = -moveDirection;
                time = 3.0f;  // timeをリセットして、再度反転まで待機
            }

            time -= Time.deltaTime; // timeを減少させる
        }

        // オブジェクトを移動
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right, // 右に移動
        Left   // 左に移動
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject; // 対象オブジェクト
        public DirectionType direction; // 移動方向
    }

    public List<DoorObject> doorObjects; // 移動させたいオブジェクトとその方向を設定するリスト

    private void Start()
    {
        // doorObjects の設定に基づいて各オブジェクトの移動方向を決定
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // Mover スクリプトを取得して移動方向を設定
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            if (mover != null)
            {
                if (doorObject.direction == DirectionType.Right)
                {
                    mover.SetMoveDirection(new Vector3(1f, 0f, 0f)); // 右に移動
                }
                else
                {
                    mover.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // 左に移動
                }
            }
            else
            {
                Debug.LogWarning("ターゲットオブジェクトにMoverスクリプトがアタッチされていません: " + doorObject.targetObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;

            // Mover スクリプトを取得して移動を実行
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            if (mover != null)
            {
                mover.Move();
            }
        }
    }
}
