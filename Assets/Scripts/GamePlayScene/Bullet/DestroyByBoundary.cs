using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            if (gameObject.tag == "Player Bullet")
            {
				gameObject.SetActive(false);
            }
        }
        else
        {
			Destroy(gameObject);
        }
    }
}
