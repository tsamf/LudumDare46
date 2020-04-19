using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 4f;
    [Header("Set dynamically")]
    public Rigidbody rgbdy = null;
    public EnemyGunController egc = null;
    public HeroAI hero = null;
    private HealthScript hscript = null;

    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
        hscript = GetComponent<HealthScript>();

        egc = GetComponentInChildren<EnemyGunController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hero = HeroAI.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            if (hscript.currentHealth > 0)
                hscript.UpdateHealth(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hscript.currentHealth <= 0 || hero == null)
            return;

        float distance = Vector3.Distance(transform.position, hero.transform.position);

        if (distance < lookRadius)
        {
            egc.target = hero.transform.position;

            // Shoot based on the shoot profile
            if (!egc.isFiring)
            {
                egc.isFiring = true;
                StartCoroutine(egc.Shoot());
            }
        }
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
