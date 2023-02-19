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
   

    private void Start()
    {
        player = PlayerController.instance;
       
        //currentRipples = null;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
         
            loudRipples.Play();
        }
        
       

    }
    private void Step()
    {
        if(player.isIdle)
        {
            quietRipples.Stop();
            loudRipples.Stop();
        }
        else if(player.grounded && player.moveSpeed == player.walkSpeed)
        {
            quietRipples.Play();
        }
        else if(player.grounded && player.moveSpeed == player.sprintSpeed)
        {
            loudRipples.Play();
        }
        else
        {
            RippleStop();
        }
    }

    private void Land()
    {
        //currentRipples = loudRipples;
       loudRipples.Play();
    }

    private void RippleStop()
    {
        if (loudRipples.isPlaying)
        {
            loudRipples.Stop();
        }
        if (quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        else
        {
            return;
        }
    }
      

   
   
}
