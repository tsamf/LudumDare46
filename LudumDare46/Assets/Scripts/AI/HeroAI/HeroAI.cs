using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : MonoBehaviour
{
    public Rigidbody rigidbody = null;
    public float moveSpeed = 2f;

    public static HeroAI instance = null;
    //public float lookDistance = 2f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        //RaycastHit hitInfo;
        //Physics.Raycast(transform.position, transform.forward, out hitInfo, lookDistance);
        //Debug.DrawRay(transform.position, transform.forward * lookDistance, Color.magenta);

        moveDirection = transform.position + transform.forward * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(moveDirection);
    }
}
