using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    public EventData Event;

    [SerializeField]
    public GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        Event.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Event.GetNameEventFlg("çïÇ¢âeÇî≠å©"))
        {
            Wall.SetActive(false);
        }

        else
        {
            Wall.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
