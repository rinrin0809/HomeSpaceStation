using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconOff : MonoBehaviour
{
    [SerializeField] private GameObject GameObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance.GetisMoving())
        {
            GameObj.SetActive(false);
        }

        else
        {
            GameObj.SetActive(true);
        }
    }
}
