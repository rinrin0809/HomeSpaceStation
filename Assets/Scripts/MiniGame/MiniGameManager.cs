using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    //ミニゲームのオブジェクト
    [SerializeField] private GameObject MiniGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //アクティブ化のON・OFF
        ActiveOnOff();
    }

    //アクティブ化のON・OFF
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
