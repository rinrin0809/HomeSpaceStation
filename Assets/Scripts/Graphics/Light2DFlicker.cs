using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DFlicker : MonoBehaviour
{
    public Light2D targetLight;
    public float flickerSpeed = 2f;  // �_�ő��x
    public Color minColor = new Color(1f, 1f, 1f, 0.6f);  // �ŏ��̖��邳���������邭
    public Color maxColor = new Color(1f, 1f, 1f, 2f);  // �ő�̖��邳�𑝉�
    public float flickerIntensityMin = 0.8f;  // �ŏ����x
    public float flickerIntensityMax = 1.5f;  // �ő勭�x

    private float randomOffset;  // �����I�t�Z�b�g

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);  // �����I�t�Z�b�g�������_����
    }

    void Update()
    {
        if (targetLight != null)
        {
            // �����_���ȃm�C�Y���������_��
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed + randomOffset, 0f);
            targetLight.color = Color.Lerp(minColor, maxColor, noise);

            // ���x�������_���ɕω�������
            targetLight.intensity = Mathf.Lerp(flickerIntensityMin, flickerIntensityMax, noise);
        }
    }
}
