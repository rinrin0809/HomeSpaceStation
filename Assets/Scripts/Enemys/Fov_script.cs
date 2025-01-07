using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_script : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8f;

    // ターゲットのTransformは自動で取得
    private Transform target;

    [SerializeField]
    private GameOverFade gameover;

    private string targetname = "Player";
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
        if (target == null) return;

        // ターゲットへの方向ベクトルを計算
        Vector2 dir = target.position - fovPoint.position;
        float angle = Vector2.Angle(fovPoint.up, dir);

        // レイキャストを実行
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir.normalized, range);

        // 角度が視界範囲内かどうか
        if (angle < fovAngle / 2)
        {
            if (r.collider != null && r.collider.CompareTag(targetname))
            {
                // プレイヤーを発見！
                gameover.gameObject.SetActive(true);
                //Debug.Log("敵の視界に入りました");
                Debug.DrawRay(fovPoint.position, dir.normalized * range, Color.red);
            }
            else
            {
                //Debug.Log("敵の視界から外れました");
            }
        }
        else
        {
            //Debug.Log("敵の視界外です");
        }
    }


}
