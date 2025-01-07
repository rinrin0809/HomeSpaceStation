using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    // �t�F�[�h�p��UI�p�l���iImage�j
    public Image fadePanel;
    // �t�F�[�h�̊����ɂ����鎞��
    public float fadeDuration = 1.0f;
    // �t�F�[�h�C���E�A�E�g�̌J��Ԃ���
    public int fadeRepeatCount = 3;
    // �t�F�[�h�A�E�g
    private FadeOutSceneLoader fadeOutSceneLoader;

    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
        // �t�F�[�h�C���E�A�E�g�̃V�[�P���X���J�n
        StartCoroutine(FadeInOutSequence(fadeRepeatCount));
    }

    // �t�F�[�h�C���A�j���[�V��������
    private IEnumerator FadeIn()
    {
        // �p�l����L����
        fadePanel.enabled = true;
        // �o�ߎ��Ԃ�������
        float elapsedTime = 0.0f;
        // �t�F�[�h�p�l���̊J�n�F���擾
        Color startColor = fadePanel.color;
        // �A���t�@�l��0�ɐݒ�
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadePanel.color = endColor;
    }

    // �t�F�[�h�A�E�g�A�j���[�V��������
    private IEnumerator FadeOut()
    {
        //�p�l����L����
        fadePanel.enabled = true;
        //�o�ߎ��Ԃ�������
        float elapsedTime = 0.0f;
        //�t�F�[�h�p�l���̊J�n�F���擾
        Color startColor = fadePanel.color;       
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.8f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadePanel.color = endColor;
    }

    // �t�F�[�h�C���E�A�E�g�����݂ɌJ��Ԃ�
    private IEnumerator FadeInOutSequence(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(FadeOut());
        }

        // �J��Ԃ�������������ɌĂяo��
        if(fadeOutSceneLoader != null) fadeOutSceneLoader.NewGameCallCoroutine("Over");
    }
}
