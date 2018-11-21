using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    //2D Shooting in Unity (Tutorial)
    public Rigidbody bullet;
    public Transform target;
    public float h = 25;
    public float gravity = -18;
    public float bulletSpeed;
    public bool debugPath;

    public GameObject startWeapon;
    public List<GameObject> activePlayerTurrets;
    public GameObject playerBullet;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        bullet.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (debugPath)
        {
            DrawPath();
        }
    }

    public void Shoot()
    {
        //Physics.gravity = Vector3.up * gravity;
        //bullet.useGravity = true;
        //bullet.velocity = CalculateLaunchData().initialVelocity;
        foreach (GameObject turret in activePlayerTurrets)
        {
            
        }
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - bullet.position.y;
        Vector3 displacementXZ = new Vector3(target.position.z - bullet.position.x, 0, target.position.z - bullet.position.z);
        float time = Mathf.Sqrt(2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * (-Mathf.Sign(gravity)), time);
    }
    public void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = bullet.position;

        int resolution = 30;
        for (int i = 0; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = bullet.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
