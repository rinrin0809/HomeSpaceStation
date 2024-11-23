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
        Wall.SetActive(true);
    }

    void Update()
    {
        //if (!Event.GetNameEventFlg("ÔŒÌá"))
        //{
        //    Wall.SetActive(false);
        //}

        //else
        //{
        //    Wall.SetActive(true);
        //}

        if (!Event.GetNameEventActionFlg("—«‚Æ‚Ì‰ï˜b"))
        {
            //Debug.Log("—«‚Æ‰ï˜b‚µ‚½");
            Wall.SetActive(false);
        }
    }
}
