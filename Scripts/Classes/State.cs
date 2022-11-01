using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected SmallSlimeEnemy enemy;
    public abstract void Tick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(SmallSlimeEnemy enemy)
    {
        this.enemy = enemy;
    }

}
