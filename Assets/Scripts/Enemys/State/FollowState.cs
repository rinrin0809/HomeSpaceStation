using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FollowState : IState
{
    private Enemy parent;
    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        parent.Direction = Vector2.zero;
    }

    // Update is called once per frame
    public void Update()
    {

        if (parent.Target != null)
        {
            //方向を探る
            parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;
            //敵をターゲットに向かわせる
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Target.position, parent.Speed * Time.deltaTime);
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    //void IState.Update()
    //{
    //    //方向を探る
    //    direction = (target.transform.position - transform.position).normalized;
    //    //敵をターゲットに向かわせる
    //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //}
}
