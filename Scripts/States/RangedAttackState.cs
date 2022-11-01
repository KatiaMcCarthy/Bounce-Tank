using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    public RangedAttackState(SmallSlimeEnemy enemy) : base(enemy)
    {

    }

    //equivilant to update
    public override void Tick()
    {
        enemy.RangedAttack();

      
    }

    //optional, is called when we enter the state
    public override void OnStateEnter()
    {
    }
}

