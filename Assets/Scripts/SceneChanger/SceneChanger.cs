using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool OnFlg = false;

    [SerializeField]
    string NextSceneName;

    // �t�F�[�h�A�E�g
    private FadeOutSceneLoader fadeOutSceneLoader;

    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�u�W�F�N�g�̗L����
        On();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnFlg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnFlg = false;
        }
    }

    //�Q�[���I�u�W�F�N�g�̗L����
    private void On()
    {
        if (OnFlg && Input.GetKeyDown(KeyCode.Space))
        {
            Player.Instance.UpdateFlg = true;
            fadeOutSceneLoader.NewGameCallCoroutine(NextSceneName);
        }
    }
}
