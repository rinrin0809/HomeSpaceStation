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
        Wall.SetActive(true);
    }

    void Update()
    {
        if (!Event.GetNameEventActionFlg("女将との会話"))
        {
            //Debug.Log("女将と会話した");
            Wall.SetActive(false);
        }
    }
}
