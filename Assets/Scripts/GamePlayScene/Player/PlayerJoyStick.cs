using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStick : MonoBehaviour
{
    public float playerSpeed = 8f;
    public Joystick joystick;
    Rigidbody rigidbody;
    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * playerSpeed * Time.deltaTime, Space.World);
        }
    }
}
