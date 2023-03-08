using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;
using UnityEngine.UI;
using UnityEditor.Build;



public class Cammouflage : MonoBehaviour
{
    [Header("Cammo Settings")]
    public bool isCammouflaged;
    [SerializeField] private bool cammoAcquired;
    
    

    [Header ("Cammo Cooldown")]
    [SerializeField] private Slider cooldownSlider;
    [Tooltip("Set the cammouflage cooldown time here")]
    [SerializeField] private float sliderUpSpeed;
    [SerializeField] private float sliderDownSpeed;
    private bool sliderDown;
    private bool sliderIsGoingUp;
    private float sliderTargetValue;
    
    

    [Header("Materials and Refs")]
    public Material[] material;
    [SerializeField] private GameObject frogChild;
    [SerializeField] private VisionTarget vtScript;
    [SerializeField] private MeshCollider playerColl;
    [SerializeField] protected ParticleSystem enterCammoParticles;
    [SerializeField] protected ParticleSystem exitCammoParticles;
   

    [Header("Wwise Events")]
    public AK.Wwise.Event cammoEnter;
    public AK.Wwise.Event cammoExit;

   

    private Renderer rend;
    private PlayerController player;
    private CapsuleCollider enemyColl;
    private bool wwiseEventPlayed;
    private float lastCammo;
    //particle system variables






    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = frogChild.GetComponent<Renderer>();
        enemyColl = GameObject.Find("Enemy/Mesh").GetComponent<CapsuleCollider>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        cammoAcquired = false;
        wwiseEventPlayed = false;
        
        isCammouflaged = false;

        //Cooldown Initiation
        cooldownSlider.value = 1;
        sliderIsGoingUp = false;
        sliderDown = false;



    }

    // Update is called once per frame
    void Update()
    {
        
        ToggleCammo();
        UseCammo();
       




    }

    public void ToggleCammo()
    {
        //changer le code ici pour que cammouflage s'active avec la statue
        
        if (Input.GetKeyDown(KeyCode.C) && cammoAcquired == false)
        {
            cammoAcquired = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cammoAcquired == true)
        {
            cammoAcquired = false;
        }

    }

    void UseCammo()
    {
        if(Input.GetKeyDown(KeyCode.H) && cammoAcquired && isCammouflaged == false)
        {
            isCammouflaged = true;
            Physics.IgnoreCollision(playerColl, enemyColl, true);
            rend.sharedMaterial = material[1];
            vtScript.visible = false;
         

            if (wwiseEventPlayed == false)
            {
                cammoEnter.Post(gameObject);
                enterCammoParticles.Play();
                wwiseEventPlayed = true;
               
                //make slider go to 0
                if (sliderDown)
                {
                    sliderTargetValue = 1f;
                    sliderDown = false;
                    sliderIsGoingUp = true;
                }
                else if (!sliderIsGoingUp)
                {
                    sliderTargetValue = 0f;
                    sliderDown = true;
                }
                if (sliderDown)
                {
                    if (cooldownSlider.value > 0)
                    {
                        cooldownSlider.value -= sliderDownSpeed * Time.deltaTime;
                    }
                    else
                    {
                        cooldownSlider.value = 0;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.H) && cammoAcquired && isCammouflaged == true)
        {
            RevealFrog();
        }
        else if(isCammouflaged && !player.isIdle || !player.grounded || Input.GetKeyDown(KeyCode.Space))
        {
            RevealFrog();
        }
        
    }
    void RevealFrog()
    {
        isCammouflaged = false;

        Physics.IgnoreCollision(playerColl, enemyColl, false);
        rend.sharedMaterial = material[0];
        vtScript.visible = true;

        if (wwiseEventPlayed == true)
        {
            cammoExit.Post(gameObject);
            exitCammoParticles.Play();
            wwiseEventPlayed = false;

            //trigger cooldown
            if(sliderIsGoingUp)
            {
                if (cooldownSlider.value < sliderTargetValue)
                {
                    cooldownSlider.value += sliderUpSpeed * Time.deltaTime;
                }
                else
                {
                    cooldownSlider.value = sliderTargetValue;
                    sliderIsGoingUp = false;
                }
            }
        }
    }
}
