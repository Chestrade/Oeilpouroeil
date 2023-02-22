using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HELLO DO NOT USE THIS, THIS SCRIPT IS FOR TESTING ONLY AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH. Thank you for your cooperation.

public class TestPlayerControlAAB : MonoBehaviour
{
    public float speed = 6.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;


    void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Character is on ground (built-in functionality of Character Controller)
        if (controller.isGrounded)
        {
            // Use input up and down for direction, multiplied by speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        // Apply gravity manually.
        moveDirection.y -= gravity * Time.deltaTime;
        // Move Character Controller
        controller.Move(moveDirection * Time.deltaTime);

        print(Input.GetAxis("Vertical"));
    }

}
