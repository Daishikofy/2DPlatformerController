using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private int extraJumps;
    [SerializeField]
    [Range(0f, 1f)]
    private float groundReactivity;
    [SerializeField]
    [Range(0f, 1f)]
    private float airReactivity;

    [SerializeField]
    private Collider2D collider2d;

    private Rigidbody2D rigidbody2d;

    private Vector2 playerMovement;
    private bool onGround = false;
    private int remainingJumps;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        remainingJumps = extraJumps;
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
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
                Jump();
            else if (remainingJumps > 0)
            {
                remainingJumps--;
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        /*
           * If the character is in the air, lerp is used to soften it's movements and give and inertia sensation
           * The velocity in y is always set to be the current y velocity
        */
        if (onGround)
            playerMovement.x = Mathf.Lerp(rigidbody2d.velocity.x, playerMovement.x, groundReactivity);
        else
            playerMovement.x = Mathf.Lerp(rigidbody2d.velocity.x, playerMovement.x, airReactivity);

        playerMovement.y = rigidbody2d.velocity.y;
        rigidbody2d.velocity = playerMovement;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* 
           * I tagged all the objects I could on as ground and 
           * used te fact that the collider used to detect the ground
           * can only collide if the character is ouching the "ground"
        */
        if (collision.otherCollider != collider2d)
            return;
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
        remainingJumps = extraJumps;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider != collider2d)
            return;
        onGround = false;
    }

    private void Jump()
    {      
        playerMovement.x = rigidbody2d.velocity.x;
        playerMovement.y = jumpHeight;
        rigidbody2d.velocity = playerMovement;
    }
} /* End of class: PlayerController */

