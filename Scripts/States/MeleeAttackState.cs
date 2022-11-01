using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{
    public MeleeAttackState(SmallSlimeEnemy enemy) : base(enemy)
    {

    }

    //equivilant to update
    public override void Tick()
    {
        enemy.MeleeAttack();

    }

    //optional, is called when we enter the state
    public override void OnStateEnter()
    {
        enemy.StartMeleeAttack();
    }
}
