using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class FadeOutSceneLoader : MonoBehaviour
{
    public Image fadePanel;             // �t�F�[�h�p��UI�p�l���iImage�j
    public float fadeDuration = 1.0f;   // �t�F�[�h�̊����ɂ����鎞��
    [SerializeField] private InventryData Inventory;
    //�C�x���g
    public EventData Event;

    //NewGame�̐ݒ�
    public void NewGameCallCoroutine(string Name)
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.NewGamePushFlg = true;
            LoadManager.Instance.LoadGameFlg = false;
            Inventory.ResetInventory();
            if(Event != null) Event.AllResetFlag();
            StartCoroutine(FadeOutAndNewGameOrTitle(Name));
        }
    }

    public void LoadGameCallCoroutine()
    {
        if (LoadManager.Instance != null)
        {
            LoadManager.Instance.NewGamePushFlg = false;
            LoadManager.Instance.LoadGameFlg = true;
            Debug.Log("NewFlg" + LoadManager.Instance.NewGamePushFlg);
            Debug.Log("LoadFlg" + LoadManager.Instance.LoadGameFlg);
            Inventory.ResetInventory();
            if (Event != null) Event.AllResetFlag();
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    // �t�F�[�h�A�E�g�A�j���[�V��������
    private IEnumerator FadeOut()
    {
        fadePanel.enabled = true;                 // �p�l����L����
        float elapsedTime = 0.0f;                 // �o�ߎ��Ԃ�������
        Color startColor = fadePanel.color;       // �t�F�[�h�p�l���̊J�n�F���擾
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // �t�F�[�h�p�l���̍ŏI�F��ݒ�

        // �t�F�[�h�A�E�g�A�j���[�V���������s
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // �o�ߎ��Ԃ𑝂₷
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // �t�F�[�h�̐i�s�x���v�Z
            fadePanel.color = Color.Lerp(startColor, endColor, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
            yield return null;                                     // 1�t���[���ҋ@
        }

        fadePanel.color = endColor;  // �t�F�[�h������������ŏI�F�ɐݒ�
    }

    // �t�F�[�h�A�E�gNewGame�V�[���܂���Title�V�[��
    public IEnumerator FadeOutAndNewGameOrTitle(string Name)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(Name);
    }

    // �v���C���[�f�[�^�̃��[�h�ƃV�[���J�ڏ���
    public IEnumerator FadeOutAndLoadScene()
    {
        string filePath = "";

        if (LoadManager.Instance.GetSideFlg())
        {
            filePath = LoadManager.Instance.GetFilePathBySideNum(LoadManager.Instance.GetSideNum());
        }

        else
        {
            filePath = LoadManager.Instance.GetFilePathByLengthNum(LoadManager.Instance.GetLengthNum());
        }

        if (File.Exists(filePath))
        {
            yield return StartCoroutine(FadeOut());
            SceneManager.LoadScene(LoadManager.Instance.NextSceneName);
        }
    }
}
