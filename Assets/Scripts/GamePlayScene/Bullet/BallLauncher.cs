using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    //https://www.youtube.com/watch?v=IvT8hjy6q4o
    public Rigidbody ball;
    public Rigidbody enemyRigibody;
    public Transform target;
    public float h = 10;
    public float gravity = -18;
    public bool drawPath;
    public int damagePerShot = 20;
    public float timeBetweenBullet = 0.05f;
    public float range = 100f;
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticle;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;

    public Light faceLight;
    float effectDisplayTime = 0.2f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        ball.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        if (drawPath)
        {
            DrawPath();
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "")
    }
    LaunchData CalculateLaunchVelocity()
    {
        float displaceY = target.position.y - ball.position.y;
        Vector3 displaceXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displaceY - h) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displaceXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    public void Launch()
    {
        timer = 0;
        gunAudio.Play();
        gunLight.enabled = true;
        faceLight.enabled = true;
        gunParticle.Stop();
        gunParticle.Play();


        shootRay.origin = transform.position;
        shootRay.origin = transform.forward;
        if (ball.gameObject.activeSelf == false)
        {
            ball.gameObject.SetActive(true);
        }
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchVelocity().initialVelocity;

    }

    public void DrawPath()
    {
        LaunchData launchData = new LaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 0; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
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
