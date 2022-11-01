using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerShoot shoot;
    [SerializeField] private GameObject visualBolt;


    public void TriggerShoot()
    {
        shoot.Shoot();
        visualBolt.SetActive(false);
        Debug.Log("shooting");
    }

    public void TriggerReload()
    {
        visualBolt.SetActive(true);
        shoot.Reload();
    }

    public void TriggerReloadAnim()
    {
        shoot.ReloadAnim();
    }
}
