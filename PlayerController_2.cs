using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float airSpeed;

    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;

    private Vector2 playerMovement;
    private bool onGround = false;
    private bool isJumping;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
      /* 
         *Takes the input for left and right movement 
         * Multiply by the speed
         * Store in the playerMovement vector which will be (x , velocity)
      */
        playerMovement.x = Input.GetAxisRaw("Horizontal") * speed;

        /*
           * The player can only jump if his character touches the "Ground"
           * Jump consists on a vertical impulse. 
           * The velocity in y is set to "jumpHeight" during the frame of the jump
           * It decreases under the force of the gravity
         */
        if (Input.GetButtonDown("Jump") && onGround)
        {
            isJumping = true;
            Jump();
        }
    }

    void FixedUpdate()
    {
        /*
           * If the character is in the air, lerp is used to soften it's movements and give and inertia sensation
           * The velocity in y is always set to be the current y velocity
        */
        if (!onGround || isJumping)
        {
            if (isJumping)
                isJumping = false;
            playerMovement.x = Mathf.Lerp(rigidbody2d.velocity.x, playerMovement.x, airSpeed);
        }
        playerMovement.y = rigidbody2d.velocity.y;
        rigidbody2d.velocity = playerMovement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      /* 
         * I defined "Ground" as   object taged "Ground" and with a slope less then 45 degrees
         * 
         * ColliderDistance2D gives a ton on info about the collision colider including it's normal vector
         * The normal allows us to discover the inclination of the collider
      */
        ColliderDistance2D distance = collision.collider.Distance(collider2d);
        if (collision.transform.CompareTag("Ground") && Vector2.Angle(distance.normal, Vector2.up) < 45)
            onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            onGround = false;
    }

    private void Jump()
    {
        rigidbody2d.velocity = Vector2.up * jumpHeight;
    }
} /* End of class: PlayerController */
