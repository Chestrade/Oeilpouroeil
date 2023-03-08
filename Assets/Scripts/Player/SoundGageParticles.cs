using IndieMarc.EnemyVision;
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
    public AK.Wwise.Event landEvent; //jump event dans PlayerAnimations. Surement à mettre dans le playercontroller dans la fonction qui call le jump

    [Header("Enemy Alert")]
    [SerializeField] private float quietRange;
    [SerializeField] private float loudRange;
    private float alert_range;

    private PlayerController player;
    private Animator animator;

    
    private void Start()
    {
        player = PlayerController.instance;
        animator = GetComponent<Animator>();
        alert_range = 0f;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            loudRipples.Play();
            ribbit.Post(gameObject);
            alert_range = loudRange;
            TriggerNoise();
        }
        
       

    }
    private void Step()
    {

        if(animator.GetFloat("SpeedAnimations") >=0.6 && player.grounded)
        {
            loudRipples.Play();
            loudStepEvent.Post(gameObject);
            alert_range = loudRange;
            TriggerNoise();

        }
        else if(player.isIdle || player.grounded)
        {
            RippleStop();
        }
        
    }

    private void QuietStep()
    {
        if (animator.GetFloat("SpeedAnimations") > 0.1 && animator.GetFloat("SpeedAnimations") < 0.6 && player.grounded)
        {
            quietRipples.Play();
            quietStepEvent.Post(gameObject);
            TriggerNoise();
            alert_range = quietRange;
        }
        else if (player.isIdle || !player.grounded)
        {
            RippleStop();  
        }
    }

    private void Land()
    {
        loudRipples.Play();
        alert_range = loudRange;
        landEvent.Post(gameObject);
        TriggerNoise();
        

    }

    private void RippleStop()
    {
       alert_range = 0f;

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
    public void TriggerNoise()
    {
        List<EnemyVision> list = EnemyVision.GetAllInRange(transform.position, alert_range);
        foreach (EnemyVision enemy in list)
        {
            enemy.Alert(transform.position);
        }
       
       
    }

}

