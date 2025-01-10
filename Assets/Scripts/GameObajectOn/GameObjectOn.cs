using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectOn : MonoBehaviour
{
    [SerializeField] GameObject OnObject;

    private bool OnFlg = false;

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
        if(OnFlg && Input.GetKeyDown(KeyCode.Space))
        {
            OnObject.SetActive(true);
        }

        else if(!OnFlg)
        {
            OnObject.SetActive(false);
        }
    }
}
