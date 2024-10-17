using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
class IdleState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //�v���C���[���߂��ꍇ�͒ǐՏ�ԂɕύX���܂�
        if (parent.Target != null)
        {
            parent.ChangeState(new FollowState());
        }
    }


}
