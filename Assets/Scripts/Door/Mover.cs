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
    public float timeToChangeDirection = 3.0f; // 方向を変えるまでの時間
    private Vector3 moveDirection; // 移動方向

    private bool isAtLimit = false; // リミットに到達したかどうかのフラグ
    private float remainingTime; // 方向反転までの残り時間
    private bool isNearLimit = false; // リミットに近づいているかどうかのフラグ

    public bool isMoving = false; // スペースキーが押されたときだけ動かすフラグ

    // Startは初期化処理
    void Start()
    {
        remainingTime = timeToChangeDirection; // 初期化
        moveDirection = Vector3.right; // 初期移動方向（デフォルト）
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
            if (!isNearLimit)
            {
                isNearLimit = true; // リミットに近づいたときにフラグを立てる
                remainingTime = timeToChangeDirection; // 残り時間をリセット
            }

            // リミットに到達した場合、位置をスムーズにリミットに合わせる
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // 位置をリミットに向かって徐々に変更
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(newX, newY, currentPos.z), moveSpeed * Time.deltaTime);

            // リミットに到達したフラグをセット
            isAtLimit = true;
        }
        else
        {
            isNearLimit = false; // リミットを離れたらフラグをリセット
        }

        // リミットに近づいた後、時間を減少させる
        if (isAtLimit)
        {
            remainingTime -= Time.deltaTime;
        }

        // 方向を反転させるタイミング
        if (remainingTime <= 0.0f && isAtLimit)
        {
            // 方向を反転
            moveDirection = -moveDirection;
            // 時間をリセット
            remainingTime = timeToChangeDirection;

            isAtLimit = false;
        }

        // リミット未到着でスペースキーが押されっていたなら
        if (!isAtLimit && isMoving)
        {
            // オブジェクトを移動
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    // Updateメソッドでスペースボタン入力を処理
    void Update()
    {
        // スペースボタンが押された場合
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true; // 移動を開始
        }

        // スペースボタンが離された場合
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false; // 移動を停止
        }

        // 移動処理を呼び出す
        Move();
    }
}