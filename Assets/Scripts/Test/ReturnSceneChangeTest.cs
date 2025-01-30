using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnSceneChangeTest : MonoBehaviour
{
    //フェードアウト
    private FadeOutSceneLoader fadeOutSceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            fadeOutSceneLoader.NewGameCallCoroutine("Floor(B3)");
        }
    }
}
