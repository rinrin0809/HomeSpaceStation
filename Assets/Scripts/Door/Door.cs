using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DirectionType
    {
        Right,  // 右に移動
        Left,   // 左に移動
        Up,     // 上に移動
        Down    // 下に移動
    }

    [System.Serializable]
    public class DoorObject
    {
        public GameObject targetObject;  // 対象オブジェクト
        public DirectionType direction;  // 移動方向
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
            #region Mover
            if (mover != null)
            {
                switch (doorObject.direction)
                {
                    case DirectionType.Right:
                        mover.SetMoveDirection(new Vector3(1f, 0f, 0f)); // 右に移動
                        break;
                    case DirectionType.Left:
                        mover.SetMoveDirection(new Vector3(-1f, 0f, 0f)); // 左に移動
                        break;
                    case DirectionType.Up:
                        mover.SetMoveDirection(new Vector3(0f, 1f, 0f)); // 上に移動
                        break;
                    case DirectionType.Down:
                        mover.SetMoveDirection(new Vector3(0f, -1f, 0f)); // 下に移動
                        break;
                }
            }
            else
            {
                Debug.LogWarning("ターゲットオブジェクトにMoverスクリプトがアタッチされていません: " + doorObject.targetObject.name);
            }
            #endregion
        }
    }

    // Updateは毎フレーム呼び出される
    void Update()
    {
        foreach (var doorObject in doorObjects)
        {
            if (doorObject.targetObject == null)
                continue;
            Mover mover = doorObject.targetObject.GetComponent<Mover>();
            // Mover スクリプトを取得して移動を実行
            if (mover != null)
            {
                mover.Move();
            }
        }
    }
}