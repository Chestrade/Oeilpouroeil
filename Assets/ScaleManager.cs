using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public GameObject leftPlatform;
    public GameObject rightPlatform;
    public GameObject scaleTiltBar;

    public Transform[] leftPos;
    public Transform[] rightPos;

    public Transform[] rotationPos;


    public bool tipLeft;
    public bool tipRight;   
    public bool tipMid;

    public float scaleTipSpeed = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tipLeft)
        {
            leftPlatform.transform.position = Vector3.Lerp(leftPlatform.transform.position, leftPos[0].position, scaleTipSpeed * Time.deltaTime);
            rightPlatform.transform.position = Vector3.Lerp(rightPlatform.transform.position, rightPos[2].position, scaleTipSpeed * Time.deltaTime);
            scaleTiltBar.transform.rotation = Quaternion.Lerp(scaleTiltBar.transform.rotation, rotationPos[0].rotation, scaleTipSpeed * Time.deltaTime);
        }
        if (tipMid)
        {
            leftPlatform.transform.position = Vector3.Lerp(leftPlatform.transform.position, leftPos[1].position, scaleTipSpeed * Time.deltaTime);
            rightPlatform.transform.position = Vector3.Lerp(rightPlatform.transform.position, rightPos[1].position, scaleTipSpeed * Time.deltaTime);
            scaleTiltBar.transform.rotation = Quaternion.Lerp(scaleTiltBar.transform.rotation, rotationPos[1].rotation, scaleTipSpeed * Time.deltaTime);
        }
        if (tipRight)
        {
            leftPlatform.transform.position = Vector3.Lerp(leftPlatform.transform.position, leftPos[2].position, scaleTipSpeed * Time.deltaTime);
            rightPlatform.transform.position = Vector3.Lerp(rightPlatform.transform.position, rightPos[0].position, scaleTipSpeed * Time.deltaTime);
            scaleTiltBar.transform.rotation = Quaternion.Lerp(scaleTiltBar.transform.rotation, rotationPos[2].rotation, scaleTipSpeed * Time.deltaTime);
        }
    }

    public void TipLeft()
    {
        tipLeft = true;
        tipMid = false;
        tipRight = false;
    }
    public void TipMiddle()
    {
        tipLeft = false;
        tipMid = true;
        tipRight = false;
    }
    public void TipRight()
    {
        tipLeft = false;
        tipMid = false;
        tipRight = true;
    }


}
