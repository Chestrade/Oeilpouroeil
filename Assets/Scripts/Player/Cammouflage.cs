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
    private int maxStamina = 100;
    private int currentStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

        


    [Header("Materials and Refs")]
    public Material[] material;
    [SerializeField] private GameObject frogChild;
    [SerializeField] private GameObject frogTongue;
    [SerializeField] private VisionTarget vtScript;
    [SerializeField] private MeshCollider playerColl;
    [SerializeField] protected ParticleSystem enterCammoParticles;
    [SerializeField] protected ParticleSystem exitCammoParticles;
   

    [Header("Wwise Events")]
    public AK.Wwise.Event cammoEnter;
    public AK.Wwise.Event cammoExit;


    private EyeInteractableManager eyeManager;
    private Renderer rend;
    private Renderer tongueRend;
    private PlayerController player;
    private CapsuleCollider enemyColl;
    private bool wwiseEventPlayed;
    
    

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        eyeManager = GetComponent<EyeInteractableManager>();
        rend = frogChild.GetComponent<Renderer>();
        tongueRend = frogTongue.GetComponent<Renderer>();
        enemyColl = GameObject.Find("Enemy/Mesh").GetComponent<CapsuleCollider>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        tongueRend.sharedMaterial = material[2];
        cammoAcquired = false;
        wwiseEventPlayed = false;
        
        isCammouflaged = false;

        //Cooldown Initiation
        currentStamina = maxStamina;
        cooldownSlider.maxValue = maxStamina;
        cooldownSlider.value = maxStamina;
        



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
        if (cammoAcquired && eyeManager.pickUpEye == false) 
        {
            if (Input.GetKeyDown(KeyCode.H) && isCammouflaged == false && currentStamina == 100)
            {
                isCammouflaged = true;
                Physics.IgnoreCollision(playerColl, enemyColl, true);
                rend.sharedMaterial = material[1];
                tongueRend.sharedMaterial = material[3];
                vtScript.visible = false;


                if (wwiseEventPlayed == false)
                {
                    cammoEnter.Post(gameObject);
                    enterCammoParticles.Play();
                    wwiseEventPlayed = true;

                    //make slider go to 0
                    UseStamina(100);
                }
            }
            else if (Input.GetKeyDown(KeyCode.H) && isCammouflaged == true)
            {
                RevealFrog();
            }
            else if (isCammouflaged && !player.isIdle || !player.grounded || Input.GetKeyDown(KeyCode.Space))
            {
                RevealFrog();
            }
            else if(isCammouflaged && eyeManager.pickUpEye == true)
            {
                RevealFrog();
            }
        }
        else
        {
            return;
        }
        
        
    }
    void RevealFrog()
    {
        isCammouflaged = false;

        Physics.IgnoreCollision(playerColl, enemyColl, false);
        rend.sharedMaterial = material[0];
        tongueRend.sharedMaterial = material[2];
        vtScript.visible = true;

        if (wwiseEventPlayed == true)
        {
            cammoExit.Post(gameObject);
            exitCammoParticles.Play();
            wwiseEventPlayed = false;

            //trigger cooldown
            StartCoroutine(RegenStamina());
        }
    }

   void UseStamina(int amount)
   {
        if(currentStamina - amount >=0 )
        {
            currentStamina -= amount;
            cooldownSlider.value = currentStamina;
        }
        else
        {
            return;
        }
   }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);
        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            cooldownSlider.value = currentStamina;
            yield return regenTick;
        }

    }

}
