using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction aimAction;

    [SerializeField] private Transform art;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private Transform shotForcePoint;
    [SerializeField] private float maxSpeed;

    private Vector3 moveDirection;

    private bool applyShootForce = false;
    private Vector3 shotDirection;

    [SerializeField] private Transform cam;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vCam;

    private float baseSpeed;

    private bool boost;
    private float boostStrength;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        aimAction = playerInput.actions["Aim"];
        moveAction = playerInput.actions["Move"];
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        baseSpeed = speed;
    }

    private void Update()
    {   
        moveDirection = cam.forward * moveAction.ReadValue<Vector2>().y + cam.right * moveAction.ReadValue<Vector2>().x;

        if(moveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            float singleStep = turnSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(art.forward, moveDirection, singleStep, 0.0f);

            art.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
        }

        //rough scaling, inneficancy here as it sets time scale every frame
        if(aimAction.ReadValue<float>() == 1)
        {
            Time.timeScale = 0.1f;
            if (vCam.m_Lens.FieldOfView >= 35)
            {
                vCam.m_Lens.FieldOfView -= Time.deltaTime * 500;
            }
        }
        else if(Time.timeScale != 0)
        {
            Time.timeScale = 1f;
            vCam.m_Lens.FieldOfView = 60;
        }

    }

    private void FixedUpdate()
    {
        if(applyShootForce)
        {
            m_Rigidbody.AddForceAtPosition(-shotDirection * 300, shotForcePoint.position);
            applyShootForce = false;
        }

        m_Rigidbody.AddForce(moveDirection * speed * Time.fixedDeltaTime);

        if(boost)
        {
            m_Rigidbody.velocity = moveDirection * boostStrength;
            boost = false;
        }
    }

    public void FireShootForce(Vector3 direction)
    {
        shotDirection = direction;
        applyShootForce = true;
    }

    public void ReduceSpeed(float time, float decreaseAmmount)
    {
        speed = (decreaseAmmount /100) * speed;
        m_Rigidbody.velocity = m_Rigidbody.velocity * 0.8f;
        Invoke("ResetSpeed", time);
    }

    private void ResetSpeed()
    {
        speed = baseSpeed;
    }

    public void Boost(float increaseAmmount)
    {
        boost = true;
        boostStrength = increaseAmmount;
    }
    //add if we are not grounded, our speed gets reducecd

    public void SetActionMap(string actionName)
    {
        playerInput.SwitchCurrentActionMap(actionName);
    }
}
