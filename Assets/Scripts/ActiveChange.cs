using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChange : MonoBehaviour
{
    //アクティブ化を切り替えるオブジェクト
    [SerializeField] private GameObject ActiveObject;
    //↑がONの時にOFFにしたいオブジェクト
    [SerializeField] private GameObject InverseActiveObject;

    //アクティブ化の変更をするかのフラグ
    [SerializeField] private bool ChangeFlg = true;

    // Update is called once per frame
    void Update()
    {
        //フラグがtrueの時
        if(ChangeFlg) Change();
    }

    public void Change()
    {
        if(ActiveObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ActiveObject.SetActive(false);
                if(InverseActiveObject != null) InverseActiveObject.SetActive(true);
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ActiveObject.SetActive(true);
                if (InverseActiveObject != null) InverseActiveObject.SetActive(false);
            }
        }
    }

    public void ButtonChange()
    {
        if (ActiveObject.activeSelf)
        {
            ActiveObject.SetActive(false);
            if(Player.Instance)Player.Instance.UpdateFlg = true;
            if (InverseActiveObject != null) InverseActiveObject.SetActive(true);
        }

        else
        {
            ActiveObject.SetActive(true);
        }
    }
}
