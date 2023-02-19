using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGageParticles : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] protected ParticleSystem quietRipples;
    [SerializeField] protected ParticleSystem loudRipples;

    [Header("Wwise Events")]
    //public AK.Wwise.Event ribbit;
    //public AK.Wwise.Event quietStepEvent;
    //public AK.Wwise.Event loudStepEvent;


    //private ParticleSystem currentRipples;

    private PlayerController player;
    private Animator animator;

    private bool quietStep;
    private bool loudStep;

    private bool playerInAir;
   

    private void Start()
    {
        player = PlayerController.instance;
        playerInAir = false; 
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
         
            loudRipples.Play();
            //play wwise ribbit sound here
        }

        if(player.grounded)
        {
            Land();
        }

    }
    private void Step()
    { }

    private void ParticleEmit()
    {
        if(player.isIdle)
        {
            quietRipples.Stop();
            loudRipples.Stop();
        }
        else if(player.grounded && player.moveSpeed > 0 && player.moveSpeed <player.sprintSpeed)
        {
            loudRipples.Stop();
            quietRipples.Play();
            
        }
        else if(player.grounded && player.moveSpeed == player.sprintSpeed)
        {
            quietRipples.Stop();
            loudRipples.Play();
            
        }
        else
        {
            RippleStop();
        }
    }

    private void Land()
    {
       
    }

    private void RippleStop()
    {
        if (loudRipples.isPlaying)
        {
            loudRipples.Stop();
        }
        else if (quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        else
        {
            return;
        }
    }
      

   
   
}
