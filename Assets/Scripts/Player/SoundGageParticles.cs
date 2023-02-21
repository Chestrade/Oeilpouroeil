using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGageParticles : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] protected ParticleSystem quietRipples;
    [SerializeField] protected ParticleSystem loudRipples;

    [Header("Wwise Events")]
    public AK.Wwise.Event ribbit;
    public AK.Wwise.Event quietStepEvent;
    public AK.Wwise.Event loudStepEvent;


    //private ParticleSystem currentRipples;

    private PlayerController player;
    private Animator animator;

    private bool quietStep;
    private bool loudStep;
   

    private void Start()
    {
        player = PlayerController.instance;
        animator = GetComponent<Animator>();
        //currentRipples = null;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
         
            loudRipples.Play();
            ribbit.Post(gameObject);
        }
        
       

    }
    private void Step()
    {

        if(animator.GetFloat("SpeedAnimations") >=0.6)
        {
            loudRipples.Play();
            loudStepEvent.Post(gameObject);

        }
        else if(player.isIdle)
        {
            RippleStop();
        }
        
    }

    private void QuietStep()
    {
        if (animator.GetFloat("SpeedAnimations") > 0.1 && animator.GetFloat("SpeedAnimations") < 0.6)
        {
            quietRipples.Play();
            quietStepEvent.Post(gameObject);
        }
        else if (player.isIdle)
        {
            RippleStop();
        }
    }

    private void Land()
    {
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



    /* OLD CODE in Step() 
        if (player.isIdle)
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
        */



}
