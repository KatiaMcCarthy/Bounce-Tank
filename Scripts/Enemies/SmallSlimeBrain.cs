using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlimeBrain : MonoBehaviour
{
    public float patrolWeight;
    public float melleAttackWeight;
    public float RangedAttackWeight;
    public void DecideState()
    {
        float i = Random.Range(0, melleAttackWeight + patrolWeight + RangedAttackWeight);

        if(i >= 0 && i <=patrolWeight)
        {
            GetComponent<SmallSlimeEnemy>().SetState(new PatrolState(GetComponent<SmallSlimeEnemy>()));
        }
        else if (i > patrolWeight && i <= melleAttackWeight)
        {
            GetComponent<SmallSlimeEnemy>().SetState(new MeleeAttackState(GetComponent<SmallSlimeEnemy>()));
        }
        else if (i > melleAttackWeight && i <= RangedAttackWeight)
        {
            GetComponent<SmallSlimeEnemy>().SetState(new RangedAttackState(GetComponent<SmallSlimeEnemy>()));
        }
    }
}
