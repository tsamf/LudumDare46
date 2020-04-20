using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 4f;
    public float rotationVal = 180f;

    [Header("Set dynamically")]
    public Rigidbody rgbdy = null;
    public EnemyGunController egc = null;
    public HeroAI hero = null;
    public Character sideKick = null;
    private HealthScript hscript = null;
    private Renderer renderer = null;

    int currentDir = 1;
    int lastDir = 1;

    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
        hscript = GetComponent<HealthScript>();
        renderer = GetComponent<Renderer>();

        egc = GetComponentInChildren<EnemyGunController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hero = HeroAI.instance;
        sideKick = Character.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!renderer.isVisible)
            return;

        if (collision.gameObject.GetComponent<BulletController>())
        {
            if (hscript.currentHealth > 0)
                hscript.UpdateHealth(-1);
            if (hscript.currentHealth <= 0)
            {
                ScoreManager.instance.UpdateScore(1);
                Die();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hscript.currentHealth <= 0 || hero == null || sideKick == null)
            return;

        float distanceToHero = Vector3.Distance(transform.position, hero.transform.position);
        float distanceToSideKick = Vector3.Distance(transform.position, sideKick.transform.position);

        if (distanceToSideKick < lookRadius)
        {
            AimAt(sideKick.transform);
        }

        if (distanceToHero < lookRadius)
        {
            AimAt(hero.transform);
        }
    }

    private void AimAt(Transform t)
    {
        float dir = t.transform.position.x - transform.position.x;
        float sign = Mathf.Sign(dir);

        egc.target = t.transform.position;

        if (sign > 0)
        {
            currentDir = 1;
        }
        else
            currentDir = -1;
        if (lastDir != currentDir)
        {
            lastDir = currentDir;
            transform.Rotate(Vector3.up, rotationVal);
            // flip gun so we can always see it won;t hide behind the player     
            Vector3 posx = egc.transform.localPosition;
            posx.x = -posx.x;
            egc.transform.localPosition = posx;
        }

        // Shoot based on the shoot profile
        if (!egc.isFiring)
        {
            egc.isFiring = true;
            StartCoroutine(egc.Shoot());
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(hero)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
    }
}
