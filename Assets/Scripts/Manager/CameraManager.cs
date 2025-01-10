using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 追従する対象を決める変数
    public GameObject target;

    //お面
    public GameObject Enemy;

    // カメラの初期位置を記憶するための変数
    Vector3 pos;

    // カメラ移動速度
    public float cameraSpeed = 0.5f;

    //イベント
    public EventData Event;

    //イベント前のカメラ位置
    Vector3 originalCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //カメラの初期位置を変数posに入れる
        pos = Camera.main.gameObject.transform.position;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Event != null)
        {
            //お面の初登場時のカメラ演出
            if (Event.GetNameEventActionFlg("お面の初登場"))
            {
                LockOnEnemyCamera(true);
            }

            //お面の初登場時のカメラ演出（ターゲットをプレイヤーに戻す時）
            else if (!Event.GetNameEventActionFlg("お面の初登場") && Event.GetNameEventFlg("お面の初登場"))
            {
                LockOnEnemyCamera(false);
            }

            else if (!Event.GetNameEventActionFlg("お面の初登場") && !Event.GetNameEventFlg("お面の初登場"))
            {
                //プレイヤーにカメラを追従する
                LockOnPlayerCamera();
            }
        }
    }

    //プレイヤーにカメラを追従する
    public void LockOnPlayerCamera()
    {
        // cameraPosという変数を作り、追従する対象の位置を入れる
        Vector3 cameraPos = target.transform.position;

        // もし対象の縦位置が0より大きい場合
        if (target.transform.position.y > 0)
        {
            // カメラの縦位置に対象の位置を入れる
            cameraPos.y = target.transform.position.y;
        }

        // カメラの奥行きの位置に-10を入れる
        cameraPos.z = -10;
        //追従している時に位置を保存する
        originalCameraPosition = cameraPos;

        //　カメラの位置に変数cameraPosの位置を入れる
        Camera.main.gameObject.transform.position = cameraPos;
    }

    //お面の初登場時のカメラ演出
    private void LockOnEnemyCamera(bool EventActionFlg)
    {
        // 現在のカメラ位置
        Vector3 currentCameraPos = Camera.main.gameObject.transform.position;
        // 目標位置（お面の少し前方）
        Vector3 targetCameraPos = new Vector3(0f, 0f, 0f);
        // カメラを徐々にお面に移動させる
        Vector3 smoothedPosition = new Vector3(0f, 0f, 0f);

        //イベントで何かしら（今回の場合はカメラ）の動作をさせる時
        if (EventActionFlg)
        {
            // 目標位置（お面の少し前方）
            targetCameraPos = Enemy.transform.position;
            targetCameraPos.z = -10; // カメラの奥行きを設定
            // カメラを徐々にお面に移動させる
            smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);

            Camera.main.gameObject.transform.position = smoothedPosition;
        }

        else
        {
            // 目標位置（現在のカメラ位置に戻す）
            targetCameraPos = originalCameraPosition; // 元のカメラ位置を保持した変数
            // カメラを徐々に元の位置に戻す
            smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);
            // カメラ位置を更新
            Camera.main.gameObject.transform.position = smoothedPosition;
        }
    }

    private Vector3 LockOnCamera(GameObject Start, GameObject Target)
    {
        // 現在のカメラ位置
        Vector3 currentCameraPos = Start.transform.position;
        // 目標位置（お面の少し前方）
        Vector3 targetCameraPos = Target.transform.position;
        // カメラの奥行きを設定
        targetCameraPos.z = -10;
        // カメラを徐々にお面に移動させる
        Vector3 smoothedPosition = Vector3.Lerp(currentCameraPos, targetCameraPos, Time.deltaTime * cameraSpeed);

        return smoothedPosition;
    }
}