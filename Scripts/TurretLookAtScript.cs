using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretLookAtScript : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction mouseLookAction;
    [SerializeField] private float turretSensitivity;
    [SerializeField] private GameObject cineTarget;

    [SerializeField]
    private float minCamPitch = -10;
    [SerializeField]
    private float maxCamPitch = 10;

    private float cineTargetYaw;
    private float cineTargetPitch;
    private float cameraAngleOverride;
    private Quaternion targetRotation;

    private void Awake()
    {
        mouseLookAction = playerInput.actions["MouseLook"];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //poss turret rotation solution?
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (mouseLookAction.ReadValue<Vector2>() != null)
        {
            cineTargetYaw += mouseLookAction.ReadValue<Vector2>().x * turretSensitivity * Time.deltaTime;
            cineTargetPitch -= mouseLookAction.ReadValue<Vector2>().y * turretSensitivity * Time.deltaTime;
        }

        //Clamps rotaitons
        cineTargetYaw = ClampAngle(cineTargetYaw, float.MinValue, float.MaxValue);
        cineTargetPitch = Mathf.Clamp(cineTargetPitch, minCamPitch, maxCamPitch);

        //cine will follow this target
        targetRotation = Quaternion.Euler(cineTargetPitch + cameraAngleOverride, cineTargetYaw, 0.0f);

        cineTarget.transform.rotation = targetRotation;

    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
