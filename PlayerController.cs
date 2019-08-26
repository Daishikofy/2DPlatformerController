using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;

    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;

    private Vector2 playerMovement;
    private bool onGround = false;
    private bool isJumping;

    // Use this for initialization
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      /* 
         *Takes the input for left and right movement 
         * Multiply by the speed
         * Store in the playerMovement vector which will always be (x , 0)
      */
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalMovement = horizontalInput * speed;
        playerMovement.x = horizontalMovement;


      /*
         * The player can only jump if his character touches the "Ground"
         * Jump consists on a vertical impulse. 
         * The force of the impulse is given by "jumpHeight"
       */
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
      /*
         * Using MovePosition when on the ground blocks the jump
         * That's why we use MovePosition only when the player is "onGround" and the jump button wasn't press
         * When in the air we use addForce to move the character to give this less precise effect
      */
        if (onGround && !isJumping)      
            rigidbody2d.MovePosition(rigidbody2d.position + playerMovement * Time.deltaTime); 
        else
        {
            if (isJumping)
                isJumping = false;
            rigidbody2d.AddForce(playerMovement);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      /* 
         * I defined "Ground" as an object taged "Ground" and with a slope less then 45 degrees
         * 
         * ColliderDistance2D gives a ton on info about the collision colider including it's normal vector
         * The normal allows us to discover the inclination of the collider
         * 
         * When the player touches the ground, the velocity stored in it's rigidbody is set to ( 0 , 0 )
      */
        ColliderDistance2D distance = collision.collider.Distance(collider2d);
        if (collision.transform.CompareTag("Ground") && Vector2.Angle(distance.normal, Vector2.up) < 45)
        {
            onGround = true;
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            onGround = false;
    }

    private void Jump()
    {
        Vector2 impulse = new Vector2(0, jumpHeight);
        rigidbody2d.AddForce(impulse, ForceMode2D.Impulse);
    }
} /* End of class: PlayerController */

