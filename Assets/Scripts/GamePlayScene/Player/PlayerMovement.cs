﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;
    //Button E to pick up item

    GameObject itemDrop;
    Transform playerTransform;
    public Joystick joystick;
    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    //https://www.youtube.com/channel/UCYbK_tjZ2OrIZFBvU6CCMiA
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void FixedUpdate()
    // {
    //     float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");

    //     float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

    //     Move(h, v);
    //     Turning();
    //     Animating(h, v);
    // }

    public void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;

        animator.SetBool("IsWalking", walking);
    }
    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }


        Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X"), 0f, CrossPlatformInputManager.GetAxisRaw("Mouse Y"));
        if (turnDir != Vector3.zero)
        {
            Vector3 playerToMouse = (transform.position + turnDir) - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * speed * Time.deltaTime, Space.World);
            float h = joystick.Horizontal;
            float v = joystick.Vertical;
            Animating(h, v);
        }

    }
    public void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }

    public void pickUp()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemDrop"))
        {

        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {

    }

}
