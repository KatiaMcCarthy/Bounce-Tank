using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this will exist on entities (player, slimes, particle effects, audio will be called when things happen) 
// only 32 sounds can be played at the same time, make sure to not have sounds last super long 

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip birthSound;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip actionSound;
    [SerializeField] private AudioClip secondaryActionSound;

    [SerializeField] private AudioSource source;

    private void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }

    public void OnBirthCall()
    {
        if(birthSound != null)
        source.PlayOneShot(birthSound);
    }

    public void OnMoveCall()
    {
        if (moveSound != null)
        source.PlayOneShot(moveSound);
    }

    public void OnActionCall()
    {
        if (actionSound != null)
        source.PlayOneShot(actionSound);
    }
    public void OnActionTwoCall()
    {
        if (secondaryActionSound != null)
        source.PlayOneShot(secondaryActionSound);
    }

    public void OnDeathCall()
    {
        if (deathSound != null)
        source.PlayOneShot(deathSound);
    }
}
