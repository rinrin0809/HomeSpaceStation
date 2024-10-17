using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //状態を準備する
    void Enter(Enemy parent);

    void Update();

    void Exit();
}