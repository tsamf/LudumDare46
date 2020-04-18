using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterController controller;
    private Animator anim;

    public float speed = 12f;
    public float gravity = -18f;
    public float jumpHeight = 3f;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;    // only want to check collisions with specific (ground) objects

    Vector3 velocity;
    bool isGrounded;


    // TODO: 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);                 // sphere to check collision with items on ground layer

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                anim.SetTrigger("Leap");
                velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        }

        Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical") * Time.deltaTime).normalized;           // only z-axis 
        
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            controller.Move(Vector3.forward * -speed * Time.deltaTime);
            transform.TransformDirection(move);
            transform.rotation = Quaternion.Euler(0,180,0);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            transform.TransformDirection(move);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (move != Vector3.zero)
        {
            transform.forward = move;
            //transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        velocity.y += gravity * Time.deltaTime;

    }
}
