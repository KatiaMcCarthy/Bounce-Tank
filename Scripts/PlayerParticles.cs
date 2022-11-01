using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem groundImpact;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enviroment"))
        {
            ContactPoint contact = collision.GetContact(0);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            Instantiate(groundImpact, pos, rot);
        }
    }
}


