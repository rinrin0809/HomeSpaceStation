using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    //�~�j�Q�[���̃I�u�W�F�N�g
    [SerializeField] private GameObject MiniGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�A�N�e�B�u����ON�EOFF
        ActiveOnOff();
    }

    //�A�N�e�B�u����ON�EOFF
    private void ActiveOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (MiniGame.activeSelf)
            {
                MiniGame.SetActive(false);
            }

            else
            {
                MiniGame.SetActive(true);
            }
        }
    }
}
