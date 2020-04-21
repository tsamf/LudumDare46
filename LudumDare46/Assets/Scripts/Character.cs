using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator anim;
   
    public LayerMask layermask;
    public float movespeed = 12f;
    public float jumpspeed = 12f;
    public float raydistance = 5f;
    public float jumpDuration = 3f;
    public float rotationVal = 180f;

    [Header("set dynamically")]
    public bool isGrounded = false;
    public bool jump = false;

    private Vector3 gravity = Vector3.zero; 
    private Vector3 movement = Vector3.zero;
    private Vector3 floorpos = Vector3.zero;

    public Rigidbody rb;
    public GunController gc = null;
    private HealthScript hscript = null;
    public static Character instance = null;

    float jumpTimer = 0f;
    int currentDir = 1;
    int lastDir = 1;
    int movementHash = -1;

    void Awake()
    {
        gc = GetComponentInChildren<GunController>();
        rb = GetComponent<Rigidbody>();
        hscript = GetComponent<HealthScript>();
        instance = this;
        //anim = GetComponent<Animator>();
        //movementHash = Animator.StringToHash("movement");
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
        //anim.SetFloat(movementHash, h);

        Vector3 gunLookPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
        gunLookPos = Camera.main.ScreenToWorldPoint(gunLookPos);
        float dir = gunLookPos.x - transform.position.x;
        float sign = Mathf.Sign(dir);
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
            Vector3 posx = gc.transform.localPosition;
            posx.x = -posx.x;
            gc.transform.localPosition = posx;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            if (hscript.currentHealth > 0)
                hscript.UpdateHealth(-1);
            if (hscript.currentHealth <= 0)
            {
                // game over
                GameManager.instance.GameOver();
                Die();
            }
        }

        if(collision.gameObject.GetComponent<SideKickDeath>())
        {
            hscript.UpdateHealth(-hscript.maxHealth);
            Die();
            GameManager.instance.GameOver();

        }
    }

    private void Die()
    {
        Destroy(gameObject);
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
