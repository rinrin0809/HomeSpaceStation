using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChange : MonoBehaviour
{
    //�A�N�e�B�u����؂�ւ���I�u�W�F�N�g
    [SerializeField] private GameObject ActiveObject;
    //����ON�̎���OFF�ɂ������I�u�W�F�N�g
    [SerializeField] private GameObject InverseActiveObject;

    //�A�N�e�B�u���̕ύX�����邩�̃t���O
    [SerializeField] private bool ChangeFlg = true;

    // Update is called once per frame
    void Update()
    {
        //�t���O��true�̎�
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
