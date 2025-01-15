using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fovAngle = 90f; // 通常視野角
    public float reverseFovAngle = 90f; // 反対側の視野角
    public Transform fovPoint;
    public float range = 8f; // 検知範囲
    public float lookDelay = 2f; // 反対側を向くまでの遅延時間

    private Transform target; // プレイヤー
    private string targetName = "Player";
    private bool isInReverseFov = false; // 反対側の視野内にいるか
    private Coroutine lookCoroutine = null; // 遅延処理コルーチン

    // 新たに追加: 敵がプレイヤーを発見した場合に動きを停止するフラグ
    public bool isPlayerInSight = false;

    [SerializeField]
    private GameOverFade gameover;

    void Start()
    {
        // プレイヤーを自動で探す
        GameObject player = GameObject.FindGameObjectWithTag(targetName);
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector2 dir = target.position - fovPoint.position;
        float distance = dir.magnitude;
        float angle = Vector2.Angle(-fovPoint.up, dir); // 視野の反対側を基準

        // プレイヤーが反対側の視野内にいるかチェック
        if (distance <= range && angle < reverseFovAngle / 2)
        {
            if (!isInReverseFov)
            {
                isInReverseFov = true;
                if (lookCoroutine != null) StopCoroutine(lookCoroutine);
                lookCoroutine = StartCoroutine(LookAtAfterDelay(dir));
            }
        }
        else
        {
            isInReverseFov = false;
            if (lookCoroutine != null)
            {
                StopCoroutine(lookCoroutine);
                lookCoroutine = null;
            }
        }

        // プレイヤーが視界に入っているかどうかの判定
        if (distance <= range && angle <= fovAngle / 2)
        {
            isPlayerInSight = true;
            if (gameover != null) gameover.gameObject.SetActive(true);

        }
        else
        {
            isPlayerInSight = false;
        }
    }

    private IEnumerator LookAtAfterDelay(Vector2 dir)
    {
        yield return new WaitForSeconds(lookDelay);

        // 振り向く処理
        Vector3 direction = new Vector3(dir.x, dir.y, 0).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        fovPoint.rotation = targetRotation;

        Debug.Log("反対側のプレイヤー方向を向きました");
    }

    void OnDrawGizmos()
    {
        if (fovPoint == null) return;

        // 通常視野の描画
        Gizmos.color = Color.yellow;
        DrawFovGizmo(fovPoint.up, fovAngle);

        // 反対側視野の描画
        Gizmos.color = Color.red;
        DrawFovGizmo(-fovPoint.up, reverseFovAngle);

        // 視線の描画: プレイヤーが視界内にいる場合
        if (target != null && isPlayerInSight)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(fovPoint.position, target.position); // プレイヤーと視点を結ぶ線
        }

        // 背後視野内のプレイヤーを検出した場合、ギズモでレイキャストを描画
        if (target != null && isInReverseFov)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(fovPoint.position, target.position); // 背後視野内のプレイヤーとの接続線
        }
    }

    private void DrawFovGizmo(Vector3 direction, float angle)
    {
        // 視野角の左右の端を計算
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -angle / 2) * direction * range;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, angle / 2) * direction * range;

        // 視野範囲を描画するためのラインを描く
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + leftBoundary);
        Gizmos.DrawLine(fovPoint.position, fovPoint.position + rightBoundary);

        // 視野範囲内を示すため、ギズモを薄く描画（セグメントで描画）
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.2f);
        int segmentCount = 30; // 円弧を構成するセグメント数
        Vector3 prevPoint = fovPoint.position + leftBoundary;

        for (int i = 1; i <= segmentCount; i++)
        {
            // 各セグメント角度を計算
            float segmentAngle = -angle / 2 + (angle / segmentCount) * i;
            Vector3 nextPoint = fovPoint.position + (Quaternion.Euler(0, 0, segmentAngle) * direction) * range;

            // 直線でセグメントを繋げる
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

}
