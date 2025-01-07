using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExportNumber : MonoBehaviour
{

    public int ExpNum;

    public string Exword;

    public Sprite sprite;

    public Image image;

    public void SetSprite(GameObject targetObject,Sprite sprite)
    {
        if (targetObject != null)
        {
            Image image = targetObject.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = sprite;
            }
        }
    }
    
    public void SetRemoved(Image SpImage)
    {
        Debug.Log("1");
         if( SpImage != null)
        {
            Debug.Log("a");
            image = null;
        }
    }

}
