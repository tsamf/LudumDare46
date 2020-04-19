using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float movementSpeed = 0.0f;
    public Transform followTransform;
    public Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        followTransform = HeroAI.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!followTransform)
            return;

        transform.position = followTransform.position + offset;
    }

    #region OldCode
    //Vector3 CurrentPosition = transform.position;
    //CurrentPosition.x = CurrentPosition.x + movementSpeed * Time.deltaTime;
    //transform.position = CurrentPosition;
    #endregion
}
