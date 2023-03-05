using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAnim : MonoBehaviour
{

    public AK.Wwise.Event tongueShoot;
    

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
            tongueShoot.Post(gameObject);
        }
    }
}
