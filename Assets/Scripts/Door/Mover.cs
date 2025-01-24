using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 1f;  // 移動速度
    public float leftLimit = -10000f; // 左の移動制限
    public float rightLimit = 10000f; // 右の移動制限
    public float topLimit = 10000f;   // 上の移動制限
    public float bottomLimit = -10000f; // 下の移動制限
    public float timeToChangeDirection = 1.0f; // 方向を変えるまでの時間
    private Vector3 moveDirection; // 移動方向

    public bool isAtLimit = false; // リミットに到達したかどうかのフラグ
    private float remainingTime; // 方向反転までの残り時間

    public bool RockFlg = false; //カギがかかっている時のフラグ
    public bool isMoving = false; // スペースキーが押されたときだけ動かすフラグ

    // Startは初期化処理
    void Start()
    {
        isAtLimit = true;

        // PlayerPrefsで前回の状態を復元
        if (PlayerPrefs.HasKey("RemainingTime"))
        {
            remainingTime = PlayerPrefs.GetFloat("RemainingTime");
            moveDirection = new Vector3(PlayerPrefs.GetFloat("MoveDirectionX"),
                                         PlayerPrefs.GetFloat("MoveDirectionY"),
                                         0); // 保存されていた方向を復元
        }
        else
        {
            remainingTime = timeToChangeDirection; // 初期値
            moveDirection = Vector3.right; // 初期移動方向
        }
    }

    // 移動方向を設定
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    // 移動処理
    public void Move()
    {
        // 現在の位置を取得
        Vector3 currentPos = transform.position;

        // リミットに近づいた場合の処理
        if (currentPos.x <= leftLimit || currentPos.x >= rightLimit || currentPos.y <= bottomLimit || currentPos.y >= topLimit)
        {
            // リミットに到達した場合、位置をリミットに合わせる
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // 位置をリミットに合わせる
            transform.position = new Vector3(newX, newY, currentPos.z);

            // リミットに到達したフラグをセット
            isAtLimit = true;
        }

        // 方向を反転させるタイミング
        if (isAtLimit)
        {
            // 方向を反転
            moveDirection = -moveDirection;

            isAtLimit = false;
        }

        // リミット未到着でスペースキーが押されていたなら
        if (!isAtLimit && isMoving)
        {
            // オブジェクトを移動
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
