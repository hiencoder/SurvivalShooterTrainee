using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    //2D Shooting in Unity (Tutorial)
    public Rigidbody bulletRigibody;
    public Transform target;
    public float h = 25;
    public float gravity = -18;
    public float bulletSpeed;
    public bool debugPath;
    public int damagePerShot = 20;
    public float timeBetweenBullet = 0.05f;
    public float range = 100f;
    ParticleSystem gunParticleSystem;
    int shootableMask;
    AudioSource gunAudio;
    Light gunLight;
    public Light faceLight;
    float effectDisplayTime = 0.2f;

    //public GameObject startWeapon;
    //public GameObject playerBullet;
    //public List<GameObject> activePlayerTurrets;

    //GameObject playerBullet;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticleSystem = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        //playerBullet = bulletRigibody.gameObject;
    }
    // Use this for initialization
    void Start()
    {
        bulletRigibody.useGravity = false;

        //activePlayerTurrets.Add(startWeapon);

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
        // if (playerBullet.activeSelf == false)
        // {
        //     playerBullet.SetActive(true);
        // }

        Physics.gravity = Vector3.up * gravity;
        bulletRigibody.useGravity = true;
        //bulletRigibody.velocity = CalculateLaunchData().initialVelocity;
        bulletRigibody.velocity = Calculate();

        // foreach (GameObject turret in activePlayerTurrets)
        // {
        //     if (ObjectPooler.instance.GetPoolObject("Player Bullet") != null)
        //     {
        //         GameObject obj = ObjectPooler.instance.GetPoolObject("Player Bullet");
        //         obj.transform.position = turret.transform.position;
        //         obj.transform.rotation = turret.transform.rotation;
        //         obj.SetActive(true);
        //     }
        //     else
        //     {
        //         Debug.LogError("Error null");
        //     }

        // }

    }

    public Vector3 Calculate()
    {
        float displacementY = target.position.y - bulletRigibody.position.y;
        Vector3 displacementXZ = new Vector3(target.position.z - bulletRigibody.position.x, 0, target.position.z - bulletRigibody.position.z);
        float time = Mathf.Sqrt(2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;
        
        return velocityXZ + velocityY * (-Mathf.Sign(gravity));
    }
    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - bulletRigibody.position.y;
        Vector3 displacementXZ = new Vector3(target.position.z - bulletRigibody.position.x, 0, target.position.z - bulletRigibody.position.z);
        float time = Mathf.Sqrt(2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * (-Mathf.Sign(gravity)), time);
    }
    public void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = bulletRigibody.position;

        int resolution = 30;
        for (int i = 0; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = bulletRigibody.position + displacement;
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
