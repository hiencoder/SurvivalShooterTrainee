using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabolize : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //just some setup parameter
        float minY = -5f;
        Vector3 launchVelocity = new Vector3(Random.value * 20f, Random.value * 5f, 0f);

        List<Vector3> points = new List<Vector3>();

        Vector3 current = new Vector3(-5f, 0f, 0f);
        while (current.y > minY)
        {
            points.Add(current);
            current += launchVelocity * Time.fixedDeltaTime;
            launchVelocity += Physics.gravity * Time.fixedDeltaTime;
        }

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.5f, 0.05f);

        lineRenderer.SetVertexCount(points.Count);
        for (int p = 0; p < points.Count; p++)
        {
			lineRenderer.SetPosition(p,points[p]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
