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
    public AK.Wwise.Event landEvent; //jump event dans PlayerAnimations. Surement ? mettre dans le playercontroller dans la fonction qui call le jump

    [Header("Enemy Alert")]
    [SerializeField] private float quietRange;
    [SerializeField] private float loudRange;
    [SerializeField] private float ribbitRange;
    private float alert_range;

    private PlayerController player;
    private Animator animator;
    private EyeInteractableManager eyeManager;

    private bool hasRibbit;
    private float timeBetweenRibbits;


    private void Start()
    {
        
        player = PlayerController.instance;
        eyeManager = GetComponent<EyeInteractableManager>();
        animator = GetComponent<Animator>();
        alert_range = 0f;
        timeBetweenRibbits = 0.8f;
        hasRibbit = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && hasRibbit == false)
        {
            loudRipples.Play();
            ribbit.Post(gameObject);
            TriggerNoise();
            alert_range = ribbitRange;
            StartCoroutine(WaitForRibbit());
        }

        //Debug.Log("Alert Range : " + alert_range);
    }
    private void Step() //run
    {

        if (animator.GetFloat("SpeedAnimations") >= 0.6 && player.grounded)
        {
            loudRipples.Play();
            loudStepEvent.Post(gameObject);
            alert_range = loudRange;
            TriggerNoise();

        }
        else if (player.isIdle || player.grounded)
        {
            RippleStop();
        }

    }

    private void QuietStep() //walk
    {
        if (animator.GetFloat("SpeedAnimations") > 0.1 && animator.GetFloat("SpeedAnimations") < 0.6 && player.grounded && eyeManager.pickUpEye == false)
        {
            quietRipples.Play();
            quietStepEvent.Post(gameObject);
            TriggerNoise();
            alert_range = quietRange;
        }
        else if (animator.GetFloat("SpeedAnimations") > 0.1 && animator.GetFloat("SpeedAnimations") < 0.6 && player.grounded && eyeManager.pickUpEye == true)
        {
            loudRipples.Play();
            loudStepEvent.Post(gameObject);
            alert_range = loudRange;
            TriggerNoise();
        }
        else if (player.isIdle || !player.grounded)
        {
            RippleStop();
        }
    }

    public void Land() //se trouve dans le player controller
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

    private IEnumerator WaitForRibbit()
    {
        hasRibbit = true;
        yield return new WaitForSeconds(timeBetweenRibbits);
        hasRibbit = false;
        alert_range = 0f;
    }

    
}

