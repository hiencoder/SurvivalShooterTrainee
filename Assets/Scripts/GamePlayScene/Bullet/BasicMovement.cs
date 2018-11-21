using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.up * speed;
    }
}
