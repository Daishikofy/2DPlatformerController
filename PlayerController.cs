using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;

    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private Vector2 playerMovement;
    [SerializeField]
    private bool onGround = false;
    private bool isJumping;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float horizontalMovement = horizontalInput * speed;
        playerMovement.x = horizontalMovement;

        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
            isJumping = true;
        }
    }


    void FixedUpdate()
    {
        if (onGround && !isJumping)
            rigidbody.MovePosition(rigidbody.position + playerMovement * Time.deltaTime);      
        else
        {
            if (isJumping)
                isJumping = false;
            rigidbody.AddForce(playerMovement);
            Debug.Log("Player movement : " + playerMovement);
        }
            
    }

    private void Jump()
    {
        Vector2 impulse = new Vector2(0, jumpHeight);
        rigidbody.AddForce(impulse, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ColliderDistance2D distance = collision.collider.Distance(collider);
        if (collision.transform.CompareTag("Ground") && Vector2.Angle(distance.normal, Vector2.up) < 45) 
            onGround = true;     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            onGround = false;
    }
}
