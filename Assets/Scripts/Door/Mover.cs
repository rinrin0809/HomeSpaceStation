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
    private bool isNearLimit = false; // リミットに近づいているかどうかのフラグ

    public bool isMoving = false; // スペースキーが押されたときだけ動かすフラグ

    private float customTime = 0f; // 自分で管理する時間

    // Startは初期化処理
    void Start()
    {
        // シーン遷移後もオブジェクトを保持
        DontDestroyOnLoad(gameObject);

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
            if (!isNearLimit)
            {
                isNearLimit = true; // リミットに近づいたときにフラグを立てる
                customTime = 0f; // カスタムタイマーをリセット
            }

            // リミットに到達した場合、位置をスムーズにリミットに合わせる
            float newX = Mathf.Clamp(currentPos.x, leftLimit, rightLimit);
            float newY = Mathf.Clamp(currentPos.y, bottomLimit, topLimit);

            // 位置をリミットに向かって徐々に変更
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(newX, newY, currentPos.z), moveSpeed * 0.033f);

            // リミットに到達したフラグをセット
            isAtLimit = true;
        }
        else
        {
            isNearLimit = false; // リミットを離れたらフラグをリセット
        }

        // リミットに近づいた後、カスタムタイマーを増加させる
        if (isAtLimit)
        {
            customTime += Time.deltaTime; // 手動で時間を増加させる
        }

        // 方向を反転させるタイミング
        if (customTime >= timeToChangeDirection && isAtLimit)
        {
            // 方向を反転
            moveDirection = -moveDirection;
            // タイマーをリセット
            customTime = 0f;

            isAtLimit = false;
        }

        // リミット未到着でスペースキーが押されていたなら
        if (!isAtLimit && isMoving)
        {
            // オブジェクトを移動
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    // Updateメソッドでスペースボタン入力を処理
    void Update()
    {
        // スペースボタンが離された場合
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false; // 移動を停止
        }

        // 移動処理を呼び出す
        Move();
    }

    // シーン遷移前に状態を保存
    void OnApplicationQuit()
    {
        // 現在の状態を保存
        PlayerPrefs.SetFloat("RemainingTime", remainingTime);
        PlayerPrefs.SetFloat("MoveDirectionX", moveDirection.x);
        PlayerPrefs.SetFloat("MoveDirectionY", moveDirection.y);
        PlayerPrefs.Save();
    }
}
