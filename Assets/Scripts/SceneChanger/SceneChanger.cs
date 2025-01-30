using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool OnFlg = false;

    [SerializeField]
    string NextSceneName;

    // フェードアウト
    public FadeOutSceneLoader fadeOutSceneLoader;

    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオブジェクトの有効化
        On();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnFlg = true;
            Debug.Log("true");
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        OnFlg = false;
    //    }
    //}

    //ゲームオブジェクトの有効化
    private void On()
    {
        if (Player.Instance.SkilFlg == true || OnFlg == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Player.Instance.ClearExitFlg = true;
                fadeOutSceneLoader.NoResetCallCoroutine(NextSceneName);
            }
        }
    }
}
