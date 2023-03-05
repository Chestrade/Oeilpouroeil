using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimations : MonoBehaviour
{
    // References 
    public AK.Wwise.Event deathEvent;
    public AK.Wwise.Event jumpEvent;

    private PlayerController player;
    private Animator animator;



    private void Start()
    {
        player = PlayerController.instance;
        animator = GetComponent<Animator>();

    }

    
    private void Update()
    {
        SpeedAnimations();
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Death");
            deathEvent.Post(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SpeedAnimations()
    {
        if (player.isIdle == true)
        { 
            animator.SetFloat("SpeedAnimations", 0, 0.1f, Time.deltaTime);
        }


        if (player.grounded && UnityEngine.Input.GetAxisRaw("Horizontal") != 0 || UnityEngine.Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetFloat("SpeedAnimations", 0.5f, 0.1f, Time.deltaTime);

        }

        if (player.climbing)
        {
            animator.SetTrigger("Climb");
        }

    
        if (player.grounded && UnityEngine.Input.GetKey(KeyCode.LeftShift) && player.isIdle == false)
        {
            animator.SetFloat("SpeedAnimations", 1, 0.1f, Time.deltaTime);
        }


        if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && player.grounded)
        {
            animator.SetTrigger("Jump");
            jumpEvent.Post(gameObject);
        }
    }

}
