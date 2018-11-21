using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerShooting : MonoBehaviour
{
    //https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/
    //https://www.youtube.com/watch?v=IvT8hjy6q4o
    //https://www.youtube.com/watch?v=iLlWirdxass
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.05f;
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

    public GameObject playerBullet;

    public float velocity;
    public float angle;
    public int resolution;
    float g;
    float radianAngle;

    /*
        Ball lauchn
     */
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

        g = Mathf.Abs(Physics2D.gravity.y);
    }
    // Use this for initialization
    void Start()
    {
        //RenderArc();
    }

    public void RenderArc()
    {
        gunLine.SetVertexCount(resolution + 1);
        gunLine.SetPositions(CalculateArcArray());
    }

    public Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    public Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }


    public void OnValidate()
    {
        // if(gunLine != null && Application.isPlaying){
        //     RenderArc();
        // }
    }
    float x;
    float y;
    float z;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //
        Vector3 current = Input.mousePosition;
        x = current.x;
        y = current.y;
        z = current.z;
        //
        // if (Input.GetButton("Fire1") && timer > timeBetweenBullets && Time.timeScale != 0)
        // {
        //     Shoot();
        // }

        // if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer > timeBetweenBullets)
        // {
        //     Debug.Log("Mouse X or Mouse Y");
        //     Shoot();
        // }

        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }
    }

    public void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;
        faceLight.enabled = true;

        gunParticle.Stop();
        gunParticle.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            //gunLine.SetPosition(1, shootHit.point);
            RenderArc();
        }
        else
        {
            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            RenderArc();
        }
    }
    public void DisableEffects()
    {
        gunLight.enabled = false;
        faceLight.enabled = false;
        gunLine.enabled = false;
    }
}
