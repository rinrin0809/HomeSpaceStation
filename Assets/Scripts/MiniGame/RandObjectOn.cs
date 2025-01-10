using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandObjectOn : MonoBehaviour
{
    [SerializeField] GameObject Obj;
    [SerializeField] GameObject parentObject;
    [SerializeField] float IntervalTime = 1.0f;

    [SerializeField] int randomValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(parentObject.activeSelf)
        {
            if(!Obj.activeSelf)
            {
                IntervalTime -= 0.01f;
                if (IntervalTime <= 0.0f)
                {
                    randomValue = Random.Range(0, 10);

                    if ((randomValue %= 5) == 0)
                    {
                        IntervalTime = 1.0f;
                        Obj.SetActive(true);
                    }

                    else
                    {
                        IntervalTime = 1.0f;
                    }
                }
            }

            else
            {
                IntervalTime = 1.0f;
            }
        }
    }
}
