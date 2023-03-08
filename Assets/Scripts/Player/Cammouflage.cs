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
    [SerializeField] private int cooldownTime;
    private int currentTime;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

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
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        cammoAcquired = false;
        wwiseEventPlayed = false;
        isCammouflaged = false;

        //Cooldown Initiation
        currentTime = cooldownTime;
        cooldownSlider.maxValue = cooldownTime;
        cooldownSlider.value = cooldownTime;

        enemyColl = GameObject.Find("Enemy/Mesh").GetComponent<CapsuleCollider>();

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
        if (player.isIdle && player.grounded && cammoAcquired && currentTime >= cooldownTime)
        {
            isCammouflaged = true;
            Physics.IgnoreCollision(playerColl, enemyColl, true);
            rend.sharedMaterial = material[1];
            vtScript.visible = false;
            depleteCammo(cooldownTime);

            if (wwiseEventPlayed == false)
            {
                cammoEnter.Post(gameObject);
                enterCammoParticles.Play();
                wwiseEventPlayed = true;
            }

        }
        else
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
                StartCoroutine(ResetCooldown());
            }
        }
    }
    void depleteCammo(int amount)
    {
        if (currentTime - amount >= 0)
        {
            currentTime -= amount;
            cooldownSlider.value = currentTime; 
        }
        else
        {
            return;
        }
    }
    private IEnumerator ResetCooldown()
    {
        yield return null;
        while(currentTime < cooldownTime)
        {
            currentTime += cooldownTime / 100;
            cooldownSlider.value = currentTime;
            yield return regenTick;
        }
    }
}
