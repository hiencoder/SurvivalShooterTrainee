using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
		//Tao 3 doi tuong 
        PoolManager.instance.CreatePool(prefab, 3);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager.instance.ResuseObject(prefab, Vector3.zero, Quaternion.identity);

        }
    }
}
