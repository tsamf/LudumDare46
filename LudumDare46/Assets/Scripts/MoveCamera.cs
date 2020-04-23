using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform followTransform;
    public Vector3 offset = Vector3.zero;

    void Start()
    {
       followTransform = HeroAI.instance.transform;
    }

    void Update()
    {
        if (!followTransform)
            return;

        transform.position = followTransform.position + offset;
    }
}
