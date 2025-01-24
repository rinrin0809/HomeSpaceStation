using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D を使うため

public class Light2D : MonoBehaviour
{
    public Light2D light2D; // Light2Dの参照
    public float minIntensity = 0.5f; // 最小の明るさ
    public float maxIntensity = 1.5f; // 最大の明るさ
    public float flickerSpeed = 0.1f; // 点滅の速さ

    public float intensity { get; private set; }

    private void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>(); // アタッチされている Light2D を取得

        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
