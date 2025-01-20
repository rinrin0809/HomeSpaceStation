using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] float FadeDuration = 2.0f; // �t�F�[�h�C���̏��v����
    [SerializeField] bool FadeInFlg = true;
    void Start()
    {
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0.0f); // �����̓����x��ݒ�
    }

    private void Update()
    {
        if(FadeInFlg)
        {
            StartCoroutine(FadeIn());
        }
        
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f; // �o�ߎ��Ԃ�������
        Color startColor = Text.color; // �����̐F
        Color targetColor = new Color(Text.color.r, Text.color.g, Text.color.b, 1.0f); // �ڕW�̐F

        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime; // �t���[�����Ƃ̌o�ߎ��Ԃ����Z
            Text.color = Color.Lerp(startColor, targetColor, elapsedTime / FadeDuration); // ���X�ɓ����x��ω�
            yield return null; // ���̃t���[���܂őҋ@
        }

        Text.color = targetColor; // �ŏI�I�ɖڕW�̐F�ɐݒ�
        FadeInFlg = false;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f; // �o�ߎ��Ԃ�������
        Color startColor = Text.color; // �����̐F
        Color targetColor = new Color(Text.color.r, Text.color.g, Text.color.b, 0.0f); // �ڕW�̐F

        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime; // �t���[�����Ƃ̌o�ߎ��Ԃ����Z
            Text.color = Color.Lerp(startColor, targetColor, elapsedTime / FadeDuration); // ���X�ɓ����x��ω�
            yield return null; // ���̃t���[���܂őҋ@
        }

        Text.color = targetColor; // �ŏI�I�ɖڕW�̐F�ɐݒ�
        FadeInFlg = true;
    }
}
