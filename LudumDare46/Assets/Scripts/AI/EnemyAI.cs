using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 4f;
    [Header("Set dynamically")]
    public Rigidbody rgbdy = null;
    public GunController guncontroller = null;
    public HeroAI hero = null;

    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
        guncontroller = GetComponentInChildren<GunController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hero = HeroAI.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, hero.transform.position);
        transform.LookAt(hero.transform.position);

        if (distance < lookRadius)
        {
            // Shoot based on the shoot profile
            if (!guncontroller.isFiring)
            {
                guncontroller.isFiring = true;
                StartCoroutine(guncontroller.Shoot());
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
