using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOutOnOrOff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SceneCanvas;
    public GameObject LightOutCanvas;
    public Button Obj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LightOutCanvasOnOrOff()
    {
        if(LightOutCanvas.activeSelf)
        {
            SceneCanvas.SetActive(true);
            LightOutCanvas.SetActive(false);
        }

        else
        {
            SceneCanvas.SetActive(false);
            LightOutCanvas.SetActive(true);
        }
    }
}
