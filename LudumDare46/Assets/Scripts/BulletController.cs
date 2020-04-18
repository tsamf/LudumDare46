using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float aliveTime = 0f;

    private void Start()
    {
        Invoke("Destroy", aliveTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // add particle effect 
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
