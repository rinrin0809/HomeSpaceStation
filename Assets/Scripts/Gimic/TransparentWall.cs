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
        if (!Event.GetNameEventActionFlg("�����Ƃ̉�b"))
        {
            //Debug.Log("�����Ɖ�b����");
            Wall.SetActive(false);
        }
    }
}
