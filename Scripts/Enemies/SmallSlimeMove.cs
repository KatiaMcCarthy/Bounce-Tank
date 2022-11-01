using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SmallSlimeMove : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private float attackRange;
     
    private void Start()
    {
        this.GetComponent<SmallSlimeEnemy>().OnStartMoveCall += StartMove;
        this.GetComponent<SmallSlimeEnemy>().OnMoveCall += MoveToward;
        this.GetComponent<SmallSlimeEnemy>().OnStopMoveCall += StopMove;
    }

    private void StartMove()
    {
        //TODO: fix this
        this.GetComponent<RichAI>().canMove = true;
    }

    private void MoveToward(float speed)
    {
        if (GetComponent<SmallSlimeEnemy>().GetPlayer() != null)
        {
            destinationSetter.target = GetComponent<SmallSlimeEnemy>().GetPlayer();
        }

        //if we are in range and we are grounded, how do we check if we are grounded?
        if(Vector3.Distance(transform.position, GetComponent<SmallSlimeEnemy>().GetPlayer().position) <= attackRange && GetComponent<SmallSlimeEnemy>().Grounded())
        {
            GetComponent<SmallSlimeBrain>().patrolWeight = 0;

            if(GetComponent<SmallSlimeEnemy>().isRanged)
            {
                GetComponent<SmallSlimeBrain>().RangedAttackWeight = 100;
            }
            else
            {
                GetComponent<SmallSlimeBrain>().melleAttackWeight = 100;
            }
           
            GetComponent<SmallSlimeBrain>().DecideState();
        }
    }

    private void StopMove()
    {
        //TODO: fix this
        this.GetComponent<RichAI>().canMove = false;
    }

    private void OnDestroy()
    {
        this.GetComponent<SmallSlimeEnemy>().OnStartMoveCall -= StartMove;
        this.GetComponent<SmallSlimeEnemy>().OnMoveCall -= MoveToward;
        this.GetComponent<SmallSlimeEnemy>().OnStopMoveCall += StopMove;
    }
}