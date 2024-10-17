using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;

    private Vector2 newPosition;

    private void Awake()
    {
        newPosition = transform.position;
    }

    private void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            float threshold = .1f;

            // Only calculate new position if we are under the "threshold"
            if (Vector2.Distance(transform.position, newPosition) < threshold)
            {
                newPosition = target.position - transform.position;

                if (Mathf.Abs(newPosition.x) > Mathf.Abs(newPosition.y))
                {
                    newPosition.x = target.position.x;
                    newPosition.y = transform.position.y;
                }
                else
                {
                    newPosition.x = transform.position.x;
                    newPosition.y = target.position.y;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, newPosition, step);
        }
    }
}
