using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerBoost : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController playerController;

    private InputAction boostAction;
    [SerializeField] private float boostCooldown;
    private float boostTime;

    [SerializeField] private float boostStrength;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        boostAction = playerInput.actions["Boost"];
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (boostAction.ReadValue<float>() == 1 && Time.time >= boostTime)
        {
            playerController.Boost(boostStrength);
            boostTime = Time.time + boostCooldown;
        }
    }
}
