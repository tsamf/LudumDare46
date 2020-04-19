using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator anim;
    public Rigidbody rb;
    public GunController gc = null;

    public LayerMask layermask;
    public float movespeed = 12f;
    public float jumpspeed = 12f;
    public float raydistance = 5f;
    public float jumpDuration = 3f;

    public bool isGrounded = false;
    public bool jump = false;

    private Vector3 gravity = Vector3.zero; 
    private Vector3 movement = Vector3.zero;
    private Vector3 floorpos = Vector3.zero;

    float jumpTimer = 0f;
    int currentDir = 1;
    int lastDir;

    void Awake()
    {
        gc = GetComponentInChildren<GunController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        bool fire  = Input.GetMouseButton(0);
        if (fire && !gc.isFiring)
        {
            gc.isFiring = true;
            StartCoroutine(gc.Shoot());
        }

        movement = Vector3.zero;
        movement = Vector3.right * h;
        movement = movement.normalized;

        //if (h > 0)
        //    currentDir = -1;
        //else if (h < 0)
        //    currentDir = 1;

        //if (lastDir != currentDir)
        //{
        //    lastDir = currentDir;
        //    transform.Rotate(Vector3.up, 180f);
        //}

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
            jumpTimer = jumpDuration;
        }

        if (FloorRayCast(0, 0, raydistance) == Vector3.zero )
        {
            isGrounded = false;
            if(!jump)
                gravity -= Vector3.up * Physics.gravity.y * Physics.gravity.y * Time.deltaTime;
        }

        if (FloorRayCast(0, 0, raydistance) != Vector3.zero)
        {
            isGrounded = true;
            gravity.y = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if (jump && jumpTimer > 0)
        {
            jumpTimer -= Time.unscaledDeltaTime;
            gravity = Vector3.up * jumpspeed * Time.deltaTime;
        }
        else
        {
            jumpTimer = 0f;
            jump = false;
        }

        rb.velocity = movement * movespeed + gravity;
    }

    Vector3 FloorRayCast(float offsetX, float offsetZ, float raycastLength)
    {
        RaycastHit hit;
        floorpos = transform.TransformPoint(0+offsetX, 0.5f, 0 + offsetZ);
        Debug.DrawRay(floorpos, -Vector3.up * raycastLength, Color.magenta);
        if (Physics.Raycast(floorpos, -Vector3.up, out hit, raycastLength, layermask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
