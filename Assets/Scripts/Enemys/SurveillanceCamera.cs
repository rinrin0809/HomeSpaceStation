using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    //ターゲット設定
    private Transform target;
    private string targetname = "Player";

    //視野
    private int angle = 180;
    private int anglecorrection = 360;
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8f;
   
    //回転
    public float rotationSpeed = 30f; // 回転速度（度/秒）
    public float minRotation = -45f;  // 回転の最小角度
    public float maxRotation = 45f;   // 回転の最大角度
    private bool rotatingClockwise = true; // 時計回りの回転中かどうか

    //ギズモ用
    private int half = 2;            
    void Start()
    {
        // プレイヤーを自動で探す
        GameObject player = GameObject.FindGameObjectWithTag(targetname);
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        // オブジェクトを回転させる
        RotateObject();

        if (target == null) return;

        // ターゲットへの方向ベクトルを計算
        Vector2 dir = target.position - fovPoint.position;
        float angle = Vector2.Angle(fovPoint.up, dir);

        // レイキャストを実行
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir.normalized, range);

        // 角度が視界範囲内かどうか
        if (angle < fovAngle / 2)
        {
            if (r.collider != null && r.collider.CompareTag("Player"))
            {
                // プレイヤーを発見！
               // Debug.Log("敵の視界に入りました");
                //Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);
            }
            else
            {
                //Debug.Log("敵の視界から外れました");
            }
        }
        else
        {
           // Debug.Log("敵の視界外です");
        }
    }

    private void RotateObject()
    {
        // 現在のローカルZ軸の回転角度を取得
        float currentRotation = transform.localEulerAngles.z;

       
        // 角度を -180 ～ 180 に補正
        if (currentRotation > angle)
        {
               currentRotation -= anglecorrection;
        }

        // 回転方向の切り替え
        if (rotatingClockwise && currentRotation >= maxRotation)
        {
            rotatingClockwise = false;
        }
        else if (!rotatingClockwise && currentRotation <= minRotation)
        {
            rotatingClockwise = true;
        }

        // 回転
        float rotationDelta = rotationSpeed * Time.deltaTime * (rotatingClockwise ? 1 : -1);
        transform.Rotate(0, 0, rotationDelta);
    }

    void OnDrawGizmos()
    {
        if (fovPoint == null) return;

        // ギズモの色設定
        Gizmos.color = Color.green;

        // 視界の中心線
        Gizmos.DrawRay(fovPoint.position, fovPoint.up * range);

        // 扇形を描画
        DrawFOVGizmo();
    }

    private void DrawFOVGizmo()
    {
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -fovAngle / half) * fovPoint.up * range;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, fovAngle / half) * fovPoint.up * range;

        // 視野の範囲を扇形で描画
        float transparency = 0.3f;//透明度
        Gizmos.color = new Color(0, 1, 0, transparency); // 半透明の緑
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + leftBoundary);
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + rightBoundary);

        // 扇形の補助線を細かく描画
        int segments = 20; // セグメント数（多いほど滑らか）
        float angleStep = fovAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -fovAngle / half + angleStep * i;
            Vector3 segmentDir = Quaternion.Euler(0, 0, angle) * fovPoint.up * range;
            Gizmos.DrawLine(fovPoint.position, fovPoint.position + segmentDir);
        }
    }
}
