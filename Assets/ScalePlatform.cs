using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public ScaleManager scaleManager;
    private ScaleWeight scaleWeight;
    public float currentWeight;

    public bool isLeftPlatform;
    public bool isRightPlatform;

    public ScalePlatform partnerPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Rock")
        {
            collision.gameObject.transform.SetParent(transform);
            scaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
            currentWeight = currentWeight + scaleWeight.scaleWeight;

            /*
            if (partnerPlatform.scaleWeight.scaleWeight < scaleWeight.scaleWeight)
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
            if (partnerPlatform.scaleWeight.scaleWeight == scaleWeight.scaleWeight)
            {
                scaleManager.TipMiddle();
            }
            if (partnerPlatform.scaleWeight.scaleWeight > scaleWeight.scaleWeight)
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
            */
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Rock")
        {
            collision.gameObject.transform.SetParent(null);
            scaleWeight = collision.gameObject.GetComponent<ScaleWeight>();
            currentWeight = currentWeight - scaleWeight.scaleWeight;
            //scaleManager.TipMiddle();
        }
    }

    private void Update()
    {
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
