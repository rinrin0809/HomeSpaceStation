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

    [SerializeField]
    GameObject FastObj;

    [SerializeField]
    GameObject SkipObj;

    [SerializeField]
    int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        fadeOutSceneLoader = FindObjectOfType<FadeOutSceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Count)
        {
            case 0:
                SkipObj.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Count++;
                }
                break;

            case 1:
                FastObj.SetActive(false);
                SkipObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Count++;
                }
                break;

            case 2:
                
                break;
        }

        if (rectTransform.anchoredPosition.y >= 1000 || Count >= 2)
        {
            if (fadeOutSceneLoader != null) fadeOutSceneLoader.NewGameCallCoroutine("Floor(B1)");
        }
    }
}
