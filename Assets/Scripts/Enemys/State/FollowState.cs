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
            //������T��
            parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;
            //�G���^�[�Q�b�g�Ɍ����킹��
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Target.position, parent.Speed * Time.deltaTime);
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    //void IState.Update()
    //{
    //    //������T��
    //    direction = (target.transform.position - transform.position).normalized;
    //    //�G���^�[�Q�b�g�Ɍ����킹��
    //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //}
}
