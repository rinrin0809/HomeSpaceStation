using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DFlicker : MonoBehaviour
{
    public Light2D targetLight;
    public float flickerSpeed = 2f;  // 点滅速度
    public Color minColor = new Color(1f, 1f, 1f, 0.6f);  // 最小の明るさを少し明るく
    public Color maxColor = new Color(1f, 1f, 1f, 2f);  // 最大の明るさを増加
    public float flickerIntensityMin = 0.8f;  // 最小強度
    public float flickerIntensityMax = 1.5f;  // 最大強度

    private float randomOffset;  // 乱数オフセット

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);  // 初期オフセットをランダム化
    }

    void Update()
    {
        if (targetLight != null)
        {
            // ランダムなノイズを加えた点滅
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed + randomOffset, 0f);
            targetLight.color = Color.Lerp(minColor, maxColor, noise);

            // 強度もランダムに変化させる
            targetLight.intensity = Mathf.Lerp(flickerIntensityMin, flickerIntensityMax, noise);
        }
    }
}
