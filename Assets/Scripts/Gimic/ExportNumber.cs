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

    public Transform panelPos;

    public float angleZ = 0;

    Vector3 ChangepanemPos;

    public float CorrectAnswer;

    public float a = 90.00001f;

    public void SetSprite(GameObject targetObject,Sprite sprite)
    {
        if (targetObject != null)
        {
            Image image = targetObject.GetComponent<Image>();
            if (image != null)
            {
                //Debug.Log("namesp:" + sprite.name);
                image.sprite = sprite;
            }
        }
    }
    public void ChangePos(float targetPos)
    {
        Debug.Log("åƒÇ—èoÇ≥ÇÍÇΩ");
        angleZ = panelPos.eulerAngles.z + targetPos;
        Quaternion newRotation = Quaternion.Euler(0, 0, angleZ);
        transform.rotation = newRotation;
        //
        Debug.Log("1:" + angleZ);

    }

  
}
