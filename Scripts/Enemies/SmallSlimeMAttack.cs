using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlimeMAttack : MonoBehaviour
{
    //changeable baised off of difficulty
    private float attackSpeed;
    private float damage;

    private float attackTime;
    [SerializeField] private float leapSpeed;
    private Vector3 targetPos;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemiesToAttack;
    [Tooltip("As Percentage ie 30 = 30%")]
    [SerializeField] private float slowStrength;
    [SerializeField] private float slowDuration;

    private void Start()
    {
        this.GetComponent<SmallSlimeEnemy>().OnStartMelleAttakCall += SetTarget;
        this.GetComponent<SmallSlimeEnemy>().OnMeleeAttackCall += DoAttack;

        damage = this.GetComponent<EnemyStats>().Damage;
        attackSpeed = this.GetComponent<EnemyStats>().AttackSpeed;

        Debug.Log(damage + ": as: " + attackSpeed);
    }

    private void SetTarget(Vector3 target)
    {
        targetPos = target;
        InterceptionDirection(out Vector3 direciton, out Vector3 point);
        targetPos = point;
    }

    //https://www.youtube.com/watch?v=2zVwug_agr0  , refernce for the predictive aiming, works correctly, we get from this the direction we need to look at
    private bool InterceptionDirection(out Vector3 direction, out Vector3 point)
    {
        //player pos is a
        Vector3 playerPos = targetPos; //a
        // fire point is b
        // velocity of player is va
        Vector3 playerVelocity = GetComponent<SmallSlimeEnemy>().GetPlayer().GetComponent<Rigidbody>().velocity;
        // sb is speed of projectile
        float projectileSpeed = leapSpeed;
        // a to b (direciton)
        Vector3 firePointToPlayer = transform.position - playerPos;
        //dc magnitue of that vector  (distance from fp to player
        float distanceToPlayer = firePointToPlayer.magnitude;
        //alpha is the angle between the player move vector and the player pos to the fire point
        var alpha = Vector3.Angle(firePointToPlayer, playerVelocity) * Mathf.Deg2Rad;
        //sa 
        float playerSpeed = playerVelocity.magnitude;
        //r 
        var r = playerSpeed / projectileSpeed;

        if (MyMath.SolveQuadratic(a: 1 - r * r, b: 2 * r * distanceToPlayer * Mathf.Cos(alpha), -(distanceToPlayer * distanceToPlayer), out var root1, out var root2) == 0)
        {
            direction = Vector3.zero;
            point = Vector3.zero;
            return false;
        }

        var dA = Mathf.Max(root1, root2);
        var t = dA / projectileSpeed;
        Vector3 c = playerPos + playerVelocity * t;
        point = c;
        direction = (c - transform.position).normalized;
        return true;

    }


    private void DoAttack()
    {

        if (Vector3.Distance(transform.position, targetPos) >= 1.0f)
        {
            //leap/ move into range, works, but how to add prediciveness to it???
            transform.position = Vector3.MoveTowards(transform.position, targetPos, leapSpeed * Time.deltaTime);


        }
        else if (Time.time >= attackTime)
        {
            Attack();
            attackTime = Time.time + attackSpeed;
        }

    }

    private void Attack()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRadius, enemiesToAttack);

        foreach (Collider target in targets)
        {
            if(target.CompareTag("Player"))
            {
                target.GetComponent<PropertyHealth>().TakeDamage(damage);
                target.GetComponent<PlayerController>().ReduceSpeed(slowDuration,slowStrength);
            }
        }

        //works, need to change state afterwards
        GetComponent<SmallSlimeBrain>().patrolWeight = 100;
        GetComponent<SmallSlimeBrain>().melleAttackWeight = 0;
        GetComponent<SmallSlimeBrain>().DecideState();

    }

    private void OnDestroy()
    {
        this.GetComponent<SmallSlimeEnemy>().OnStartMelleAttakCall -= SetTarget;
        this.GetComponent<SmallSlimeEnemy>().OnMeleeAttackCall -= DoAttack;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);        
    }
}
