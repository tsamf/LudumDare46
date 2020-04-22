using UnityEngine;

public class Sidekick : MonoBehaviour
{
    // Animator vars
    private Animator animator;
    public Rigidbody rb;
    public float movespeed = 2f;

    // jump vars
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight = 35.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            animator.SetBool("grounded", grounded);
            rb.AddForce(new Vector3(0, jumpHeight, 0));
            Debug.Log(jumpHeight.ToString());
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

        rb.velocity = new Vector3(move * movespeed, rb.velocity.y, 0);

    }

}
