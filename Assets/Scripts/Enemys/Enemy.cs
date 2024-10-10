using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
   
    private Vector2 newPosition;
    private Transform target;  // プレイヤーのTransformをインスペクターから指定

    private IState currentState;

   //public AStar AStar;


    public Transform Target 
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    protected void Awake()
    {
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        currentState.Update();
        base.Update();
    }

    //private void Awake()
    //{
    //    newPosition = transform.position;
    //}

    //private void Update()
    //{
    //    if (target == null) return;

    //    Vector2 endPosition = target.position;
    //    Vector2 delta = target.position - transform.position;

    //    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
    //    {
    //        // Y is the smaller delta => move only in Y
    //        // => keep your current X
    //        endPosition.x = transform.position.x;
    //    }
    //    else
    //    {
    //        // X is the smaller delta => move only in X
    //        // => keep your current Y
    //        endPosition.y = transform.position.y;
    //    }

    //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    //}



    public void ChangeState(IState newState)
    {
        if(currentState!=null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

}
