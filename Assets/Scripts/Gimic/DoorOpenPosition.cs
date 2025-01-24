using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPosition : MonoBehaviour
{
    public GameObject OpenDoor;
    private bool isPlayerInRangeOpenDoor = false;
    public Mover move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーがコライダに入ったとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = true;
            Debug.Log("プレイヤーがコライダに入った！");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // プレイヤーがコライダから出たとき
        if (other.CompareTag("Player"))
        {
            isPlayerInRangeOpenDoor = false;
            Debug.Log("プレイヤーがコライダから出た！");
        }
    }

    // Update is called once per frame
    #region 連打できる
    /*void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");
            if(move != null)
            {
                move.isMoving = true;
            }
        }
    }*/
    #endregion

    #region 時間制限で制御
    public bool hasPressedSpace = false; // Spaceキーが押されたかどうかのフラグ
    private float timeBetweenInputs = 5.0f; // 入力を受け付けるまでの待機時間（秒）
    public float time = 0.0f;
    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space) && !hasPressedSpace)
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");

            move.isMoving = true;

            time = 2.0f;

            // Spaceキーが押された後、フラグを立てる
            hasPressedSpace = true;

            // 一定時間後にフラグをリセットするコルーチンを開始
            StartCoroutine(WaitForInputCooldown());
        }

        if (hasPressedSpace && time < 0)
        {
            move.isMoving = false;
        }
        time -= Time.deltaTime;
    }

    // 一定時間待機してから再度入力を受け付ける
    private IEnumerator WaitForInputCooldown()
    {
        yield return new WaitForSeconds(timeBetweenInputs); // 指定した秒数待機

        // 時間が経過したらフラグをリセットして再度入力を受け付ける
        hasPressedSpace = false;
    }
    #endregion

    #region フレーム管理
    /*private bool hasPressedSpace = false; // Spaceキーが押されたかどうかのフラグ

    void Update()
    {
        if (isPlayerInRangeOpenDoor && Input.GetKeyDown(KeyCode.Space) && !hasPressedSpace)
        {
            Debug.Log("スペースキーが押された！ギミックをアクティブにします。");

            if (move != null)
            {
                move.isMoving = true;
            }

            // Spaceキーが押された後、フラグを立てる
            hasPressedSpace = true;

            // フラグをリセットするために1フレーム後に再びフラグを戻す
            StartCoroutine(ResetSpacePressFlag());
        }
    }

    // 1フレーム後にフラグをリセットするコルーチン
    private IEnumerator ResetSpacePressFlag()
    {
        yield return null; // 1フレーム待機
        hasPressedSpace = false; // 1フレーム後にフラグを戻して連打を防ぐ
    }*/
    #endregion
}
