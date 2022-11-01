using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public PatrolState(SmallSlimeEnemy enemy) : base(enemy)
    {

    }

    //equivilant to update
    public override void Tick()
    {
        enemy.Move();

      
    }

    //optional, is called when we enter the state
    public override void OnStateEnter()
    {
        enemy.StartMove();
    }

    public override void OnStateExit()
    {
        enemy.StopMove();
    }
}
