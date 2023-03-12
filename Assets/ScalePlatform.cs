using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public ScaleManager scaleManager;

    private ScaleWeight playerScaleWeight;
    private ScaleWeight rockScaleWeight;
    private ScaleWeight eyeScaleWeight;
    private ScaleWeight enemyScaleWeight;

    public float currentWeight;

    public float playerWeight = 0;
    public float rockWeight = 0;
    public float eyeWeight = 0;
    public float enemyWeight = 0;

    public bool isLeftPlatform;
    public bool isRightPlatform;

    public ScalePlatform partnerPlatform;

    private EyeInteractableManager eyeInteractManager;

    private void Start()
    {
        eyeInteractManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EyeInteractableManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.SetParent(transform);

        if (collision.gameObject.tag == "Player")
        {
            playerScaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
        }
        if (collision.gameObject.tag == "Eye")
        {
            eyeScaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
        }
        if (collision.gameObject.tag == "Rock")
        {
            rockScaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyScaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
        }
    }


    private void OnCollisionExit(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            playerWeight = 0;
            collision.gameObject.transform.SetParent(null);
        }
        if (collision.gameObject.tag == "Eye")
        {
            eyeWeight = 0;
            if (eyeInteractManager.pickUpEye == false)
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
        if (collision.gameObject.tag == "Rock")
        {
            rockWeight = 0;
            collision.gameObject.transform.SetParent(null);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyWeight = 0;
            collision.gameObject.transform.SetParent(null);
        }
    }



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerWeight = playerScaleWeight.scaleWeight;
        }
        if (collision.gameObject.tag == "Eye")
        {
            eyeWeight = eyeScaleWeight.scaleWeight;
        }
        if (collision.gameObject.tag == "Rock")
        {
            rockWeight = rockScaleWeight.scaleWeight;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyWeight = enemyScaleWeight.scaleWeight;
        }
    }


    private void Update()
    {
        currentWeight = playerWeight + eyeWeight + rockWeight + enemyWeight;


        if (partnerPlatform.currentWeight < currentWeight)
        {
            if (isLeftPlatform)
            {
                scaleManager.TipLeft();
            }
            if (isRightPlatform)
            {
                scaleManager.TipRight();
            }
        }
        if (partnerPlatform.currentWeight == currentWeight)
        {
            scaleManager.TipMiddle();
        }
        if (partnerPlatform.currentWeight > currentWeight)
        {
            if (isLeftPlatform)
            {
                scaleManager.TipRight();
            }
            if (isRightPlatform)
            {
                scaleManager.TipLeft();
            }
        }
    }
}
