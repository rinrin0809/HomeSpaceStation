using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D ���g������

public class Light2D : MonoBehaviour
{
    public Light2D light2D; // Light2D�̎Q��
    public float minIntensity = 0.5f; // �ŏ��̖��邳
    public float maxIntensity = 1.5f; // �ő�̖��邳
    public float flickerSpeed = 0.1f; // �_�ł̑���

    public float intensity { get; private set; }

    private void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>(); // �A�^�b�`����Ă��� Light2D ���擾

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
