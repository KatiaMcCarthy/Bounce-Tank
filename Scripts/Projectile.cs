using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }
    private Rigidbody rb;
    [SerializeField] private LayerMask enemiesToHit;
    [HideInInspector]
    public float damage;
    [SerializeField] private float raycastLength;

    [Space(4)]
    [Header("Explosion Controls")]
    [SerializeField]
    private bool explode = false;
    [HideInInspector]
    public Vector3 target;
    
    [Space(1)]
    [SerializeField] private float explodeRadius;
    [SerializeField] private ParticleSystem missExplosion;


    [Space(4)]
    [Header("Tracking Controls")]
    [SerializeField]
    private bool tracking = false;
    [SerializeField] private float rotationSpeed;
    public Transform targetTransfrom;

    //Missle effects, followed https://www.youtube.com/watch?v=Z6qBeuN-H1M, solves target and slimes modified version by me works for missing 
    [Header("Tracking Prediction")]
    [SerializeField] private float maxDistancePredict = 100;
    [SerializeField] private float minDistancePredict = 5;
    [SerializeField] private float maxTimePrediction = 5;
    private Vector3 standardPrediction;
    private Vector3 deviatedPrediction;

    [Header("Tracking deviation")]
    [SerializeField] private float curveHeight = 50; //the hight of the curve
    [SerializeField] private float curveFrequancy = 2;  //the frequancy of the curve

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(explode)
        {
            if(Vector3.Distance(this.transform.position, target) < 1.0f)
            {
                Explode();
            }

            if(targetTransfrom != null)
            {
                if(Vector3.Distance(this.transform.position, targetTransfrom.position) < 1.0f)
                {
                    Explode();
                }
            }
        }
    }

    private void Explode()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, explodeRadius, enemiesToHit);

        if (missExplosion != null)
        {
            Instantiate(missExplosion, transform.position, Quaternion.identity);
        }

        foreach (Collider target in targets)
        {
            if (target.GetComponent<PropertyHealth>() != null)
            {
                target.GetComponent<PropertyHealth>().TakeDamage(damage);
            }
            //explosion effect
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (tracking)
        {
            //make it move to the target postion
            rb.velocity = transform.forward * Speed * Time.deltaTime;

            if (targetTransfrom != null)
            {
                var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, targetTransfrom.transform.position));

                PredictMovement(leadTimePercentage);

                AddDeviation(leadTimePercentage);

                RotateMissle();
            }
            // need to have my own millsel rotate code here
            else
            {
                //we want to change the height of the cosin over the course of the distance from the rocket to the target 
                //good explination on how mathf. inverse lerp works https://answers.unity.com/questions/1292020/what-exactly-does-mathfinverselerp-do.html
                var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, target));   //turns the distance into a percentage from us to the target

                // target is the location of the hit point
                standardPrediction = target;

                //getting how much we want the missile to move
                var deviation = new Vector3(Mathf.Cos(Time.time * curveFrequancy), 0, 0);

                //how much we offset the missle location
                var predictionOffset = transform.TransformDirection(deviation) * curveHeight * leadTimePercentage;

                deviatedPrediction = standardPrediction + predictionOffset;

                RotateMissle();
            }
        }
        else
        {
            rb.velocity = transform.forward * Speed * Time.fixedDeltaTime;
        }
    }

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);
            standardPrediction = targetTransfrom.GetComponent<Rigidbody>().position + targetTransfrom.GetComponent<Rigidbody>().velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * curveFrequancy), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * curveHeight * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    private void RotateMissle()
    {
        Vector3 heading;
            heading = deviatedPrediction - transform.position;
        Quaternion rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Slime"))
        {
            collision.collider.GetComponent<PropertyHealth>().TakeDamage(damage);
            Die();
        }

        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PropertyHealth>().TakeDamage(damage);
            Die();
        }

        if (collision.collider.CompareTag("Enviroment"))
        {
            Die();
        }
    }


    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
