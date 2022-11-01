using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlimeRAttack : MonoBehaviour
{
    private float attackSpeed;
    private float damage;


    private float attackTime;
    [SerializeField] private LayerMask enemiesToAttack;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectile;
    [SerializeField] private float attackRange;
    private void Start()
    {
        this.GetComponent<SmallSlimeEnemy>().OnRangedAttackCall += DoAttack;
        attackSpeed = this.GetComponent<EnemyStats>().AttackSpeed;
        damage = this.GetComponent<EnemyStats>().RangedDamage;
    }

    private void DoAttack()
    {
        if (Time.time >= attackTime)
        {
            Attack();
            attackTime = Time.time + attackSpeed;
        }
    }

    //https://www.youtube.com/watch?v=2zVwug_agr0  , refernce for the predictive aiming, works correctly, we get from this the direction we need to look at
    private bool InterceptionDirection(out Vector3 direction, out Vector3 point)
    {
        //player pos is a
        Vector3 playerPos = GetComponent<SmallSlimeEnemy>().GetPlayer().position; //a
        // fire point is b
        // velocity of player is va
        Vector3 playerVelocity = GetComponent<SmallSlimeEnemy>().GetPlayer().GetComponent<Rigidbody>().velocity;
        // sb is speed of projectile
        float projectileSpeed = projectile.GetComponent<Projectile>().Speed * Time.fixedDeltaTime;
        // a to b (direciton)
        Vector3 firePointToPlayer = firePoint.position - playerPos;
        //dc magnitue of that vector  (distance from fp to player
        float distanceToPlayer = firePointToPlayer.magnitude;
        //alpha is the angle between the player move vector and the player pos to the fire point
        var alpha = Vector3.Angle(firePointToPlayer, playerVelocity) * Mathf.Deg2Rad;
        //sa 
        float playerSpeed = playerVelocity.magnitude;
        //r 
        var r = playerSpeed / projectileSpeed;

        if(MyMath.SolveQuadratic(a: 1 - r * r, b: 2*r*distanceToPlayer * Mathf.Cos(alpha), -(distanceToPlayer * distanceToPlayer), out var root1, out var root2) == 0)
        {
            direction = Vector3.zero;
            point = Vector3.zero;
            return false;
        }

        var dA = Mathf.Max(root1, root2);
        var t = dA / projectileSpeed;
        Vector3 c = playerPos + playerVelocity * t;
        point = c;
        direction = (c - firePoint.position).normalized;
        return true;
    }

    private void Attack()
    {
        //todo remove direciton, keeping it in for aiming purposes
        InterceptionDirection(out Vector3 direction, out Vector3 point);

        //Debug.DrawRay(firePoint.position, direction * 100, Color.red, 1.0f);
        
        firePoint.LookAt(point);
        GameObject spike = Instantiate(projectile, firePoint.position, firePoint.rotation);
        spike.GetComponent<Projectile>().target = point;
        spike.GetComponent<Projectile>().damage = damage;
        //swap back to patroling afterwards if we are no longer in range
        if (Vector3.Distance(this.transform.position, GetComponent<SmallSlimeEnemy>().GetPlayer().position) >= attackRange)
        {
            //works, need to change state afterwards
            GetComponent<SmallSlimeBrain>().patrolWeight = 100;
            GetComponent<SmallSlimeBrain>().RangedAttackWeight = 0;
            GetComponent<SmallSlimeBrain>().DecideState();
        }
    }

    private void OnDestroy()
    {
        this.GetComponent<SmallSlimeEnemy>().OnRangedAttackCall -= DoAttack;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
