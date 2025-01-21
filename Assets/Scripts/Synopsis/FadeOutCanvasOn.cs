using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCanvasOn : MonoBehaviour
{
    [SerializeField] 
    GameObject Obj;
    [SerializeField]
    RectTransform rectTransform;

    FadeOutSceneLoader fadeOutSceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition.y >= 1000)
        {
            if (fadeOutSceneLoader != null) fadeOutSceneLoader.NewGameCallCoroutine("Floor(B1)");
        }
    }
}
