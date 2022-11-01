using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemiesToHit;
    [SerializeField] private Transform barrelArt;
    [SerializeField] private Image shotIndicator;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip reloadAnimation;
 


    private float attackTime;
    private InputAction shootAction;
    private RaycastHit amingRaycastHit;

    private PlayerController PC;
    private Transform target;
    private Vector3 targetPoint;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];

        PC = GetComponent<PlayerController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("CrossbowReload"))
        {
            shotIndicator.fillAmount = 1 - (animator.GetCurrentAnimatorStateInfo(0).normalizedTime / reloadAnimation.length); 
        }
        else
        {
            //Solves a wierd bug where it was at 0.0133 fill ammmount when it exits state
            if (shotIndicator.fillAmount <= 0.0134)
            {
                shotIndicator.fillAmount = 0;
            }
        }

       
        if (shootAction.ReadValue<float>() == 1 && Time.time >= attackTime)
        {
            animator.SetTrigger("Fire");
           
            shotIndicator.fillAmount = 1;
            attackTime = Mathf.Infinity;
        }
    }

    private void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height /2, Camera.main.nearClipPlane));

        if (Physics.Raycast(ray, out amingRaycastHit, Mathf.Infinity, enemiesToHit))
        {
            targetPoint = amingRaycastHit.point;
            if(amingRaycastHit.collider.CompareTag("Slime"))
            {
                target = amingRaycastHit.collider.transform;
            }
            else
            {
                target = null;
            }

            barrelArt.LookAt(amingRaycastHit.point);
            firePoint.LookAt(amingRaycastHit.point);
        }
    }


    //called in animation
    public void Shoot()
    {
        GameObject rocket = Instantiate(projectile, firePoint.position, firePoint.rotation);
        rocket.GetComponent<Projectile>().target = targetPoint;  //for explosion?
        if (target != null)
        {
            rocket.GetComponent<Projectile>().targetTransfrom = target; //for tracking?
        }
        
        PC.FireShootForce(firePoint.forward);

       

    }


    //called after reload animation finnishes
    public void Reload()
    {
        //visualBolt.gameObject.SetActive(true);
        attackTime = Time.time;
    }

    //called once fireing animation is done
    public void ReloadAnim()
    {
        animator.SetTrigger("Reload");
    }
}
