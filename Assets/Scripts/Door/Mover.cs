using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 3f;  // 移動速度
    public float leftLimit = -5f; // 左の移動制限
    public float rightLimit = 5f; // 右の移動制限
    public float time = 3.0f;     // 方向を変えるまでの時間
    private Vector3 moveDirection; // 移動方向

    private bool isAtLimit = false; // リミットに到達したかどうかのフラグ

    // 移動方向を設定
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    // 移動処理
    public void Move()
    {
        // 現在の位置を取得
        float currentX = transform.position.x;

        // 移動の限界をチェックして、オブジェクトがリミットを超えないように制御
        if (currentX <= leftLimit || currentX >= rightLimit)
        {
            // リミットに到達した場合、位置をリミットに合わせる
            transform.position = new Vector3(Mathf.Clamp(currentX, leftLimit, rightLimit), transform.position.y, transform.position.z);

            // リミットに到達したフラグをセット
            isAtLimit = true;
        }
        else
        {
            // リミットを超えない場合は、フラグをリセット
            isAtLimit = false;
        }

        // リミットに到達した場合、方向を反転させる
        if (isAtLimit && time <= 0.0f)
        {
            moveDirection = -moveDirection; // 方向を反転
            time = 3.0f;  // timeをリセットして、再度反転まで待機
        }

        // timeが減少する処理
        if (isAtLimit)
        {
            time -= Time.deltaTime; // リミットに到達している間、timeを減少
        }

        // オブジェクトを移動
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
