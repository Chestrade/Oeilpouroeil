using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformRopeActivated : MonoBehaviour
{
    private RopeActivatedObject ropeActivatedObject;

    public Transform[] movePoints;
    public int movePosition = 0;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;

    private bool actionToggle = false;

    void Start()
    {
        ropeActivatedObject = GetComponent<RopeActivatedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ropeActivatedObject.startAction)
        {
            print("TRIGGER ACTION");

            if (movePosition < movePoints.Length - 1)
            {
                movePosition++;
            }
            else
            {
                movePosition = 0;
            }
            ropeActivatedObject.startAction = false;

            //actionToggle = true;
           // StartCoroutine(ActionDisableDelay());
        }

        transform.position = Vector3.Lerp(transform.position, movePoints[movePosition].position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, movePoints[movePosition].rotation, rotationSpeed * Time.deltaTime);
    }


    IEnumerator ActionDisableDelay()
    {
        yield return new WaitForSeconds(0.5f);
        {
            //actionToggle = false;
        }
    }
}
