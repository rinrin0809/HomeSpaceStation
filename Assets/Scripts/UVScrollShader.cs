using UnityEngine;

public class UVScrollShader : MonoBehaviour
{
    // マテリアル参照
    [SerializeField] private Material material;

    [SerializeField] float SpeedX = 1.0f;
    [SerializeField] float SpeedY = 1.0f;

    [SerializeField] bool Upflg = false;

    void Start()
    {
        // Rendererからマテリアルを取得
        //Renderer renderer = GetComponent<Renderer>();
        //material = renderer.material;

        // 初期値を設定
        material.SetFloat("_XSpeed", SpeedX); // X方向のスクロール速度
        material.SetFloat("_YSpeed", SpeedY); // Y方向のスクロール速度
    }

    void Update()
    {
        // 速度を動的に変更 (例: キー入力で調整)
        if (Upflg)
        {
            material.SetFloat("_YSpeed", SpeedY); // Y方向の速度を増加
        }

        else
        {
            material.SetFloat("_XSpeed", SpeedX); // Y方向の速度を増加
        }
    }
}
