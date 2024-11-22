using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public EventData Event;

    [SerializeField]
    public GameObject Wall;

    void Start()
    {
        Event.Initialize();
    }

    void Update()
    {
        //if (!Event.GetNameEventFlg("車故障"))
        //{
        //    Wall.SetActive(false);
        //}

        //else
        //{
        //    Wall.SetActive(true);
        //}

        if (!Event.GetNameEventActionFlg("女将との会話"))
        {
            Debug.Log("女将と会話した");
            Wall.SetActive(true);
        }

        else
        {
            Debug.Log("21rjkwgnvdsjmblks");
            Wall.SetActive(false);
        }
    }
}
