using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float movementSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CurrentPosition = transform.position;
        CurrentPosition.x = CurrentPosition.x + movementSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
    }
}
