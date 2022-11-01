using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmallSlimeEnemy : Enemy
{
    public Action<float> OnMoveCall;
    public Action OnMeleeAttackCall;
    public Action OnRangedAttackCall;

    public Action OnStopMoveCall;
    public Action OnStartMoveCall;

    public Action<Vector3> OnStartMelleAttakCall;

    [SerializeField] private LayerMask groundLayers;
    [field: SerializeField] public bool isRanged { get; private set; }

    protected override void Start()
    {
        base.Start();
        speed = this.GetComponent<EnemyStats>().Speed;
        GetComponent<SmallSlimeBrain>().DecideState();

    }

    protected override void Update()
    {
        base.Update();
    }

    public void Move()
    {
        if (OnMoveCall != null)
        {
            OnMoveCall(speed);
        }
    }

    public void StopMove()
    {
        if(OnStopMoveCall != null)
        {
            OnStopMoveCall();
        }

    }

    public void StartMove()
    {
        if (OnStartMoveCall != null)
        {
            OnStartMoveCall();
        }
    }

    public void StartMeleeAttack()
    {
        if (OnStartMelleAttakCall != null)
        {
            OnStartMelleAttakCall(player.position);
        }
    }

    public void MeleeAttack()
    {
        if (OnMeleeAttackCall != null)
        {
            OnMeleeAttackCall();
        }
    }


    public void RangedAttack()
    {
        if (OnRangedAttackCall != null)
        {
            OnRangedAttackCall();
        }
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public bool Grounded()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.0f, groundLayers);

        if (hits.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
