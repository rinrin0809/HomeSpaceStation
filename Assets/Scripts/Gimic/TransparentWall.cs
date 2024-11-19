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
        if (!Event.GetNameEventFlg("�Ԍ̏�"))
        {
            Wall.SetActive(false);
        }

        else
        {
            Wall.SetActive(true);
        }
    }
}