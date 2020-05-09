using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Sidekick : MonoBehaviour
{
    // Animator vars
    private Animator animator;
    public Rigidbody rigBody;
    public float movespeed = 2f;

    // jump/collision vars
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight = 30.0f;
    public AudioSource jump;

    // combat vars
    public GunController gunController = null;
    private HealthScript healthScript = null;
    public static Sidekick instance = null;
    int currentDir = 1;
    int lastDir = 1;
    public float rotationVal = 180f;

    void Awake ()
    {
        rigBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gunController = GetComponentInChildren<GunController>();
        healthScript = GetComponent<HealthScript>();
        instance = this;
    }

    void Update()
    {
        // shooting
        bool fire = Input.GetMouseButton(0);
        if (fire && !gunController.isFiring)
        {
            gunController.isFiring = true;
            StartCoroutine(gunController.Shoot());
        }
        // aiming
        Vector3 gunLookPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                                            transform.position.z - Camera.main.transform.position.z);
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
            Vector3 posx = gunController.transform.localPosition;
            posx.x = -posx.x;
            gunController.transform.localPosition = posx;
        }
    }

    void FixedUpdate()
    {

        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            animator.SetBool("grounded", grounded);         // set animation variable "grounded" to local variable
            jump.Play(0);                                   //Play jump sound
            Debug.Log("Jump");
            rigBody.AddForce(new Vector3(0, jumpHeight, 0));     
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        animator.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));

        rigBody.velocity = new Vector3(move * movespeed, rigBody.velocity.y, 0);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            if (healthScript.currentHealth > 0)
                healthScript.UpdateHealth(-1);
            if (healthScript.currentHealth <= 0)
            {
                // game over
                GameManager.instance.GameOver();
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.GetComponent<SideKickDeath>())
        {
            healthScript.UpdateHealth(-healthScript.maxHealth);
            Destroy(gameObject);
            GameManager.instance.GameOver();

        }
    }

}
