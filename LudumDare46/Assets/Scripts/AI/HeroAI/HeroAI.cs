using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : MonoBehaviour
{
    public Rigidbody rigidbody = null;
    public float moveSpeed = 2f;
    public static HeroAI instance = null;
    private HealthScript hscript = null;
    //public float lookDistance = 2f;

    int currentDir = 1;
    int lastDir = 0;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        hscript = GetComponent<HealthScript>();
        instance = this;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            if(hscript.currentHealth > 0)
             hscript.UpdateHealth(-1);
            if (hscript.currentHealth < 0)
            {
                // game over
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hscript.currentHealth <= 0)
            return;

        Vector3 moveDirection = Vector3.zero;
        //RaycastHit hitInfo;
        //Physics.Raycast(transform.position, transform.forward, out hitInfo, lookDistance);
        //Debug.DrawRay(transform.position, transform.forward * lookDistance, Color.magenta);

        moveDirection = transform.position + transform.forward * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(moveDirection);
    }
}
