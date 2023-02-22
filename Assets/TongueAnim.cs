using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAnim : MonoBehaviour
{
    private Animator tongueAnimator;

    private void Start()
    {
        tongueAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            tongueAnimator.SetTrigger("TongueOut");
        }
    }
}
