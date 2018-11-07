using System.Collections;
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
    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        
        Move(h, v);
        Turning();
        Animating(h, v);
    }

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
    public void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }
    // Update is called once per frame
    void Update()
    {
		
    }

    public void MoveLeft()
    {
        Debug.Log("Move left");
    }

    public void MoveRight()
    {
        Debug.Log("Move right");
    }

    public void MoveTop()
    {
        Debug.Log("Move top");
    }

    public void MoveDown()
    {

    }
}
